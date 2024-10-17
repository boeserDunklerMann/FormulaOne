using Bcm.Aed.FormulaOne.WebAPI;

namespace BCM.AedFormulaOne.MVVM.UI_Model
{
    /// <ChangeLog>
    /// <Create Datum="29.01.2024" Entwickler="AED" />
    /// </ChangeLog>
    /// <summary>
    /// class representing a team onto UI (including country)
    /// </summary>
    public class UITeam : Team
    {
        public UITeam(Team team)
        {
            this.TeamName = team.TeamName;
            this.TeamId = team.TeamId;
            this.TeamCountryId = team.TeamCountryId;
        }

        public Country? Country { get; set; }
    }
}