using Microsoft.EntityFrameworkCore;

namespace WeatherApi;

public class WeatherContext : DbContext
{
    public DbSet<WeatherMeasurement> WeatherMeasurements { get; set; }
    public DbSet<Ladezeitpunkt> Ladezeitpunkte { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            "Server=tcp:bootcamp-sql-server.database.windows.net,1433;Initial Catalog=lena-efcore;Persist Security Info=False;User ID=bootcamp;Password=Azubi123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    
}

public class WeatherMeasurement
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public double TemperatureC { get; set; }
    public double Regen { get; set; }
    public string Icon { get; set; }
    public int LadezeitpunktId { get; set; }
    public Ladezeitpunkt Ladezeitpunkt { get; set; }
}

public class Ladezeitpunkt
{
    public int Id { get; set; }
    public DateTime Uhrzeit { get; set; }
    public List<WeatherMeasurement> WeatherMeasurements { get; set; } = new List<WeatherMeasurement>();
}