using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.DataAccess;

namespace Web.Controllers
{
    public class PlaneController : Controller
    {
        private readonly PlaneContext context;

        public PlaneController()
        {
            this.context = new PlaneContext();
        }

        // GET: Plane
        public async Task<IActionResult> Index()
        {
            return View(await context.ReadAll());
        }

        // GET: Plane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await context.Read((int)id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        // GET: Plane/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaneType,PassengerCapacity,BusinessCapacity")] Plane plane)
        {
            if (ModelState.IsValid)
            {
                await context.Create(plane);
                return RedirectToAction(nameof(Index));
            }
            return View(plane);
        }

        // GET: Plane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await context.Read((int)id);
            if (plane == null)
            {
                return NotFound();
            }
            return View(plane);
        }

        // POST: Plane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlaneType,PassengerCapacity,BusinessCapacity")] Plane plane)
        {
            if (id != plane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await context.Update(plane);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plane);
        }

        // GET: Plane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await context.Read((int)id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        // POST: Plane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await context.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
