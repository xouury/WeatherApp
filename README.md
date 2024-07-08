This is a simple weather dashboard application built using C# and Gtk#. 
The application fetches weather data from the OpenWeatherMap API and displays the current weather and a 4-day forecast for the specified city.

## Installation

1. **Clone the repository:** <br>
    - git clone https://github.com/xouury/WeatherApp <br>
    - cd weatherApp

2. **Install dependencies:** <br>
    - Ensure you have GTK#. If you don't, on the following website it is possible to install GTK# for macOS, Linux or Windows: https://www.mono-project.com/download/stable/#download-win <br>
    - Ensure you have Newtonsoft.Json installed. You can add it via NuGet. 

3. **Run the application:**
    dotnet run

## Usage
   - Enter the name of a city in the text box labeled "Enter a City Name".
   - Click the "Search" button to fetch and display the weather data for the specified city.

   - The weather data is updated for the current day at the next available three-hour interval. For example, if the current time is 2 PM, the next update will be for 3 PM.
   - The 4-day forecast displays weather data for 12 PM each day.
     
## OpenWeatherMap API

The application fetches weather data using the OpenWeatherMap API. To use this application, you need to have a valid API key from OpenWeatherMap. If you want to use your own key, replace the placeholder `APIKey` in the `WeatherUI` class with your actual API key.

## Contact

For any questions or suggestions, feel free to contact me at kravchenkoolesya11@gmail.com.
