using System;
using System.Collections.Generic;

namespace Bcm.Aed.FormulaOne.Model;

public partial class Race
{
    public int RaceId { get; set; }

    public int RacetrackId { get; set; }

    public int DriverId { get; set; }

    public DateTime RaceDate { get; set; }

    public int RaceTypeId { get; set; }

    public string? Comment { get; set; }

    public virtual Driver Driver { get; set; } = null!;

    public virtual RaceResult? RaceResult { get; set; }

    public virtual RaceType RaceType { get; set; } = null!;

    public virtual Racetrack Racetrack { get; set; } = null!;
}
