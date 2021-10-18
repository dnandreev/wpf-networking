using System.Windows;
using System.Net;
using System.IO;

namespace WpfWebRequest{
	public partial class MainWindow : Window{
		const string newLine = "\r\n";

		public MainWindow() => InitializeComponent();

		void RequestToUri(object sender, RoutedEventArgs e){
			WebRequest webRequest = WebRequest.Create(uriIn.Text);
			WebResponse webResponse = webRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())){
				string readLine;
				if((readLine = streamReader.ReadLine()) != null)
					contentOut.Text = readLine;
				while((readLine = streamReader.ReadLine()) != null)
					contentOut.AppendText(newLine + readLine);
			}
			descriptionOut.Text = $"URI запроса: {webRequest.RequestUri}{newLine}Метод запроса: {webRequest.Method}{newLine}Тип данных ответа: {webResponse.ContentType}{newLine}Длина ответа: {webResponse.ContentLength}{newLine}Заголовки:";
			WebHeaderCollection webHeaderCollection = webResponse.Headers;
			foreach(string key in webHeaderCollection.AllKeys){
				descriptionOut.AppendText($"{newLine}{key}:");
				foreach(string value in webHeaderCollection.GetValues(key))
					descriptionOut.AppendText($" {value}");
			}
		}

		void ReadFile(object sender, RoutedEventArgs e){
			using(StreamReader streamReader = new StreamReader(((FileWebRequest)WebRequest.Create(fileUriIn.Text)).GetResponse().GetResponseStream()))
				fileContentOut.Text = streamReader.ReadToEnd();
		}

		void WriteFile(object sender, RoutedEventArgs e){
			FileWebRequest fileWebRequest = (FileWebRequest)WebRequest.Create(fileUriIn.Text);
			fileWebRequest.Method = "PUT";
			using(StreamWriter streamWriter = new StreamWriter(fileWebRequest.GetRequestStream()))
				streamWriter.Write(fileContentOut.Text);
		}
	}
}