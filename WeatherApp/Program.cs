using Gdk;
using Gtk;
using Newtonsoft.Json;

public abstract class WeatherBase : Gtk.Window
{
    protected int width = 1400, height = 700;
    protected Box main;
    protected Label cityLabel, temperatureLabel, windLabel, humidityLabel;

    public WeatherBase() : base("Weather Application")
    {
        SetDefaultSize(width, height);
        SetPosition(WindowPosition.Center);
        SetIconFromFile("cloud.png");

        cityLabel = new Label("_________ (_________)")
        {
            Xalign = 0.01f,
            Yalign = 0,
            Name = "city-weather-label"
        };

        temperatureLabel = new Label("Temperature: __ C")
        {
            Xalign = 0.01f,
            Yalign = 0
        };

        windLabel = new Label("Wind: __ M/S")
        {
            Xalign = 0.01f,
            Yalign = 0
        };

        humidityLabel = new Label("Humidity: __ %")
        {
            Xalign = 0.01f,
            Yalign = 0
        };

        main = new Box(Orientation.Vertical, 2);
        Add(main);
        ShowAll();
    }
}

public class WeatherUI : WeatherBase
{
    string APIKey = "fb08e273a4569cd1c21f412029faa1e0";
    Box[] forecastBoxes = new Box[4];

    public WeatherUI()
    {
        SetUpWindow();
        ApplyCss();
        ShowAll();
    }

