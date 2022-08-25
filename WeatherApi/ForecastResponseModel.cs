namespace WeatherApi;

public class ForecastResponseModel
{
    public List<ListWeatherEntry> List { get; set; }
}

public class WeatherEntry
{
    public WeatherInfo Main { get; set; } = new WeatherInfo();

    public List<WeatheDescription> Weather { get; set; } = new List<WeatheDescription>();
    public Dictionary<string, double>? Rain { get; set; }
}

public class ListWeatherEntry : WeatherEntry
{
    public string Dt_txt { get; set; } = "";
}

public class WeatherInfo
{
    public double Temp { get; set; } = 0.0;
}

public class WeatheDescription
{
    public string Icon { get; set; }
}
