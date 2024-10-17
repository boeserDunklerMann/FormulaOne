using Bcm.Aed.FormulaOne.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bcm.Aed.FormulaOne.WebAPI.Controllers
{
	[ApiController]
	[Route("Drivers")]
	/// <ChangeLog>
	/// <Create Datum="01.02.2024" Entwickler="AED" />
	/// <Change Datum="12.02.2024" Entwickler="AED">warning disables removed 
	/// </Change>	
	/// </ChangeLog>
	public class DriverController : ControllerBase, IDisposable
	{
		private readonly ILogger<DriverController> _logger;
		private BcmAedFormulaOneContext? _context;
		public DriverController(ILogger<DriverController> logger)
		{
			_logger = logger;
			_context = new BcmAedFormulaOneContext();
		}

		public void Dispose()
		{
			if (_context != null)
			{
				_context.Dispose();
				_context = null;
			}
		}

		#region Drivers-CRUD
		[HttpPost]
		public async Task<IActionResult> CreateDriverAsync(Driver driver)
		{
			driver.DriverId = 0;    // assigned by SQL server
			/* I assume that DriverCountryId and DriverTeamId is properly set.
			 * else this will raise:
			 * .SqlException (0x80131904): Ein expliziter Wert für die Identitätsspalte kann nicht in der Team-Tabelle eingefügt werden, wenn IDENTITY_INSERT auf OFF festgelegt ist.
			*/
			driver.DriverCountry = null;
			driver.DriverTeam = null;
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			_context.Add(driver);
			await _context.SaveChangesAsync();
			return Ok(driver);
		}

		[HttpGet()]
		public async Task<IEnumerable<Driver>> GetDriverAsync()
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.Drivers
					.Include(d => d.DriverTeam)
					.Include(d => d.DriverCountry)
					.ToListAsync();
		}
		[HttpGet()]
		[Route("{driverID}")]
		public async Task<Driver> GetDriverAsync(int driverID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Driver? retval = await _context.Drivers
					.Include(d => d.DriverTeam)
					.Include(d => d.DriverCountry)
					.FirstOrDefaultAsync(d => d.DriverId == driverID);
			if (retval == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Driver), driverID);
			return retval;
		}

		[HttpPut()]
		public async Task<IActionResult> UpdateDriverAsync(Driver driver)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Driver? dFromDB = await _context.Drivers.FirstOrDefaultAsync(d => d.DriverId == driver.DriverId);
			if (dFromDB == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Driver), driver.DriverId);
			dFromDB.DriverName = driver.DriverName;
			dFromDB.DateOfBirth = driver.DateOfBirth;
			dFromDB.DriverTeamId = driver.DriverTeamId;
			await _context.SaveChangesAsync();
			return Ok(dFromDB);
		}

		[HttpDelete()]
		[Route("{driverID}")]
		public async Task<IActionResult> DeleteDriverAsync(int driverID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Driver? dFromDB = await _context.Drivers.FirstOrDefaultAsync(d => d.DriverId == driverID);
			if (dFromDB == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Driver), driverID);
			_context.Drivers.Remove(dFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion

		#region Vehicles-CRUD
		[HttpPost]
		[Route(nameof(Vehicle))]
		public async Task<IActionResult> CreateVehicleAsync(Vehicle vehicle)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			vehicle.VehicleId = 0;  // will be assigned by SQL Server
			_context.Vehicles.Add(vehicle);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		[Route(nameof(Vehicle))]
		public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			return await _context.Vehicles.ToListAsync();
		}

		[HttpGet]
		[Route($"{nameof(Vehicle)}/{{vehicleID}}")]
		public async Task<Vehicle> GetVehicleAsync(int vehicleID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Vehicle? retval = await _context.Vehicles
				.FirstOrDefaultAsync(v => v.VehicleId == vehicleID);
			if (retval == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Vehicle), vehicleID);
			return retval;
		}

		[HttpPut]
		[Route($"{nameof(Vehicle)}")]
		public async Task<IActionResult> UpdateVehicleAsync(Vehicle vehicle)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Vehicle? vFromDB = await _context.Vehicles
				.FirstOrDefaultAsync(v => v.VehicleId == vehicle.VehicleId);
			if (vFromDB == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Vehicle), vehicle.VehicleId);
			vFromDB.VehicleName = vehicle.VehicleName;
			vFromDB.VehiclePhoto = vehicle.VehiclePhoto;
			await _context.SaveChangesAsync();
			return Ok(vFromDB);
		}
		[HttpDelete]
		[Route($"{nameof(Vehicle)}/{{vehicleID}}")]
		public async Task<IActionResult> DeleteVehicleAsync(int vehicleID)
		{
			if (_context == null)
				throw new NullReferenceException(nameof(_context));
			Vehicle? vFromDB = await _context.Vehicles
				.FirstOrDefaultAsync(v => v.VehicleId == vehicleID);
			if (vFromDB == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Vehicle), vehicleID);
			_context.Vehicles.Remove(vFromDB);
			await _context.SaveChangesAsync();
			return Ok();
		}
		#endregion
	}
}
