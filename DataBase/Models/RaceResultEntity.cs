namespace DataBase.Models;

public class RaceResultEntity
{
    public int Year { get; set; }
    public int Round { get; set; }
    public string CircuitId { get; set; }
    public int Position { get; set; }
    public double Points { get; set; }
    public string Time { get; set; }
    public string DriverId { get; set; }
    public string FastLap { get; set; }
    public string TeamId { get; set; }
    public int Grid { get; set; }

}