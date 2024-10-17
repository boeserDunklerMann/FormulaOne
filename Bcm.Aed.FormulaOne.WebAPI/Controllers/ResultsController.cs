using Bcm.Aed.FormulaOne.Model;
using Bcm.Aed.FormulaOne.WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bcm.Aed.FormulaOne.WebAPI.Controllers
{
	[ApiController]
	[Route("RaceResult")]
	/// <ChangeLog>
	/// <Create Datum="??.02.2024" Entwickler="AED" />
	/// </ChangeLog>
	public class ResultsController : FormulaOneController
	{
		private ILogger<ResultsController> _logger;
		public ResultsController(ILogger<ResultsController> logger) : base()
		{
			_logger = logger;
		}

		#region RaceResultType stuff
		[HttpPost]
		[Route(nameof(RaceResultType))]
		public async Task<IActionResult> CreateRaceResultTypeAsync(RaceResultType raceResultType)   // tested ok
		{
			if (_context != null)
			{
				_context.Add(raceResultType);
				await _context.SaveChangesAsync();
				return Ok();
			}
			throw new NullReferenceException(nameof(_context));
		}

		[HttpGet]
		[Route(nameof(RaceResultType))]
		public async Task<IEnumerable<RaceResultType>> GetRaceResultTypesAsync()    // tested ok
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.RaceResultTypes.ToListAsync();
		}

		[HttpGet]
		[Route($"{nameof(RaceResultType)}/{{raceResultTypeID}}")]
		public async Task<RaceResultType> GetRaceResultTypeAsync(int raceResultTypeID)  // tested ok
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceResultType? retval = await _context.RaceResultTypes.FirstOrDefaultAsync(rrt => rrt.RaceResultTypeId == raceResultTypeID);
			if (retval == null)
			{
				_logger.LogWarning($"RaceResultTypeID {raceResultTypeID} not found.");
				throw new ObjectNotFoundException(nameof(RaceResultType), raceResultTypeID);
			}
			return retval;
		}

		[HttpPut]
		[Route(nameof(RaceResultType))]
		public async Task<IActionResult> UpdateRaceResultTypeAsync(RaceResultType raceResultType)   // tested ok
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceResultType? rrtFromDB = await _context.RaceResultTypes.FirstOrDefaultAsync(rrt => rrt.RaceResultTypeId == raceResultType.RaceResultTypeId);
			if (rrtFromDB == null)
			{
				_logger.LogWarning($"RaceResultTypeID {raceResultType.RaceResultTypeId} not found.");
				throw new ObjectNotFoundException(nameof(RaceResultType), raceResultType.RaceResultTypeId);
			}
			rrtFromDB.RaceResultShort = raceResultType.RaceResultShort;
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		[Route($"{nameof(RaceResultType)}/{{raceResultTypeID}}")]
		public async Task<IActionResult> DeleteRaceResultTypeAsync(int raceResultTypeID)    // tested ok
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceResultType? rrtFromDB = await _context.RaceResultTypes.FirstOrDefaultAsync(rrt => rrt.RaceResultTypeId == raceResultTypeID);
			if (rrtFromDB == null)
			{
				_logger.LogWarning($"RaceResultTypeID {raceResultTypeID} not found.");
				throw new ObjectNotFoundException(nameof(RaceResultType), raceResultTypeID);
			}
			_context.RaceResultTypes.Remove(rrtFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion

		#region RaceResult stuff
		[HttpPost]
		public async Task<IActionResult> CreateRaceResultAsync(RaceResult raceResult)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			_context.Add(raceResult);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		public async Task<IEnumerable<RaceResult>> GetRaceResultsAsync()
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.RaceResults
				.Include(rr => rr.Race)
				.Include(rr => rr.RaceResultType)
				.ToListAsync();
		}

		[HttpGet]
		[Route("{raceID}")]
		public async Task<RaceResult> GetRaceResultAsync(int raceID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceResult? retval = await _context.RaceResults
				.Include(rr => rr.Race)
				.Include(rr => rr.RaceResultType)
				.FirstOrDefaultAsync(rr => rr.RaceId == raceID);
			if (retval == null)
			{
				_logger.LogWarning($"RaceID {raceID} in RaceResult not found.");
				throw new ObjectNotFoundException(nameof(RaceResult), raceID);
			}
			return retval;
		}
		[HttpPut]
		public async Task<IActionResult> UpdateRaceResultAsync(RaceResult raceResult)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceResult? rrFromDB = await _context.RaceResults.FirstOrDefaultAsync(rr => rr.RaceId == raceResult.RaceId);
			if (rrFromDB == null)
			{
				_logger.LogWarning($"RaceID {raceResult.RaceId} in RaceResult not found.");
				throw new ObjectNotFoundException(nameof(RaceResult), raceResult.RaceId);
			}
			rrFromDB.DurationMs = raceResult.DurationMs;
			rrFromDB.DistanceKm = raceResult.DistanceKm;
			rrFromDB.RaceResultType = raceResult.RaceResultType;
			rrFromDB.RaceResultTypeId = raceResult.RaceResultTypeId;
			await _context.SaveChangesAsync();
			return Ok(rrFromDB);
		}

		[HttpDelete]
		[Route("{raceID}")]
		public async Task<IActionResult> DeleteRaceResultAsync(int raceID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			RaceResult? rrFromDB = await _context.RaceResults.FirstOrDefaultAsync(rr => rr.RaceId == raceID);
			if (rrFromDB == null)
			{
				_logger.LogWarning($"RaceID {raceID} in RaceResult not found.");
				throw new ObjectNotFoundException(nameof(RaceResult), raceID);
			}
			_context.RaceResults.Remove(rrFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion

	}
}
