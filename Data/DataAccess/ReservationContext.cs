using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class ReservationContext : IDB<Reservation, int>
    {
        private FlightManagerDbContext context;

        public ReservationContext()
        {
            context = new FlightManagerDbContext();
        }

        public async Task Create(Reservation reservation)
        {
            try
            {
                Flight flightFromDb = context.Flights.Find(reservation.FlightId);
                if (flightFromDb != null)
                {
                    reservation.Flight = flightFromDb;
                }

                context.Reservations.Add(reservation);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Reservation> Read(int key)
        {
            try
            {
                Reservation reservation = await context.Reservations.FindAsync(key);
                if (reservation == null)
                {
                    throw new ArgumentException("There is no reservation with that id");
                }

                return reservation;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Reservation>> ReadAll()
        {
            try
            {
                return await context.Reservations.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Reservation reservation)
        {
            try
            {
                Reservation fromDb = await Read(reservation.Id);
                context.Entry(fromDb).CurrentValues.SetValues(reservation);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int key)
        {
            try
            {
                Reservation reservation = await Read(key);
                context.Reservations.Remove(reservation);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
