namespace TP.Models;

public class Office
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Ex: "Mairie", "Préfecture", "Consulat", etc.
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Schedule { get; set; } = string.Empty; // Ex: "Lun-Ven: 8h-17h"
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Icon { get; set; } = "🏛️";
}

