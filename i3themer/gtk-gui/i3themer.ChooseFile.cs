
// This file has been generated by the GUI designer. Do not modify.
namespace i3themer
{
	public partial class ChooseFile
	{
		private global::Gtk.FileChooserWidget filechooserwidget2;

		private global::Gtk.Button buttonCancel;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget i3themer.ChooseFile
			this.Name = "i3themer.ChooseFile";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child i3themer.ChooseFile.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.filechooserwidget2 = new global::Gtk.FileChooserWidget(((global::Gtk.FileChooserAction)(0)));
			this.filechooserwidget2.Name = "filechooserwidget2";
			w1.Add(this.filechooserwidget2);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1[this.filechooserwidget2]));
			w2.Position = 0;
			// Internal child i3themer.ChooseFile.ActionArea
			global::Gtk.HButtonBox w3 = this.ActionArea;
			w3.Name = "dialog1_ActionArea";
			w3.Spacing = 10;
			w3.BorderWidth = ((uint)(5));
			w3.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w4 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w3[this.buttonCancel]));
			w4.Expand = false;
			w4.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 745;
			this.DefaultHeight = 448;
			this.Show();
		}
	}
}