using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using B2CEcommerceApp.Data;
using B2CEcommerceApp.Models;

[Authorize]
public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Cart
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.CartItems
            .Where(c => c.UserId == userId)
            .Include(c => c.Product)
            .ToListAsync();

        return View(cartItems);
    }

    // POST: /Cart/Add
    [HttpPost]
    public async Task<IActionResult> Add(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        if (cartItem == null)
        {
            cartItem = new CartItem { ProductId = productId, UserId = userId!, Quantity = 1 };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // POST: /Cart/Remove
    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
