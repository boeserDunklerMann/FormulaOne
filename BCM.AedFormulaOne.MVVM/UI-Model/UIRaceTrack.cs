using Bcm.Aed.FormulaOne.WebAPI;

namespace BCM.AedFormulaOne.MVVM.UI_Model
{
	/// <ChangeLog>
	/// <Create Datum="29.01.2024" Entwickler="AED" />
	/// </ChangeLog>
	/// <summary>
	/// class representing a team onto UI (including country)
	/// </summary>
	public class UIRaceTrack: Racetrack
	{
        public UIRaceTrack(Racetrack track)
        {
			this.RacetrackId = track.RacetrackId;
			this.RacetrackName = track.RacetrackName;
			this.RacetrackDistanceKm = track.RacetrackDistanceKm;
			this.RacetrackCountryId = track.RacetrackCountryId;
        }
		public Country? Country { get; set; }
	}
}