    private void SetUpWindow()
    {
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

    private void ApplyCss()
    {
        CssProvider cssProvider = new CssProvider();
        cssProvider.LoadFromPath("Styles.css");
        StyleContext.AddProviderForScreen(Screen.Default, cssProvider, StyleProviderPriority.Application);

        Name = "window";
    }

    private void AddHeader(Box container)
    {
        EventBox colorBox = new EventBox()
        {
            Name = "header-event-box",
            HeightRequest = 60,
        };

        Label label = new Label("Weather Dashboard")
        {
            Name = "weather-dashboard-label"
        };

        Box vbox = new Box(Orientation.Vertical, 2);
        vbox.PackStart(colorBox, false, false, 0);

        colorBox.Add(label);

        container.PackStart(vbox, false, false, 0);
    }

    private void AddEntryWidgets(Box container)
    {
        Box entryBox = new Box(Orientation.Vertical, 20)
        {
            Spacing = 10
        };

        Label cityLabel = new Label("Enter a City Name:")
        {
            Name = "city-label",
            Xalign = 0,
            Yalign = 10
        };

        Entry cityEntry = new Entry
        {
            Name = "city-entry",
            PlaceholderText = "E.g. Prague, Berlin",
            WidthRequest = 350,
            HeightRequest = 30
        };

        entryBox.PackStart(cityLabel, false, false, 5);

        Button searchButton = new Button("Search")
        {
            Name = "search-button",
        };
        searchButton.Clicked += async (sender, e) => await OnSearchButtonClicked(cityEntry.Text);

        entryBox.PackStart(cityEntry, false, false, 5);
        entryBox.PackStart(searchButton, false, false, 5);

        Alignment alignment = new Alignment(0.05f, 0, 0, 0) { entryBox };
        container.PackStart(alignment, false, false, 0);
    }

    private void AddForecastWidgets(Box container)
    {
        Box detailsBox = new Box(Orientation.Vertical, 20)
        {
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

        Alignment alignRight = new Alignment(0.7f, 0, 0, 0) { detailsBox };
        container.PackStart(alignRight, false, false, 20);

        Label forecastLabel = new Label("4-Day Forecast")
        {
            Xalign = 0.09f,
            Name = "forecast-label"
        };
        Box forecastLayout = new Box(Orientation.Horizontal, 5);

        for (int i = 0; i < 4; i++)
        {
            forecastBoxes[i] = CreateForecastBox(i + 1);
            forecastLayout.PackStart(forecastBoxes[i], false, false, 5);
        }

        container.Add(forecastLabel);
        alignRight = new Alignment(0.7f, 0, 0, 0) { forecastLayout };
        container.Add(alignRight);
    }

    private Box CreateForecastBox(int index)
    {
        Box forecastBox = new Box(Orientation.Vertical, 5)
        {
            WidthRequest = 200,
            HeightRequest = 200,
            Name = "forecast-box"
        };

        Label dayLabel = new Label($"Day {index}")
        {
            Xalign = 0.05f,
            Yalign = 0
        };

        Label tempLabel = new Label("Temp: __ °C")
        {
            Xalign = 0.05f,
            Yalign = 0
        };

        Label windLabel = new Label("Wind: __ M/S")
        {
            Xalign = 0.05f,
            Yalign = 0
        };

        Label humidityLabel = new Label("Humidity: __ %")
        {
            Xalign = 0.05f,
            Yalign = 0
        };

        forecastBox.PackStart(dayLabel, false, false, 0);
        forecastBox.PackStart(tempLabel, false, false, 0);
        forecastBox.PackStart(windLabel, false, false, 0);
        forecastBox.PackStart(humidityLabel, false, false, 0);

        dayLabel.StyleContext.AddClass("forecast-weather-label");

        return forecastBox;
    }

    protected override bool OnDeleteEvent(Event e)
    {
        Application.Quit();
        return true;
    }

    private async Task OnSearchButtonClicked(string cityName)
    {
        WeatherData.Root weatherData = await GetWeather(cityName);
        if (weatherData != null)
        {
            UpdateCurrentWeatherDetails(weatherData);
            UpdateForecastBoxes(weatherData);
        }
    }

    private async Task<WeatherData.Root> GetWeather(string cityName)
    {
        HttpClient client = new HttpClient();
        string url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid={APIKey}&units=metric";
        var response = await client.GetStringAsync(url);
        return JsonConvert.DeserializeObject<WeatherData.Root>(response)!;
    }

    private void UpdateCurrentWeatherDetails(WeatherData.Root weatherData)
    {
        var currentWeather = weatherData.list![0];
        cityLabel.Text = $"{weatherData.city!.name} ({DateTime.Parse(currentWeather.dt_txt!).ToString("dddd")})";
        temperatureLabel.Text = $"Temperature: {currentWeather.main!.temp} °C";
        windLabel.Text = $"Wind: {currentWeather.wind!.speed} M/S";
        humidityLabel.Text = $"Humidity: {currentWeather.main.humidity} %";
    }

    private void UpdateForecastBoxes(WeatherData.Root weatherData)
    {
        for (int i = 0; i < 4; i++)
        {
            var dayForecast = weatherData.list!.Where(l => DateTime.Parse(l.dt_txt!).Hour == 12).Skip(i).FirstOrDefault();
            if (dayForecast != null)
            {
                var forecastBox = forecastBoxes[i];
                var tempLabel = (Label)forecastBox.Children[1];
                var windLabel = (Label)forecastBox.Children[2];
                var humidityLabel = (Label)forecastBox.Children[3];
                var dayLabel = (Label)forecastBox.Children[0];

                
                tempLabel.Text = $"Temp: {dayForecast.main!.temp} °C";
                windLabel.Text = $"Wind: {dayForecast.wind!.speed} M/S";
                humidityLabel.Text = $"Humidity: {dayForecast.main.humidity} %";
                dayLabel.Text = DateTime.Parse(dayForecast.dt_txt!).ToString("dddd");
            }
        }
    }
}

class WeatherData
{
    public class City
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? country { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main? main { get; set; }
        public List<Weather>? weather { get; set; }
        public Wind? wind { get; set; }
        public Sys? sys { get; set; }
        public string? dt_txt { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int humidity { get; set; }
    }

    public class Root
    {
        public List<List>? list { get; set; }
        public City? city { get; set; }
    }

    public class Sys
    {
        public string? pod { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string? main { get; set; }
        public string? description { get; set; }
        public string? icon { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
    }
}

class Program
{
    static void Main()
    {
        Application.Init();
        var w = new WeatherUI();
        Application.Run();
    }
}
