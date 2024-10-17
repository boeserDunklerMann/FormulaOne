using Bcm.Aed.FormulaOne.Model;
using Bcm.Aed.FormulaOne.WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bcm.Aed.FormulaOne.WebAPI.Controllers
{
	[ApiController]
	[Route("Races")]
	/// <ChangeLog>
	/// <Create Datum="??.02.2024" Entwickler="AED" />
	/// </ChangeLog>
	public class RaceController : FormulaOneController
	{
		private readonly ILogger<RaceController> logger;
		public RaceController(ILogger<RaceController> log)
		{
			logger = log;
		}

		#region Racetrack stuff
		// TODO: test CRUD for racetrack
		[HttpPost]
		[Route("Racetracks")]
		public async Task<IActionResult> CreateRacetrackAsync(Racetrack track)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			track.RacetrackCountry = null;
			_context.Add(track);
			await _context.SaveChangesAsync();
			return Ok(track);
		}

		[HttpGet]
		[Route("Racetracks")]
		public async Task<IEnumerable<Racetrack>> GetRacetracksAsync()
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.Racetracks.Include(rt => rt.RacetrackCountry).ToListAsync();
		}
		[HttpGet]
		[Route("Racetracks/{RacetrackID}")]
		public async Task<Racetrack> GetRacetrackAsync(int RacetrackID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Racetrack? retval = await _context.Racetracks
				.Include(rt => rt.RacetrackCountry)
				.FirstOrDefaultAsync(rt => rt.RacetrackId == RacetrackID);
			if (retval == null)
				throw new ObjectNotFoundException(nameof(Racetrack), RacetrackID);
			return retval;
		}

		[HttpPut]
		[Route("Racetracks")]
		public async Task<IActionResult> UpdateRacetrackAsync(Racetrack racetrack)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Racetrack? rtFromDB = await _context.Racetracks.FirstOrDefaultAsync(rt => rt.RacetrackId == racetrack.RacetrackId);
			if (rtFromDB == null)
				throw new ObjectNotFoundException(nameof(Racetrack), racetrack.RacetrackId);
			rtFromDB.RacetrackDistanceKm = racetrack.RacetrackDistanceKm;
			rtFromDB.RacetrackName = racetrack.RacetrackName;
			rtFromDB.RacetrackCountryId = racetrack.RacetrackCountryId;
			await _context.SaveChangesAsync();
			return Ok(rtFromDB);
		}

		[HttpDelete]
		[Route("Racetracks/{RacetrackID}")]
		public async Task<IActionResult> DeleteRacetrackAsync(int RacetrackID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Racetrack? rt2Delete = await _context.Racetracks
				.Include(rt => rt.RacetrackCountry)
				.FirstOrDefaultAsync(rt => rt.RacetrackId == RacetrackID);
			if (rt2Delete == null)
				throw new ObjectNotFoundException(nameof(Racetrack), RacetrackID);
			_context.Remove(rt2Delete);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion

		#region Race stuff
		// TODO: test race CRUD
		[HttpPost]
		public async Task<IActionResult> CreateRaceAsync(Race race)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			_context.Add(race);
			await _context.SaveChangesAsync();
			return Ok(race);
		}

		[HttpGet]
		public async Task<IEnumerable<Race>> GetRacesAsync()
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.Races
				.Include(r => r.Driver)
				.Include(r => r.Racetrack)
				.Include(r => r.RaceType)
				.ToListAsync();
		}
		[HttpGet]
		[Route("Races/{RaceID}")]
		public async Task<Race> GetRaceAsync(int RaceID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Race? retval = await _context.Races
				.Include(r => r.Driver)
				.Include(r => r.Racetrack)
				.Include(r => r.RaceType)
				.FirstOrDefaultAsync(r => r.RaceId == RaceID);
			if (retval == null)
				throw new ObjectNotFoundException(nameof(Race), RaceID);
			return retval;
		}

		[HttpPut]
		public async Task<IActionResult> UpdateRaceAsync(Race race)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Race? rFromDB = _context.Races.FirstOrDefault(r => r.RaceId == race.RaceId);
			if (rFromDB == null)
				throw new ObjectNotFoundException(nameof(Race), race.RaceId);
			rFromDB.RaceDate = race.RaceDate;
			rFromDB.Comment = race.Comment;
			await _context.SaveChangesAsync();
			return Ok(race);
		}

		[HttpDelete]
		[Route("Races/{RaceID}")]
		public async Task<IActionResult> DeleteRaceAsync(int RaceID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Race? rFromDB = _context.Races.FirstOrDefault(r => (r.RaceId == RaceID));
			if (rFromDB == null)
				throw new ObjectNotFoundException(nameof(Race), RaceID);
			_context.Races.Remove(rFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion

		#region RaceType stuff
		[HttpPost]
		[Route(nameof(RaceType))]
		public async Task<IActionResult> CreateRaceTypeAsync(RaceType raceType) //tested OK
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			_context.Add(raceType);
			await _context.SaveChangesAsync();
			return Ok(raceType);
		}

		[HttpGet]
		[Route(nameof(RaceType))]
		public async Task<IEnumerable<RaceType>> GetRaceTypesAsync()    //tested OK
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.RaceTypes.ToListAsync();
		}

		[HttpGet]
		[Route($"{nameof(RaceType)}/{{raceTypeID}}")]
		public async Task<RaceType> RaceTypeAsync(int raceTypeID)   // tested OK
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceType? retval = await _context.RaceTypes.FirstOrDefaultAsync(rtype => rtype.RaceTypeId == raceTypeID);
			if (retval == null)
				throw new ObjectNotFoundException(nameof(RaceType), raceTypeID);
			return retval;
		}

		[HttpPut]
		[Route($"{nameof(RaceType)}")]
		public async Task<IActionResult> UpdateRaceTypeAsync(RaceType raceType) //tested OK
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceType? rtFromDB = await _context.RaceTypes.FirstOrDefaultAsync(rtype => rtype.RaceTypeId == raceType.RaceTypeId);
			if (rtFromDB == null)
				throw new ObjectNotFoundException(nameof(RaceType), raceType.RaceTypeId);
			rtFromDB.RaceTypeName = raceType.RaceTypeName;
			rtFromDB.RaceTypeShort = raceType.RaceTypeShort;
			await _context.SaveChangesAsync();
			return Ok(raceType);
		}

		[HttpDelete]
		[Route($"{nameof(RaceType)}/{{raceTypeID}}")]
		public async Task<IActionResult> DeleteRaceTypeAsync(int raceTypeID)    //tested OK
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceType? rtFromDB = await _context.RaceTypes.FirstOrDefaultAsync(rtype => rtype.RaceTypeId == raceTypeID);
			if (rtFromDB == null)
				throw new ObjectNotFoundException(nameof(RaceType), raceTypeID);
			_context.RaceTypes.Remove(rtFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion
	}
}