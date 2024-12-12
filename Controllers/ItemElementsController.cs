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
    public class ItemElementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemElementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemElements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemElements.Include(i => i.Items);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItemElements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemElements = await _context.ItemElements
                .Include(i => i.Items)
                .FirstOrDefaultAsync(m => m.ItemElementID == id);
            if (itemElements == null)
            {
                return NotFound();
            }

            return View(itemElements);
        }

        // GET: ItemElements/Create
        public IActionResult Create()
        {
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "ItemID");
            return View();
        }

        // POST: ItemElements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemElementID,ItemID,ElementID,Quantity,Unit,IsDeleted,CreatedDate,CreatedBy")] ItemElements itemElements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemElements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "ItemID", itemElements.ItemID);
            return View(itemElements);
        }

        // GET: ItemElements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemElements = await _context.ItemElements.FindAsync(id);
            if (itemElements == null)
            {
                return NotFound();
            }
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "ItemID", itemElements.ItemID);
            return View(itemElements);
        }

        // POST: ItemElements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemElementID,ItemID,ElementID,Quantity,Unit,IsDeleted,CreatedDate,CreatedBy")] ItemElements itemElements)
        {
            if (id != itemElements.ItemElementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemElements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemElementsExists(itemElements.ItemElementID))
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
            ViewData["ItemID"] = new SelectList(_context.Items, "ItemID", "ItemID", itemElements.ItemID);
            return View(itemElements);
        }

        // GET: ItemElements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemElements = await _context.ItemElements
                .Include(i => i.Items)
                .FirstOrDefaultAsync(m => m.ItemElementID == id);
            if (itemElements == null)
            {
                return NotFound();
            }

            return View(itemElements);
        }

        // POST: ItemElements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemElements = await _context.ItemElements.FindAsync(id);
            if (itemElements != null)
            {
                _context.ItemElements.Remove(itemElements);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemElementsExists(int id)
        {
            return _context.ItemElements.Any(e => e.ItemElementID == id);
        }
    }
}
