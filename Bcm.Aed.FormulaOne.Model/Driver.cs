using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bcm.Aed.FormulaOne.Model;

public partial class Driver
{
    public int DriverId { get; set; }

    public int? DriverCountryId { get; set; }

    public string DriverName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public int? StartingNumber { get; set; }

    public int? DriverTeamId { get; set; }

    public int? DriverVehicleId { get; set; }

    public virtual Country? DriverCountry { get; set; }

    public virtual Team? DriverTeam { get; set; }

    public virtual Vehicle? DriverVehicle { get; set; }
    [JsonIgnore]
    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
