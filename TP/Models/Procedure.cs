namespace TP.Models;

public class Procedure
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
    public bool IsPopular { get; set; }
    public int StepCount { get; set; }
}

