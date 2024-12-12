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
    public class JournalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JournalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Journals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Journals.ToListAsync());
        }

        // GET: Journals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journals = await _context.Journals
                .FirstOrDefaultAsync(m => m.JournalID == id);
            if (journals == null)
            {
                return NotFound();
            }

            return View(journals);
        }

        // GET: Journals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JournalID,TransactionDate,Amount,Type,Category,Description,ItemID,Balance,IsDeleted,CreatedDate,CreatedBy")] Journals journals)
        {
            if (ModelState.IsValid)
            {
                _context.Add(journals);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(journals);
        }

        // GET: Journals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journals = await _context.Journals.FindAsync(id);
            if (journals == null)
            {
                return NotFound();
            }
            return View(journals);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JournalID,TransactionDate,Amount,Type,Category,Description,ItemID,Balance,IsDeleted,CreatedDate,CreatedBy")] Journals journals)
        {
            if (id != journals.JournalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalsExists(journals.JournalID))
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
            return View(journals);
        }

        // GET: Journals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journals = await _context.Journals
                .FirstOrDefaultAsync(m => m.JournalID == id);
            if (journals == null)
            {
                return NotFound();
            }

            return View(journals);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var journals = await _context.Journals.FindAsync(id);
            if (journals != null)
            {
                _context.Journals.Remove(journals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalsExists(int id)
        {
            return _context.Journals.Any(e => e.JournalID == id);
        }
    }
}
