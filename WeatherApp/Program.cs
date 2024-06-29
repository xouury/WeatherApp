using System;
using Cairo;
using Gtk;
using Pango;

public class WeatherApp : Window
{
    Entry cityEntry;
    Label weatherLabel;
    Label errorLabel;

    public WeatherApp() : base("Simple Weather App")
    {
        SetDefaultSize(1200, 700);
        SetPosition(WindowPosition.Center);

        VBox headerVBox = new VBox();
        HBox headerBox = new HBox();

        headerBox.ModifyBg(StateType.Normal, new Gdk.Color(0, 120, 215));

        Label headerLabel = new Label("Weather Dashboard");
        headerLabel.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); 
        headerLabel.SetAlignment(0.5f, 0.5f); 

        FontDescription headerFont = FontDescription.FromString("Sans 15");
        headerLabel.ModifyFont(headerFont);

        headerLabel.WidthRequest = 400;
        headerLabel.HeightRequest = 50;

        headerBox.PackStart(headerLabel, true, true, 0);
        headerVBox.PackStart(headerBox, false, false, 0);

        VBox entryBox = new VBox();
        entryBox.Spacing = 10;

        Label entryLabel = new Label("Enter the city name:");
        FontDescription labelFont = FontDescription.FromString("Sans 12");
        labelFont.Weight = Weight.Bold;
        entryLabel.ModifyFont(labelFont);
        entryBox.PackStart(entryLabel, false, false, 5);

        cityEntry = new Entry();
        cityEntry.PlaceholderText = "E.g., New York, Paris, Berlin";

        FontDescription entryFont = FontDescription.FromString("Sans 12");
        cityEntry.ModifyFont(entryFont);

        cityEntry.WidthRequest = 400;
        cityEntry.HeightRequest = 30;
        entryBox.PackStart(cityEntry, false, false, 5);

        Button searchButton = new Button("Search");
        searchButton.ModifyBg(StateType.Normal, new Gdk.Color(68, 114, 196)); 
        searchButton.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); 
        searchButton.Clicked += OnSearchButtonClicked;
        searchButton.WidthRequest = 400;
        searchButton.HeightRequest = 30;
        entryBox.PackStart(searchButton, false, false, 5);

        HBox orBox = new HBox();
        Label orLabel = new Label("or");
        orBox.PackStart(new Label(""), true, true, 0); 
        orBox.PackStart(orLabel, false, false, 0);
        orBox.PackStart(new Label(""), true, true, 0); 

        entryBox.PackStart(orBox, false, false, 5);

        Button locationButton = new Button("Use Current Location");
        locationButton.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255)); 
        locationButton.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); 
        locationButton.WidthRequest = 400;
        locationButton.HeightRequest = 30;

        entryBox.PackStart(locationButton, false, false, 5);


        Gtk.Alignment alignment = new Gtk.Alignment(0.05f, 0, 0, 0);
        alignment.Add(entryBox);

        headerVBox.PackStart(alignment, false, false, 0);

        Add(headerVBox);
        ShowAll();
    }

    void OnSearchButtonClicked(object sender, EventArgs args)
    {
        string cityName = cityEntry.Text;
    }

    public static void Main()
    {
        Application.Init();
        new WeatherApp();
        Application.Run();
    }
}