using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WeatherApi.Controllers;

[ApiController]
[Route("WeatherForecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly WeatherContext _weatherContext;

    public WeatherForecastController(HttpClient httpClient, WeatherContext weatherContext)
    {
        _httpClient = httpClient;
        _weatherContext = weatherContext;
        _httpClient.BaseAddress = new Uri("http://api.openweathermap.org/");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<WeatherForecast[]> Get()
    {

        var ladezeitpunkts = _weatherContext.Ladezeitpunkte
            .Include(l =>l.WeatherMeasurements)
            .Where(l => l.Uhrzeit > DateTime.UtcNow.AddHours(-3))
            .ToList();
        if (ladezeitpunkts.Count > 0)
        {
            var neusterLadezeitpunkt = ladezeitpunkts.OrderByDescending(l => l.Uhrzeit).First();
            return neusterLadezeitpunkt.WeatherMeasurements.Select(measurement => new WeatherForecast
            {
                Date = measurement.Date,
                TemperatureC = measurement.TemperatureC,
                Regen = measurement.Regen,
                Icon = measurement.Icon
            }).ToArray();
        }
        else
        {
            var Forecasts = await FetchDataFromOpenWeatherApi();
            var Ladezeitpunkt = new Ladezeitpunkt()
            {
                Uhrzeit = DateTime.UtcNow
            };
            _weatherContext.Ladezeitpunkte.Add(Ladezeitpunkt);
            _weatherContext.SaveChanges();

            foreach (var Forecast in Forecasts.List)
            {
                var weatherMeasurement = new WeatherMeasurement()
                {
                    Date = DateTime.Parse(Forecast.Dt_txt),
                    TemperatureC = Forecast.Main.Temp - 273.15,
                    Regen = Forecast.Rain?["3h"] ?? 0,
                    Icon = Forecast.Weather[0].Icon,
                    LadezeitpunktId = Ladezeitpunkt.Id
                };
                _weatherContext.WeatherMeasurements.Add(weatherMeasurement);
                _weatherContext.SaveChanges();

            }

            var weatherForecasts = Forecasts.List.Select(forecast => new WeatherForecast
            {
                Date = DateTime.Parse(forecast.Dt_txt),
                TemperatureC = forecast.Main.Temp - 273.15,
                Regen = forecast.Rain?["3h"] ?? 0,
                Icon = forecast.Weather[0].Icon,
            }).ToArray();
            return weatherForecasts;
        }
    }


private async Task<ForecastResponseModel?> FetchDataFromOpenWeatherApi()
    {
        var result = await _httpClient.GetAsync("data/2.5/forecast?q=Koblenz&appid=7a10c0ba1d0a76e25d7a432f8d40befc");
        result.EnsureSuccessStatusCode();

        var Forecasts = await result.Content.ReadFromJsonAsync<ForecastResponseModel>();
        return Forecasts;
    }

    [HttpGet]
    [Route("Today")]
    public async Task<WeatherForecast> GetToday()
    {
        
        var result = await _httpClient.GetAsync("data/2.5/weather?q=Koblenz&appid=7a10c0ba1d0a76e25d7a432f8d40befc");
        result.EnsureSuccessStatusCode();

        var Forecasts = await result.Content.ReadFromJsonAsync<WeatherEntry>();
        Console.WriteLine("test");

        return new WeatherForecast
        {
            Date = DateTime.Now,
            TemperatureC = Forecasts.Main.Temp - 273.15,
            Regen = Forecasts.Rain?["3h"] ?? 0,
            Icon = Forecasts.Weather[0].Icon,
        };
    }
}