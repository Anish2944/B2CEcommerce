using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using B2CEcommerceApp.Data;
using B2CEcommerceApp.Models;

[Authorize]
public class WishlistController : Controller
{
    private readonly AppDbContext _context;

    public WishlistController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var items = await _context.WishlistItems
            .Include(w => w.Product)
            .Where(w => w.UserId == userId)
            .ToListAsync();

        return View(items);
    }

    [HttpPost]
    public async Task<IActionResult> Add(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!_context.WishlistItems.Any(w => w.ProductId == productId && w.UserId == userId))
        {
            _context.WishlistItems.Add(new WishlistItem { ProductId = productId, UserId = userId });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Wishlist");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var item = await _context.WishlistItems.FindAsync(id);
        if (item != null)
        {
            _context.WishlistItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
