using System;

using Gdk;
using Gtk;

namespace Gimp.PicturePackage
{
  public class PicturePackage : Plugin
  {
    LayoutSet _layoutSet = new LayoutSet();

    DocumentFrame _df;

    [SaveAttribute]
    bool _flatten = false;

    [SaveAttribute]
    int _resolution = 72;

    [STAThread]
    static void Main(string[] args)
    {
      new PicturePackage(args);
    }

    public PicturePackage(string[] args) : base(args)
    {
    }

    override protected void Query()
    {
      InstallProcedure("plug_in_picture_package",
		       "Picture package",
		       "Picture package",
		       "Maurits Rijk",
		       "Maurits Rijk",
		       "2004",
		       "Picture Package...",
		       "RGB*, GRAY*",
		       null);

      MenuRegister("plug_in_picture_package", "<Image>/Filters/Render");
    }

    override protected bool CreateDialog()
    {
      gimp_ui_init("PicturePackage", true);

      _layoutSet.Load();

      Dialog dialog = DialogNew("Picture Package", "PicturePackage",
				IntPtr.Zero, 0, null, "PicturePackage");

      HBox hbox = new HBox(false, 12);
      hbox.BorderWidth = 12;
      dialog.VBox.PackStart(hbox, true, true, 0);

      VBox vbox = new VBox(false, 12);
      hbox.PackStart(vbox, false, false, 0);

      SourceFrame sf = new SourceFrame();
      vbox.PackStart(sf, false, false, 0);

      _df = new DocumentFrame(_layoutSet);
      vbox.PackStart(_df, false, false, 0);

      LabelFrame lf = new LabelFrame();
      vbox.PackStart(lf, false, false, 0);

      Frame frame = new Frame();
      hbox.PackStart(frame, true, true, 0);

      VBox fbox = new VBox();
      fbox.BorderWidth = 12;
      frame.Add(fbox);

      Preview preview = new Preview();
      preview.WidthRequest = 400;
      preview.HeightRequest = 500;
      preview.Image = _image;
      fbox.Add(preview);

      _layoutSet.SelectEvent += new SelectHandler(preview.SetLayout);

      _layoutSet.Selected = _layoutSet[0];

      dialog.ShowAll();
	
      return DialogRun();
    }

    override protected void DoSomething(Image image)
    {
      _flatten = _df.Flatten;

      // double resolution = double.Parse(_resolution.Text);
      Layout layout = _layoutSet.Selected;

      int width = (int) (layout.Width * _resolution);
      int height = (int) (layout.Height * _resolution);
      Image composed = new Image(width, height, ImageBaseType.RGB);

      layout.Render(new ImageRenderer(composed, image, _resolution));

      if (_flatten)
	{
	composed.Flatten();
	}

      new Display(composed);
      Display.DisplaysFlush();
    }
  }
  }
