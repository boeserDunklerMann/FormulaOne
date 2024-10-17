using Bcm.Aed.FormulaOne.Model;
using Bcm.Aed.FormulaOne.WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bcm.Aed.FormulaOne.WebAPI.Controllers
{
	[ApiController]
	[Route("Metadata")]
	/// <ChangeLog>
	/// <Create Datum="??.02.2024" Entwickler="AED" />
	/// <Change Datum="12.02.2024" Entwickler="AED">FormulaOneController used as base class</Change>	
	/// <Change Datum="12.02.2024" Entwickler="AED">#pragma warning disables removed</Change>	
		/// </ChangeLog>
	public class MetaController : FormulaOneController
	{
		// DONE: refactor with private DBContext
		#region Country CRUD ops
		[HttpGet]
		[Route("Countries")]
		public async Task<IEnumerable<Country>> GetCountriesAsync()
		{
			if (_context==null)
				throw new NullReferenceException(nameof(_context));
			return await _context.Countries.ToListAsync();
		}

		[HttpGet]
		[Route("Country/{countryId}")]
		public async Task<Country> ReadCountryAsync(int countryId)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Country? retval = await _context.Countries.FirstOrDefaultAsync(c => c.CountryId == countryId);
				if (retval == null)
					throw new ObjectNotFoundException(nameof(Country), countryId);
				return retval;
		}

		[HttpPost]
		[Route("Countries")]
		public async Task CreateCountryAsync(Country country)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			_context.Add(country);
			await _context.SaveChangesAsync();
		}

		[HttpPut]
		[Route("Countries")]
		public async Task UpdateCountryAsync(Country country)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Country? ctryFromDB = await _context.Countries
				.FirstOrDefaultAsync(c => c.CountryId == country.CountryId);
			if (ctryFromDB == null)
				throw new ObjectNotFoundException(nameof(Country), country.CountryId);
			ctryFromDB.CountryName = country.CountryName;
			ctryFromDB.CountryFlag = country.CountryFlag;
			await _context.SaveChangesAsync();
		}

		[HttpDelete]
		[Route("Countries")]
		public async Task DeleteCountryAsync(Country country)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Country? ctryFromDB = await _context.Countries
				.FirstOrDefaultAsync(c => c.CountryId == country.CountryId);
			if (ctryFromDB == null)
				throw new ObjectNotFoundException(nameof(Country), country.CountryId);
			_context.Remove(ctryFromDB);
			await _context.SaveChangesAsync();
		}
		#endregion

		#region Teams CRUD ops
		[HttpPost]
		[Route("Teams")]
		public async Task CreateTeamAsync(Team team)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			_context.Add(team);
			await _context.SaveChangesAsync();
		}

		[HttpGet]
		[Route("Teams")]
		public async Task<IEnumerable<Team>> GetTeamsAsync()
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.Teams.Include(t => t.TeamCountry).ToListAsync();
		}

		[HttpGet]
		[Route("Teams/{teamID}")]
		public async Task<Team> GetTeamsAsync(int teamID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Team? retval = await _context.Teams.Include(t => t.TeamCountry)
				.FirstOrDefaultAsync(t => t.TeamId == teamID);
            if (retval==null)
				throw new ObjectNotFoundException(nameof(Team), teamID);
            return retval;
		}

		[HttpPut]
		[Route("Teams")]
		public async Task<IActionResult> UpdateTeamAsync(Team team)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Team? tFromDB = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == team.TeamId);
			if (tFromDB == null)
				throw new ObjectNotFoundException(nameof(Team), team.TeamId);
			tFromDB.TeamName = team.TeamName;
			tFromDB.TeamCountry = team.TeamCountry;
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		[Route("Teams/{teamID}")]
		public async Task<IActionResult> DeleteTeamAsync(int teamID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Team? tFromDB = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamID);
			if (tFromDB == null)
				throw new ObjectNotFoundException(nameof(Team), teamID);
			_context.Remove(tFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion
	}
}