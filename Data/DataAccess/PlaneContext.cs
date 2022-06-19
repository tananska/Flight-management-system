using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class PlaneContext : IDB<Plane, int>
    {
        private FlightManagerDbContext context;

        public PlaneContext()
        {
            context = new FlightManagerDbContext();
        }

        public async Task Create(Plane plane)
        {
            try
            {
                context.Planes.Add(plane);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Plane> Read(int key)
        {
            try
            {
                Plane plane = await context.Planes.FindAsync(key);
                if (plane == null)
                {
                    throw new ArgumentException("There is no plane with that id");
                }

                return plane;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Plane>> ReadAll()
        {
            try
            {
                return await context.Planes.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Plane plane)
        {
            try
            {
                Plane fromDb = await Read(plane.Id);
                context.Entry(fromDb).CurrentValues.SetValues(plane);
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
                Plane plane = await Read(key);
                context.Planes.Remove(plane);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
