using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class FlightContext : IDB<Flight, int>
    {
        private FlightManagerDbContext context;

        public FlightContext()
        {
            context = new FlightManagerDbContext();
        }

        public async Task Create(Flight flight)
        {
            try
            {
                Plane planeFromDb = context.Planes.Find(flight.PlaneId);
                if (planeFromDb != null)
                {
                    flight.Plane = planeFromDb;
                }

                User userFromDb = context.Users.Find(flight.PilotId);
                if (userFromDb != null)
                {
                    flight.Pilot = userFromDb;
                }

                context.Flights.Add(flight);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Flight> Read(int key)
        {
            try
            {
                Flight flight = await context.Flights.FindAsync(key);
                if (flight == null)
                {
                    throw new ArgumentException("There is no flight with that id");
                }

                return flight;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Flight>> ReadAll()
        {
            try
            {
                return await context.Flights.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Flight flight)
        {
            try
            {
                Flight fromDb = await Read(flight.Id);
                context.Entry(fromDb).CurrentValues.SetValues(flight);
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
                Flight flight = await Read(key);
                context.Flights.Remove(flight);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
