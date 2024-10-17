using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bcm.Aed.FormulaOne.Model;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public byte[]? CountryFlag { get; set; }
    [JsonIgnore]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    [JsonIgnore]
    public virtual ICollection<Racetrack> Racetracks { get; set; } = new List<Racetrack>();
    [JsonIgnore]
    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
