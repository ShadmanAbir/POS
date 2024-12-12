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
    public class PurchaseOrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrderDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseOrderDetails.Include(p => p.PurchaseOrder);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderDetails = await _context.PurchaseOrderDetails
                .Include(p => p.PurchaseOrder)
                .FirstOrDefaultAsync(m => m.PoDetailsID == id);
            if (purchaseOrderDetails == null)
            {
                return NotFound();
            }

            return View(purchaseOrderDetails);
        }

        // GET: PurchaseOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["PoID"] = new SelectList(_context.PurchaseOrder, "PoID", "PoID");
            return View();
        }

        // POST: PurchaseOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoDetailsID,PoID,ItemID,Unit,Quantity,Rate")] PurchaseOrderDetails purchaseOrderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoID"] = new SelectList(_context.PurchaseOrder, "PoID", "PoID", purchaseOrderDetails.PoID);
            return View(purchaseOrderDetails);
        }

        // GET: PurchaseOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderDetails = await _context.PurchaseOrderDetails.FindAsync(id);
            if (purchaseOrderDetails == null)
            {
                return NotFound();
            }
            ViewData["PoID"] = new SelectList(_context.PurchaseOrder, "PoID", "PoID", purchaseOrderDetails.PoID);
            return View(purchaseOrderDetails);
        }

        // POST: PurchaseOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoDetailsID,PoID,ItemID,Unit,Quantity,Rate")] PurchaseOrderDetails purchaseOrderDetails)
        {
            if (id != purchaseOrderDetails.PoDetailsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderDetailsExists(purchaseOrderDetails.PoDetailsID))
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
            ViewData["PoID"] = new SelectList(_context.PurchaseOrder, "PoID", "PoID", purchaseOrderDetails.PoID);
            return View(purchaseOrderDetails);
        }

        // GET: PurchaseOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderDetails = await _context.PurchaseOrderDetails
                .Include(p => p.PurchaseOrder)
                .FirstOrDefaultAsync(m => m.PoDetailsID == id);
            if (purchaseOrderDetails == null)
            {
                return NotFound();
            }

            return View(purchaseOrderDetails);
        }

        // POST: PurchaseOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrderDetails = await _context.PurchaseOrderDetails.FindAsync(id);
            if (purchaseOrderDetails != null)
            {
                _context.PurchaseOrderDetails.Remove(purchaseOrderDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderDetailsExists(int id)
        {
            return _context.PurchaseOrderDetails.Any(e => e.PoDetailsID == id);
        }
    }
}
