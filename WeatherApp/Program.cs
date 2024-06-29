using Gdk;
using Gtk;
using Pango;

class Weather : Gtk.Window {
    private int width; 
    private int height;
    private Box mainBox; 

    public Weather() : base("Weather Application") {
        mainBox = new Box(Orientation.Vertical, 2);

        SetUpWindow();
        ShowAll();
    }

    private void SetUpWindow(){
        width = 1200; height = 700;
        Resize(width, height);
        Add(mainBox);

        AddHeader();
        AddLabels();
    }

    private void SetUpFont(FontDescription fontDescription, params Widget[] widgets){
        foreach (var widget in widgets)
        {
            if (widget is Label label)
            {
                label.ModifyFont(fontDescription);
            }
        }
    }

    private void AddHeader(){
        EventBox colorBox = new EventBox();
        colorBox.ModifyBg(StateType.Normal, new Gdk.Color(173, 216, 230)); 
        colorBox.HeightRequest = 60;

        Label label = new Label("Weather Dashboard");
        label.ModifyFont(FontDescription.FromString("Sans Bold 15"));

        Box hbox = new Box(Orientation.Vertical, 2);
        hbox.PackStart(colorBox, false, false, 0);

        colorBox.Add(label);

        mainBox.PackStart(hbox, false, false, 0);
    }

    private void AddLabels(){
        Box entryBox = new Box(Orientation.Vertical, 2);
        entryBox.Spacing = 10;

        Label cityLabel = new Label("Enter a city name:") {Xalign = 0, Yalign = 10};

        Entry cityEntry = new Entry {
            WidthRequest = 250,
            HeightRequest = 30
        };

        FontDescription fontDesc = FontDescription.FromString("Sans Bold 12");
        SetUpFont(fontDesc, cityLabel);

        entryBox.PackStart(cityLabel, false, false, 5);
        entryBox.PackStart(cityEntry, false, false, 5);

        Gtk.Alignment alignment = new Gtk.Alignment(0.05f, 0, 0, 0) {entryBox};

        mainBox.PackStart(alignment, false, false, 0);
    }

    protected override bool OnDeleteEvent(Event e) {
        Application.Quit();
        return true;
    }
}

class Program {
    static void Main() {
        Application.Init();
        Weather w = new Weather();
        Application.Run();
    }
}
