using System;
using System.Collections.Generic;

namespace Bcm.Aed.FormulaOne.Model;

public partial class RaceResultType
{
    public int RaceResultTypeId { get; set; }

    public string RaceResultShort { get; set; } = null!;

    public virtual ICollection<RaceResult> RaceResults { get; set; } = new List<RaceResult>();
}
