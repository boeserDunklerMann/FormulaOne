using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bcm.Aed.FormulaOne.Model;

public partial class Racetrack
{
    public int RacetrackId { get; set; }

    public string RacetrackName { get; set; } = null!;

    public decimal? RacetrackDistanceKm { get; set; }

    public int RacetrackCountryId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();

    public virtual Country? RacetrackCountry { get; set; } = null!;
}
