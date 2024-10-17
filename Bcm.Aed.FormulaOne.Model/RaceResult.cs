using System;
using System.Collections.Generic;

namespace Bcm.Aed.FormulaOne.Model;

public partial class RaceResult
{
    public int RaceId { get; set; }

    public int RaceResultTypeId { get; set; }

    public long? DurationMs { get; set; }

    public decimal? DistanceKm { get; set; }

    public virtual Race Race { get; set; } = null!;

    public virtual RaceResultType RaceResultType { get; set; } = null!;
}
