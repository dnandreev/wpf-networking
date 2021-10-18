using System.Windows;
using System.Net;
using System.IO;

namespace WpfWebClient{
	public partial class MainWindow : Window{
		const string newLine = "\r\n";

		public MainWindow() => InitializeComponent();

		void ContentFromUri(object sender, RoutedEventArgs e){
			using(StreamReader streamReader = new StreamReader(new WebClient().OpenRead(uriIn.Text))){
				string readLine;
				if((readLine = streamReader.ReadLine()) != null)
					contentOut.Text = readLine;
				while((readLine = streamReader.ReadLine()) != null)
					contentOut.AppendText(newLine + readLine);
			}
		}
	}
}