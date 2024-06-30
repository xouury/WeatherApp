using Gdk;
using Gtk;

class Weather : Gtk.Window {
    private int width; private int height;
    Box main;

    private Label cityLabel;
    private Label temperatureLabel;
    private Label windLabel;
    private Label humidityLabel;


    public Weather() : base("Weather Application") {
        main = new Box(Orientation.Vertical, 2);

        cityLabel = new Label("_________ (_________)"){
            Xalign = 0.01f,
            Yalign = 0
        };

        temperatureLabel = new Label("Temperature: __ C"){
            Xalign = 0.01f,
            Yalign = 0
        };

        windLabel = new Label("Wind: __ M/S"){
            Xalign = 0.01f,
            Yalign = 0
        };

        humidityLabel = new Label("Humidity: __ %"){
            Xalign = 0.01f,
            Yalign = 0
        };

        ApplyCss();
        SetUpWindow();
        
        ShowAll();
    }

    private void SetUpWindow(){
        width = 1400; height = 700;
        Resize(width, height);

        Add(main);
        AddHeader(main);

        Box horizontalLayout = new Box(Orientation.Horizontal, 20);

        Box leftSection = new Box(Orientation.Vertical, 5);
        Box rightSection = new Box(Orientation.Vertical, 5);

        AddEntryWidgets(leftSection);
        AddForecastWidgets(rightSection);

        horizontalLayout.PackStart(leftSection, true, true, 0);
        horizontalLayout.PackStart(rightSection, true, true, 0);

        main.PackStart(horizontalLayout, false, false, 30);
    }

    private void ApplyCss(){
        CssProvider cssProvider = new CssProvider();
        cssProvider.LoadFromPath("Styles.css"); 
        StyleContext.AddProviderForScreen(Screen.Default, cssProvider, StyleProviderPriority.Application);
        
        this.Name = "window";
    }

    private void AddHeader(Box container){
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

        container.PackStart(hbox, false, false, 0);
    }

     private void AddEntryWidgets(Box container){
        Box entryBox = new Box(Orientation.Vertical, 20){
            Spacing = 10
        };

        Label cityLabel = new Label("Enter a City Name:"){
            Name = "city-label",
            Xalign = 0,
            Yalign = 10
        };

        Entry cityEntry = new Entry {
            Name = "city-entry",
            PlaceholderText = "E.g. Prague, Berlin",
            WidthRequest = 350,
            HeightRequest = 30
        };

        entryBox.PackStart(cityLabel, false, false, 5);

        Button searchButton = new Button("Search"){
            Name = "search-button",
        };

        Button locationButton = new Button("Use Current Location"){
            Name = "location-button"
        };

        entryBox.PackStart(cityEntry, false, false, 5);
        entryBox.PackStart(searchButton, false, false, 5);
        entryBox.PackStart(locationButton, false, false, 5);

        Alignment alignment = new Alignment(0.05f, 0, 0, 0);
        alignment.Add(entryBox);
        container.PackStart(alignment, false, false, 0);
    }

    private void AddForecastWidgets(Box container) {
        Box detailsBox = new Box(Orientation.Vertical, 20){
            Name = "weather-box",
            WidthRequest = 830,
            HeightRequest = 230,
        };

        cityLabel.StyleContext.AddClass("label");
        temperatureLabel.StyleContext.AddClass("label");
        windLabel.StyleContext.AddClass("label");
        humidityLabel.StyleContext.AddClass("label");

        detailsBox.PackStart(cityLabel, false, false, 0);
        detailsBox.PackStart(temperatureLabel, false, false, 0);
        detailsBox.PackStart(windLabel, false, false, 0);
        detailsBox.PackStart(humidityLabel, false, false, 0);
        
        Alignment alignRight = new Alignment(0.7f, 0, 0, 0) {detailsBox};
        container.PackStart(alignRight, false, false, 20);

        Label forecastLabel = new Label("4-Day Forecast") {
            Xalign = 0.09f,
            Name = "forecast-label"
        };
        Box forecastLayout = new Box(Orientation.Horizontal, 5);

        for (int i = 1; i <= 4; i++) //TODO: needs to display the following date, not the "day 1, 2 etc"
            forecastLayout.PackStart(CreateForecastBox(i), false, false, 5);

        container.Add(forecastLabel);
        alignRight = new Alignment(0.7f, 0, 0, 0) {forecastLayout};
        container.Add(alignRight);
    }

    private Box CreateForecastBox(int index) {
        Box forecastBox = new Box(Orientation.Vertical, 5) {
            WidthRequest = 200,
            HeightRequest = 200,
            Name = "forecast-box"
        };

        Label dayLabel = new Label($"Day {index}") {
            Xalign = 0.05f,
            Yalign = 0
        };
        Label tempLabel = new Label("Temp: __ °C") {
            Xalign = 0.05f,
            Yalign = 0
        };
        Label windLabel = new Label("Wind: __ M/S") {
            Xalign = 0.05f,
            Yalign = 0
        };
        Label humidityLabel = new Label("Humidity: __ %") {
            Xalign = 0.05f,
            Yalign = 0
        };

        forecastBox.PackStart(dayLabel, false, false, 0);
        forecastBox.PackStart(tempLabel, false, false, 0);
        forecastBox.PackStart(windLabel, false, false, 0);
        forecastBox.PackStart(humidityLabel, false, false, 0);

        return forecastBox;
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
