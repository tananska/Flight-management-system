using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class UserContext : IDB<User, int>
    {
        private FlightManagerDbContext context;

        public UserContext()
        {
            context = new FlightManagerDbContext();
        }

        public async Task Create(User user)
        {
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> Read(int key)
        {
            try
            {
                User user = await context.Users.FindAsync(key);
                if (user == null)
                {
                    throw new ArgumentException("There is no user with that id");
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> ReadAll()
        {
            try
            {
                return await context.Users.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(User user)
        {
            try
            {
                User fromDb = await Read(user.Id);
                context.Entry(fromDb).CurrentValues.SetValues(user);
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
                User user = await Read(key);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
