namespace TP.Models;

public class OnboardingItem
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public Color BackgroundColor { get; set; } = Colors.Transparent;
    public Color ButtonColor { get; set; } = Colors.Transparent;
    public string ButtonText { get; set; } = string.Empty;
}

