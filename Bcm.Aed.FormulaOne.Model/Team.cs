using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bcm.Aed.FormulaOne.Model;

public partial class Team
{
    public int TeamId { get; set; }

    public string TeamName { get; set; } = null!;

    public int? TeamCountryId { get; set; }
    [JsonIgnore]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    [JsonIgnore]
    public virtual Country? TeamCountry { get; set; }
}
