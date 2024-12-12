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
    public class WorkOrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkOrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkOrderDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkOrderDetails.Include(w => w.WorkOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WorkOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderDetails = await _context.WorkOrderDetails
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderDetailsID == id);
            if (workOrderDetails == null)
            {
                return NotFound();
            }

            return View(workOrderDetails);
        }

        // GET: WorkOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["WoID"] = new SelectList(_context.WorkOrder, "WoID", "WoID");
            return View();
        }

        // POST: WorkOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkOrderDetailsID,WoID,ItemID,Quantity,IsDeleted,CreatedDate,CreatedBy")] WorkOrderDetails workOrderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workOrderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WoID"] = new SelectList(_context.WorkOrder, "WoID", "WoID", workOrderDetails.WoID);
            return View(workOrderDetails);
        }

        // GET: WorkOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderDetails = await _context.WorkOrderDetails.FindAsync(id);
            if (workOrderDetails == null)
            {
                return NotFound();
            }
            ViewData["WoID"] = new SelectList(_context.WorkOrder, "WoID", "WoID", workOrderDetails.WoID);
            return View(workOrderDetails);
        }

        // POST: WorkOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkOrderDetailsID,WoID,ItemID,Quantity,IsDeleted,CreatedDate,CreatedBy")] WorkOrderDetails workOrderDetails)
        {
            if (id != workOrderDetails.WorkOrderDetailsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderDetailsExists(workOrderDetails.WorkOrderDetailsID))
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
            ViewData["WoID"] = new SelectList(_context.WorkOrder, "WoID", "WoID", workOrderDetails.WoID);
            return View(workOrderDetails);
        }

        // GET: WorkOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrderDetails = await _context.WorkOrderDetails
                .Include(w => w.WorkOrder)
                .FirstOrDefaultAsync(m => m.WorkOrderDetailsID == id);
            if (workOrderDetails == null)
            {
                return NotFound();
            }

            return View(workOrderDetails);
        }

        // POST: WorkOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrderDetails = await _context.WorkOrderDetails.FindAsync(id);
            if (workOrderDetails != null)
            {
                _context.WorkOrderDetails.Remove(workOrderDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderDetailsExists(int id)
        {
            return _context.WorkOrderDetails.Any(e => e.WorkOrderDetailsID == id);
        }
    }
}
