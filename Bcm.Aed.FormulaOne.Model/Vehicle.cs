using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bcm.Aed.FormulaOne.Model;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string VehicleName { get; set; } = null!;

    public byte[]? VehiclePhoto { get; set; }
    [JsonIgnore]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
