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
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseOrder.Include(p => p.Vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .Include(p => p.Vendor)
                .Include(p => p.PurchaseOrderDetails)
                .FirstOrDefaultAsync(m => m.PoID == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public IActionResult Create()
        {
            ViewData["VendorID"] = new SelectList(_context.Vendor, "VendorID", "VendorName");
            ViewData["ItemID"] = new SelectList(_context.Items.Where(a => a.ItemType == 1), "ItemID", "ItemName");
            return View();
        }

        // POST: PurchaseOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {

                foreach (var item in purchaseOrder.PurchaseOrderDetails)
                {
                    item.PoDetailsID = 0;
                }
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();

/*                foreach (var detail in purchaseOrderDetails)
                {
                    detail.PoID = purchaseOrder.PoID;
                    _context.PurchaseOrderDetails.Add(detail);
                }

                await _context.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }

            ViewData["VendorID"] = new SelectList(_context.Vendor, "VendorID", "VendorName", purchaseOrder.VendorID);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(a => a.ItemType == 1), "ItemID", "ItemName");
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .Include(p => p.PurchaseOrderDetails)
                .FirstOrDefaultAsync(p => p.PoID == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            ViewData["VendorID"] = new SelectList(_context.Vendor, "VendorID", "VendorName", purchaseOrder.VendorID);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(a => a.ItemType == 1), "ItemID", "ItemName");
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseOrder purchaseOrder, List<PurchaseOrderDetails> purchaseOrderDetails)
        {
            if (id != purchaseOrder.PoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);

                    var existingDetails = _context.PurchaseOrderDetails.Where(d => d.PoID == id).ToList();
                    _context.PurchaseOrderDetails.RemoveRange(existingDetails);

                    foreach (var detail in purchaseOrderDetails)
                    {
                        detail.PoID = id;
                        _context.PurchaseOrderDetails.Add(detail);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.PoID))
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

            ViewData["VendorID"] = new SelectList(_context.Vendor, "VendorID", "VendorName", purchaseOrder.VendorID);
            ViewData["ItemID"] = new SelectList(_context.Items.Where(a => a.ItemType == 1), "ItemID", "ItemName");
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrder = await _context.PurchaseOrder
                .Include(p => p.PurchaseOrderDetails)
                .FirstOrDefaultAsync(p => p.PoID == id);

            if (purchaseOrder != null)
            {
                _context.PurchaseOrderDetails.RemoveRange(purchaseOrder.PurchaseOrderDetails);
                _context.PurchaseOrder.Remove(purchaseOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrder.Any(e => e.PoID == id);
        }
    }
}
