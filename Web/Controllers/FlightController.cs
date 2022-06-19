using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.DataAccess;

namespace Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightContext context;
        private readonly UserContext userContext;
        private readonly PlaneContext planeContext;

        public FlightController()
        {
            context = new FlightContext();
            userContext = new UserContext();
            planeContext = new PlaneContext();
        }

        // GET: Flight
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAll());
        }

        // GET: Flight/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var flight = await context.Read((int)id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flight/Create
        public async Task<IActionResult> Create()
        {
            await LoadUsers();
            await LoadPlanes();
            return View();
        }

        // POST: Flight/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,To,Departure,Arrival,Plane,PlaneId,Pilot,PilotId,BusinessPrice,EconomyPrice,AvailableBusinessSeats,AvailableEconomySeats")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                flight.Plane = await planeContext.Read(flight.PlaneId);
                flight.Pilot = await userContext.Read(flight.PilotId);
                flight.AvailableBusinessSeats = flight.Plane.BusinessCapacity;
                flight.AvailableEconomySeats = flight.Plane.PassengerCapacity - flight.Plane.BusinessCapacity;

                await context.Create(flight);
                return RedirectToAction(nameof(Index));
            }
            await LoadUsers();
            await LoadPlanes();
            return View(flight);
        }

        // GET: Flight/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await context.Read((int)id);
            if (flight == null)
            {
                return NotFound();
            }
            await LoadUsers();
            await LoadPlanes();
            return View(flight);
        }

        // POST: Flight/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,From,To,Departure,Arrival,Plane,PlaneId,Pilot,PilotId,BusinessPrice,EconomyPrice,AvailableBusinessSeats,AvailableEconomySeats")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                flight.Plane = await planeContext.Read(flight.PlaneId);
                flight.Pilot = await userContext.Read(flight.PilotId);
                flight.AvailableBusinessSeats = flight.Plane.BusinessCapacity;
                flight.AvailableEconomySeats = flight.Plane.PassengerCapacity - flight.Plane.BusinessCapacity;

                await context.Update(flight);
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flight/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await context.Read((int)id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadUsers() => ViewData["Users"] = await userContext.ReadAll();
        private async Task LoadPlanes() => ViewData["Planes"] = await planeContext.ReadAll();
    }
}
