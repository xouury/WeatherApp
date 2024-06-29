using Gtk;

class WeatherApp : Window{
    public WeatherApp() : base("Weather Application"){
        SetDefaultSize(800, 600);
        SetPosition(WindowPosition.Center);
        SetIconFromFile("cloud.png");

        DeleteEvent += delegate { Application.Quit(); };

        Show();
    }

    public static void Main()
    {
        Application.Init();
        new WeatherApp();        
        Application.Run();
    }
}