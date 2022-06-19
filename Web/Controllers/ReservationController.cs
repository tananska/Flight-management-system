using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.DataAccess;
using System.Net.Mail;
using System.Net;

namespace Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationContext context;
        private readonly FlightContext flightContext;
        private readonly FlightManagerDbContext flightManagerDbContext;
        public ReservationController()
        {
            context = new ReservationContext();
            flightContext = new FlightContext();
            flightManagerDbContext = new FlightManagerDbContext();
        }

        // GET: Reservation
        
        public ViewResult Index(string searchString)
        {
            var reservations = from r in flightManagerDbContext.Reservations
                           select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(r => r.Email.Contains(searchString));
            }

            return View(reservations.ToList());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await context.Read((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public async Task<IActionResult> Create()
        {
            await LoadFlights();
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,EGN,Phone,Email,Flight,FlightId,Nationality,TicketType")] Reservation reservation)
        {
            
            if (ModelState.IsValid)
            {
                reservation.Flight = await flightContext.Read(reservation.FlightId);
                await context.Create(reservation);

                reservation.ReserveSeat();
                await flightContext.Update(reservation.Flight);

                SendEmail(reservation);
                return RedirectToAction(nameof(Index));
            }
            await LoadFlights();
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await context.Read((int)id);
            if (reservation == null)
            {
                return NotFound();
            }
            await LoadFlights();
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,EGN,Phone,Email,Nationality,Flight,FlightId,TicketType")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await context.Create(reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await context.Read((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadFlights() => ViewData["Flights"] = await flightContext.ReadAll();

        private static void SendEmail(Reservation reservation)
        {
            MailAddress flightmanagerEmail = new MailAddress("flightmanager.itkariera@gmail.com");

            SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587);
            smpt.UseDefaultCredentials = false;
            smpt.Credentials = new NetworkCredential(flightmanagerEmail.Address, "flightManager2022");
            smpt.EnableSsl = true;

            MailMessage message = new MailMessage(flightmanagerEmail.Address, reservation.Email);
            message.Subject = "Reservation confirmation";
            string messegeBody = $@"<html>
<body>
<p>Hi {reservation.FirstName},</p>
<p>We're writing to inform you that the reservation you made for {reservation.Flight} is ready. You chose a {reservation.TicketType} class seat for {reservation.Price()}.</p>
<p>There are {reservation.Flight.AvailableBusinessSeats} available business class seats and {reservation.Flight.AvailableEconomySeats} available economy class seats if you need to purchase another ticket.</p>
<p>Have a nice flight!</p>
<p>Team of FlightManager</p>
</body>
</html>";
            message.IsBodyHtml = true;
            message.Body = messegeBody;
            smpt.Send(message);
        }
    }
}
