using Bcm.Aed.FormulaOne.WebAPI;
using BCM.AedFormulaOne.MVVM;
using BCM.AedFormulaOne.MVVM.UI_Model;
using System.Collections.ObjectModel;

namespace Bcm.Aed.FormulaOne.MVVM
{
	/// <ChangeLog>
	/// <Create Datum="29.01.2024" Entwickler="AED" />
	/// </ChangeLog>
	/// <summary>
	/// the mainwindows VM
	/// </summary>
	public class MainVM : BaseVM
	{
		#region private fields
		private MetaClient? _meta;
		private RaceClient? _race;
		private DriverClient? _driver;
		#endregion

		#region C'tor
		public MainVM() : base()
		{
			_meta = new MetaClient(_apiUrl, _httpClient);
			_race = new RaceClient(_apiUrl, _httpClient);
			_driver = new DriverClient(_apiUrl, _httpClient);

			_teams = new ObservableCollection<UITeam>();
			_countries = new ObservableCollection<Country>();
			_tracks = new ObservableCollection<UIRaceTrack>();
			_drivers = new ObservableCollection<Driver>();
			LoadListsAsync();
		}
		#endregion

		#region Bound props
		private ObservableCollection<Driver> _drivers;
		/// <summary>
		/// bound list of drivers
		/// </summary>
		public ObservableCollection<Driver> Drivers
		{
			get => _drivers;
			set
			{
				_drivers = value;
				RaisePropertyChangedEvent(nameof(Driver));
			}
		}

		private ObservableCollection<UIRaceTrack> _tracks;
		/// <summary>
		/// bound list of racetracks
		/// </summary>
		public ObservableCollection<UIRaceTrack> Tracks
		{
			get { return _tracks; }
			set
			{
				_tracks = value;
				RaisePropertyChangedEvent(nameof(Tracks));
			}
		}
		private ObservableCollection<UITeam> _teams;
		/// <summary>
		/// bound list of teams
		/// </summary>
		public ObservableCollection<UITeam> Teams
		{
			get { return _teams; }
			set
			{
				_teams = value;
				RaisePropertyChangedEvent(nameof(Teams));
			}
		}
		public object _teamListLockObj = new object();
		private string? _apiUrl = "http://localhost:5122/"; // TODO: move this to config!
		/// <summary>
		/// URL to WebAPI (bound to UI-textbox)
		/// </summary>
		public string? ApiUrl
		{
			get => _apiUrl;
			set
			{
				_apiUrl = value;
				RaisePropertyChangedEvent(nameof(ApiUrl));
			}
		}
		private bool _autoSave = true;  // TODO: move this to config!
		/// <summary>
		/// Save changes automatically to db
		/// </summary>
		public bool AutoSave
		{
			get => _autoSave;
			set
			{
				_autoSave = value;
				RaisePropertyChangedEvent(nameof(AutoSave));
			}
		}

		public object _countryListLockObj = new object();
		private ObservableCollection<Country> _countries;
		/// <summary>
		/// bound list of countries
		/// </summary>
		public ObservableCollection<Country> Countries 
		{ 
			get => _countries;
			set
			{
				_countries = value;
				RaisePropertyChangedEvent(nameof(Countries));
			}
		}

		private Country? _selectedCountry;
        public Country? SelectedCountry
		{
			get => _selectedCountry;
			set
			{
				_selectedCountry = value;
				RaisePropertyChangedEvent(nameof(SelectedCountry));
			}
		}
        #endregion

        #region Commands
        public MVVM.DelegateCommand ApplyApiChange => new DelegateCommand(ApplyApiUrlChange);
		public MVVM.DelegateCommand SaveChanges => new DelegateCommand(SaveChangesAsync);
		public DelegateCommand RemoveFlag => new DelegateCommand(RemoveFlagImageAsync);
		#endregion

