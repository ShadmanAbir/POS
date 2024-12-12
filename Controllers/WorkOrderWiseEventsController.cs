using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UtopiaCatering.Data;
using UtopiaCatering.Models;

namespace UtopiaCatering.Controllers
{
    public class WorkOrderWiseEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkOrderWiseEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkOrderWiseEvents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkOrderWiseEvents.Include(w => w.WorkOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WorkOrderWiseEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderWiseEvents = await _context.WorkOrderWiseEvents
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderWiseEventsID == id);
            if (workOrderWiseEvents == null)
            {
                return NotFound();
            }

            return View(workOrderWiseEvents);
        }

        // GET: WorkOrderWiseEvents/Create
        public IActionResult Create()
        {
            ViewData["WorkOrderID"] = new SelectList(_context.WorkOrder, "WoID", "WoID");
            return View();
        }

        // POST: WorkOrderWiseEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkOrderWiseEventsID,EventID,WorkOrderID,IsDeleted,CreatedDate,CreatedBy")] WorkOrderWiseEvents workOrderWiseEvents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workOrderWiseEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkOrderID"] = new SelectList(_context.WorkOrder, "WoID", "WoID", workOrderWiseEvents.WorkOrderID);
            return View(workOrderWiseEvents);
        }

        // GET: WorkOrderWiseEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderWiseEvents = await _context.WorkOrderWiseEvents.FindAsync(id);
            if (workOrderWiseEvents == null)
            {
                return NotFound();
            }
            ViewData["WorkOrderID"] = new SelectList(_context.WorkOrder, "WoID", "WoID", workOrderWiseEvents.WorkOrderID);
            return View(workOrderWiseEvents);
        }

        // POST: WorkOrderWiseEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderWiseEventsID,EventID,WorkOrderID,IsDeleted,CreatedDate,CreatedBy")] WorkOrderWiseEvents workOrderWiseEvents)
        {
            if (id != workOrderWiseEvents.WorkOrderWiseEventsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrderWiseEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderWiseEventsExists(workOrderWiseEvents.WorkOrderWiseEventsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkOrderID"] = new SelectList(_context.WorkOrder, "WoID", "WoID", workOrderWiseEvents.WorkOrderID);
            return View(workOrderWiseEvents);
        }

        // GET: WorkOrderWiseEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderWiseEvents = await _context.WorkOrderWiseEvents
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderWiseEventsID == id);
            if (workOrderWiseEvents == null)
            {
                return NotFound();
            }

            return View(workOrderWiseEvents);
        }

        // POST: WorkOrderWiseEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrderWiseEvents = await _context.WorkOrderWiseEvents.FindAsync(id);
            if (workOrderWiseEvents != null)
            {
                _context.WorkOrderWiseEvents.Remove(workOrderWiseEvents);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderWiseEventsExists(int id)
        {
            return _context.WorkOrderWiseEvents.Any(e => e.WorkOrderWiseEventsID == id);
        }
    }
}
