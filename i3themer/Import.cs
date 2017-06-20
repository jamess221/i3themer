using System.IO;
using Gtk;

namespace i3themer
{
	public class Import
	{
		public string configPath = "";
		public string configText = "";

		public Import()
		{
			FileChooserDialog filechooser = new FileChooserDialog("Enter i3 config file location", null,
			FileChooserAction.Open,
			"Cancel", ResponseType.Cancel,
			"Open", ResponseType.Accept);
			filechooser.SetUri("$HOME$/.config/i3/");
			if (filechooser.Run() == (int)ResponseType.Accept)
			{
				configPath = filechooser.Filename;
				getConfig();
			}
			filechooser.Destroy();
		}

		public void getConfig()
		{
			configText = "";
			using (StreamReader sr = new StreamReader(configPath))
			{
				string line = sr.ReadLine();
				while (line != null)
				{
					configText += line;
					configText += "\n";
					line = sr.ReadLine();
				}
			}
		}
	}
}
