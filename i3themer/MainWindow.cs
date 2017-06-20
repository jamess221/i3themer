using System;
using Gtk;
using i3themer;
using System.Diagnostics;
using System.IO;
using System.Drawing;

public partial class MainWindow : Window
{
	Import io;

	public MainWindow() : base(WindowType.Toplevel)
	{
		Build();
		io = new Import();
		configText.Buffer.Text = io.configText;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void onClick(object sender, System.EventArgs e)
	{
		io = new Import();
		configText.Buffer.Text = io.configText;
	}

	protected void testClicked(object sender, EventArgs e)
	{
		string path = io.configPath.Substring(0, io.configPath.Length - 6) + "config-old";
		Console.WriteLine(path);
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.Move(io.configPath, path);
		using (StreamWriter sw = new StreamWriter(io.configPath))
		{
			sw.Write(io.configText);
		}
		ExecuteCommand("i3-msg reload");
		File.Delete(io.configPath);
		File.Move(path, io.configPath);
	}

	protected void applyClicked(object sender, EventArgs e)
	{
		string path = io.configPath.Substring(0, io.configPath.Length - 6) + "config-backup";
		if (File.Exists(path))
		{
			File.Delete(path);
		}
		File.Move(io.configPath, path);
		using (StreamWriter sw = new StreamWriter(io.configPath))
		{
			sw.Write(io.configText);
		}
		ExecuteCommand("i3-msg restart");
	}

	protected void onChange(object sender, System.EventArgs e)
	{
		int startIndex = io.configText.IndexOf("font pango:", System.StringComparison.Ordinal);
		Console.WriteLine(startIndex);
		int endIndex = io.configText.IndexOf("\n", startIndex, System.StringComparison.Ordinal);
		Console.WriteLine(endIndex);
		io.configText = io.configText.Substring(0, startIndex) + io.configText.Substring(endIndex);
		io.configText = io.configText.Insert(startIndex, "font pango:" + (string)fontSelector.FontName);
		int startIndex2 = io.configText.LastIndexOf("font pango:", System.StringComparison.Ordinal);
		Console.WriteLine(startIndex2);
		int endIndex2 = io.configText.IndexOf("\n", startIndex2, System.StringComparison.Ordinal);
		Console.WriteLine(endIndex2);
		io.configText = io.configText.Substring(0, startIndex2) + io.configText.Substring(endIndex2);
		io.configText = io.configText.Insert(startIndex2, "font pango:" + (string)fontSelector.FontName);
		configText.Buffer.Text = io.configText;
	}

	protected void textColourSet(object sender, EventArgs e)
	{
		replaceColour("focused_workspace", 3, getColourString(textColourBtn.Color));
		replaceColour("active_workspace", 3, getColourString(textColourBtn.Color));
		replaceColour("inactive_workspace", 3, getColourString(textColourBtn.Color));
		replaceColour("urgent_workspace", 3, getColourString(textColourBtn.Color));
		replaceColour("client.focused", 3, getColourString(textColourBtn.Color));
		replaceColour("client.unfocused", 3, getColourString(textColourBtn.Color));
		replaceColour("client.focused_inactive", 3, getColourString(textColourBtn.Color));
		replaceColour("client.urgent", 3, getColourString(textColourBtn.Color));
		configText.Buffer.Text = io.configText;
	}

	public string getColourString(Gdk.Color input)
	{
		string redValue = ((int)input.Red / 257).ToString("x2");
		string greenValue = ((int)input.Green / 257).ToString("x2");
		string blueValue = ((int)input.Blue / 257).ToString("x2");
		return "#" + redValue + greenValue + blueValue;
	}

	public void replaceColour(string keyword, int target, string newVal)
	{
		int startIndex = keyword.Length + io.configText.IndexOf(keyword, StringComparison.Ordinal);
		Console.WriteLine(io.configText[startIndex]);
		int currVal = 0;
		while (currVal < target)
		{
			if (io.configText[startIndex] == '#')
			{
				currVal++;
			}
			startIndex++;
		}
		io.configText = io.configText.Substring(0, startIndex - 1) + io.configText.Substring(startIndex + 6);
		io.configText = io.configText.Insert(startIndex - 1, newVal);
	}

	public static void ExecuteCommand(string command)
	{
		Process proc = new System.Diagnostics.Process();
		proc.StartInfo.FileName = "/bin/bash";
		proc.StartInfo.Arguments = "-c \" " + command + " \"";
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardOutput = true;
		proc.Start();
		while (!proc.StandardOutput.EndOfStream)
		{
			Console.WriteLine(proc.StandardOutput.ReadLine());
		}
	}

	protected void NeutralClicked(object sender, EventArgs e)
	{
		replaceColour("inactive_workspace", 1, getColourString(neutralColourBtn.Color));
		replaceColour("inactive_workspace", 2, getColourString(neutralColourBtn.Color));
		replaceColour("client.unfocused", 1, getColourString(neutralColourBtn.Color));
		replaceColour("client.unfocused", 2, getColourString(neutralColourBtn.Color));
		replaceColour("client.focused_inactive", 1, getColourString(neutralColourBtn.Color));
		replaceColour("client.focused_inactive", 2, getColourString(neutralColourBtn.Color));
		int startIndex = 11 + io.configText.IndexOf("background", StringComparison.Ordinal);
		io.configText = io.configText.Substring(0, startIndex) + io.configText.Substring(startIndex + 7);
		io.configText = io.configText.Insert(startIndex, getColourString(neutralColourBtn.Color));

		configText.Buffer.Text = io.configText;
	}

	protected void focusedColourSet(object sender, EventArgs e)
	{
		setFocused(focusedColourBtn.Color);
		configText.Buffer.Text = io.configText;
	}

	public void setFocused(Gdk.Color colour)
	{
		replaceColour("focused_workspace", 1, getColourString(colour));
		replaceColour("focused_workspace", 2, getColourString(colour));
		replaceColour("active_workspace", 1, getColourString(colour));
		replaceColour("active_workspace", 2, getColourString(colour));
		replaceColour("client.focused", 1, getColourString(colour));
		replaceColour("client.focused", 2, getColourString(colour));
	}

	protected void urgentColourSet(object sender, EventArgs e)
	{
		setUrgent(urgentColourBtn.Color);
	}

	public void setUrgent(Gdk.Color colour)
	{
		replaceColour("urgent_workspace", 1, getColourString(colour));
		replaceColour("urgent_workspace", 2, getColourString(colour));
		replaceColour("client.urgent", 1, getColourString(colour));
		replaceColour("client.urgent", 2, getColourString(colour));
		configText.Buffer.Text = io.configText;
	}

	protected void generateClicked(object sender, EventArgs e)
	{
		if (generateTypeBtn.ActiveText == "Harmonious")
		{
			focusedColourBtn.Color = getHarmColour(neutralColourBtn.Color);
			setFocused(focusedColourBtn.Color);
			urgentColourBtn.Color = getHarmColour(focusedColourBtn.Color);
			setUrgent(urgentColourBtn.Color);
		}
		else if (generateTypeBtn.ActiveText == "Tonal")
		{
			focusedColourBtn.Color = getTonalColour(neutralColourBtn.Color);
			setFocused(focusedColourBtn.Color);
			urgentColourBtn.Color = getTonalColour(focusedColourBtn.Color);
			setUrgent(urgentColourBtn.Color);
		}
		else if (generateTypeBtn.ActiveText == "Complementary")
		{
			focusedColourBtn.Color = getCompColour(neutralColourBtn.Color, 0.4f);
			setFocused(focusedColourBtn.Color);
			urgentColourBtn.Color = getCompColour(focusedColourBtn.Color, 0.25f);
			setUrgent(urgentColourBtn.Color);
		}
	}

	public Gdk.Color getHarmColour(Gdk.Color input)
	{
		ColorRGB tempColor = (ColorRGB)Color.FromArgb(input.Red / 257, input.Green / 257, input.Blue / 257);
		float newHue = (tempColor.H + 0.09f) % 1f;
		tempColor = ColorRGB.FromHSL(newHue, tempColor.S, tempColor.L);
		return new Gdk.Color(tempColor.R, tempColor.G, tempColor.B);
	}

	public Gdk.Color getCompColour(Gdk.Color input, float shift)
	{
		ColorRGB tempColor = (ColorRGB)Color.FromArgb(input.Red / 257, input.Green / 257, input.Blue / 257);
		float newHue = (tempColor.H + shift) % 1f;
		tempColor = ColorRGB.FromHSL(newHue, tempColor.S, tempColor.L);
		return new Gdk.Color(tempColor.R, tempColor.G, tempColor.B);
	}

	public Gdk.Color getTonalColour(Gdk.Color input)
	{
		ColorRGB tempColor = (ColorRGB)Color.FromArgb(input.Red / 257, input.Green / 257, input.Blue / 257);
		float newLight = (tempColor.L + 0.12f);
		if (newLight > 1.0f)
			newLight = 1.0f;
		tempColor = ColorRGB.FromHSL(tempColor.H, tempColor.S , newLight);
		return new Gdk.Color(tempColor.R, tempColor.G, tempColor.B);
	}
}