		#region (priv.) methods
		private async void ApplyApiUrlChange()
		{
			if (_meta == null)  // escalate !!!
				throw new NullReferenceException(nameof(_meta));
			if (_race == null)  // escalate !!!
				throw new NullReferenceException(nameof(_race));
			if (_driver == null)  // escalate !!!
				throw new NullReferenceException(nameof(_driver));
			if (_apiUrl != null)
			{
				//_httpClient.Dispose();	// TODO: unsauber, ich weiß, auf die Schnelle muss es erstmal so gehen
				_httpClient = new HttpClient();
				_httpClient.BaseAddress = new Uri(_apiUrl);	// BaseAddress must not be set a second time
				_meta.BaseUrl = _apiUrl.ToString();
				_race.BaseUrl = _apiUrl.ToString();
				_driver.BaseUrl = _apiUrl.ToString();
				await LoadListsAsync();
			}
		}

		private async void SaveChangesAsync()
		{
			if (_meta == null)  // escalate !!!
				throw new NullReferenceException(nameof(_meta));
			Countries.Where(c => c.CountryId != 0).ToList()
				.ForEach(async country => await _meta.CountriesPutAsync(country));
			Countries.Where(c => c.CountryId == 0).ToList()
				.ForEach(async country => await _meta.CountriesPostAsync(country));
			await Task.CompletedTask;	// does nothing but avoids warning
		}

		public async Task SelectImageAsync(string filename)
		{
			if (_meta == null)  // escalate !!!
				throw new NullReferenceException(nameof(_meta));
			if (SelectedCountry == null)
				throw new NullReferenceException(nameof(SelectedCountry));
			SelectedCountry.CountryFlag = await File.ReadAllBytesAsync(filename);
			await _meta.CountriesPutAsync(SelectedCountry);
			RaisePropertyChangedEvent(nameof(Countries));
		}

		private async Task LoadListsAsync()
		{
			if (_meta == null)  // escalate !!!
				throw new NullReferenceException(nameof(_meta));
			if (_race == null)
				throw new NullReferenceException(nameof(_race));
			if (_driver == null)
				throw new NullReferenceException(nameof(_driver));

			_teams.Clear();
			(await _meta.TeamsGetAsync()).ToList().ForEach(t => _teams.Add(new UITeam(t)));
			_countries.Clear();
			(await _meta.CountriesGetAsync()).ToList().ForEach(c => _countries.Add(c));
			_tracks.Clear();
			(await _race.RacetracksGetAsync()).ToList().ForEach(rt => _tracks.Add(new UIRaceTrack(rt)));
			_drivers.Clear();
			(await _driver.DriversGetAsync()).ToList().ForEach(d=>_drivers.Add(d));	// TODO: checken, hier kommts zur ner JsonException!

			_teams.ToList().ForEach(t =>
			{
				t.Country = _countries.FirstOrDefault(c=>c.CountryId==t.TeamCountryId);
			});
			_tracks.ToList().ForEach(rt =>
			{
				rt.Country = _countries.FirstOrDefault(c => c.CountryId == rt.RacetrackCountryId);
			});
			_drivers.ToList().ForEach(d =>
			{
				d.DriverCountry = _countries.FirstOrDefault(c => c.CountryId == d.DriverCountryId);
			});
			RaisePropertyChangedEvent(nameof(Teams));
			RaisePropertyChangedEvent(nameof(Countries));
			RaisePropertyChangedEvent(nameof(Tracks));
			RaisePropertyChangedEvent(nameof(Drivers));
		}

		private async void RemoveFlagImageAsync()
		{
			if (_meta == null)  // escalate !!!
				throw new NullReferenceException(nameof(_meta));
			if (SelectedCountry == null)
				throw new NullReferenceException(nameof(SelectedCountry));
			SelectedCountry.CountryFlag = null;
			await _meta.CountriesPutAsync(SelectedCountry);
			await LoadListsAsync();
		}
		#endregion
	}
}