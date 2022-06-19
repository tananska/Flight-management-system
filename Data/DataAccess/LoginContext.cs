using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class LoginContext
    {
        private FlightManagerDbContext context;

        public LoginContext()
        {
            context = new FlightManagerDbContext();
        }

        public async Task<User> Login(string username, string password) 
        {
            User user = await context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
            return user;
        }
    }
}
