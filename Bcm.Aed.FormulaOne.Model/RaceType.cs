using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bcm.Aed.FormulaOne.Model;

public partial class RaceType
{
    public int RaceTypeId { get; set; }

    public string RaceTypeName { get; set; } = null!;

    public string RaceTypeShort { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
