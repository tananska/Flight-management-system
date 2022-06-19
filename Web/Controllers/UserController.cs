using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.DataAccess;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext context;
        private readonly FlightManagerDbContext flightManagerDbContext;

        public UserController()
        {
            this.context = new UserContext();
            this.flightManagerDbContext = new FlightManagerDbContext();
        }

        // GET: User
        
        public ViewResult Index(string searchText, string selectOption)
        {
            var users = from u in flightManagerDbContext.Users
                               select u;
            if (!String.IsNullOrEmpty(searchText))
            {
                switch (selectOption)
                {
                    case "Email":
                        users = users.Where(u =>
    u.Email.ToLower().Contains(searchText.ToLower()));
                        break;

                    case "Username":
                        users = users.Where(u =>
    u.Username.ToLower().Contains(searchText.ToLower()));
                        break;

                    case "Firstname":
                        users = users.Where(u =>
    u.FirstName.ToLower().Contains(searchText.ToLower()));
                        break;
                    case "Lastname":
                        users = users.Where(u =>
    u.LastName.ToLower().Contains(searchText.ToLower())) ;
                        break;

                }
            }
            

            return View(users.ToList());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await context.Read((int)id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Email,FirstName,LastName,EGN,Address,Phone,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                await context.Create(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await context.Read((int)id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email,FirstName,LastName,EGN,Address,Phone,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await context.Read((int)id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
