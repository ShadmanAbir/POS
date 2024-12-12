using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using UtopiaCatering.Data;
using UtopiaCatering.Enums;
using UtopiaCatering.Models;
using Microsoft.EntityFrameworkCore;

public class ItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Items.ToListAsync());
    }

    public IActionResult Create()
    {
        var itemTypes = Enum.GetValues(typeof(ItemType))
                    .Cast<ItemType>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.ToString().Replace("RawMaterials", "Raw Materials")
                                            .Replace("FinishedGoods", "Finished Goods")
                    })
                    .ToList();
        ViewData["ItemType"] = new SelectList(itemTypes, "Value", "Text");
        var RawMaterials = _context.Items.Where(i => i.ItemType == 1).ToList();
        ViewData["RawMaterials"] = new SelectList(RawMaterials, "ItemID", "ItemName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ItemID,ItemName,ItemType,Balance,ItemElements")] Items items)
    {
        if (ModelState.IsValid)
        {
            // Add the item
            _context.Add(items);
            await _context.SaveChangesAsync();

            // If the ItemType is "Finished Goods", save the ItemElements
            if (items.ItemType == (int)ItemType.FinishedGoods && items.ItemElements != null)
            {
                foreach (var itemElement in items.ItemElements)
                {
                    itemElement.ItemElementID = 0;
                    itemElement.ItemID = items.ItemID; // Associate elements with the item
                    _context.Add(itemElement);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        ViewData["ItemType"] = new SelectList(Enum.GetValues(typeof(ItemType)));
        return View(items);
    }
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var itemElements = await _context.Items
            .Include(i => i.ItemElements)
            .FirstOrDefaultAsync(m => m.ItemID == id);
        if (itemElements == null)
        {
            return NotFound();
        }

        return View(itemElements);
    }

    private bool ItemsExists(int id)
    {
        return _context.Items.Any(e => e.ItemID == id);
    }
}
