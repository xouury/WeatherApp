using Gdk;
using Gtk;
using Pango;

class Weather : Gtk.Window {
    private int width; 
    private int height;
    private Box mainBox; 

    public Weather() : base("Weather Application") {
        mainBox = new Box(Orientation.Vertical, 2);

        ApplyCss();
        SetUpWindow();
        
        ShowAll();
    }

    private void SetUpWindow(){
        width = 1200; height = 700;
        Resize(width, height);
        Add(mainBox);

        AddHeader();
        AddEntryWidgets();
    }

    private void ApplyCss(){
        CssProvider cssProvider = new CssProvider();
        cssProvider.LoadFromPath("Styles.css"); 
        StyleContext.AddProviderForScreen(Screen.Default, cssProvider, StyleProviderPriority.Application);
    }

    private void AddHeader(){
        EventBox colorBox = new EventBox(){
            Name = "header-event-box",
            HeightRequest = 60,
        };

        Label label = new Label("Weather Dashboard"){
            Name = "weather-dashboard-label"
        };

        Box hbox = new Box(Orientation.Vertical, 2);
        hbox.PackStart(colorBox, false, false, 0);

        colorBox.Add(label);

        mainBox.PackStart(hbox, false, false, 0);
    }

     private void AddEntryWidgets(){
        Box entryBox = new Box(Orientation.Vertical, 2){
            Spacing = 10
        };

        Label cityLabel = new Label("Enter a city name:"){
            Name = "city-label",
            Xalign = 0,
            Yalign = 10
        };

        Entry cityEntry = new Entry {
            Name = "city-entry",
            PlaceholderText = "E.g. Prague, Berlin, Paris",
            WidthRequest = 250,
            HeightRequest = 30
        };

        entryBox.PackStart(cityLabel, false, false, 5);

        Button searchButton = new Button("Search"){
            Name = "search-button",
        };

        Button locationButton = new Button("Use current location"){
            Name = "location-button"
        };

        entryBox.PackStart(cityEntry, false, false, 5);
        entryBox.PackStart(searchButton, false, false, 5);
        entryBox.PackStart(locationButton, false, false, 5);

        Gtk.Alignment alignment = new Gtk.Alignment(0.05f, 0, 0, 0);
        alignment.Add(entryBox);
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
