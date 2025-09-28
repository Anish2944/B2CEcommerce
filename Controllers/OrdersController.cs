using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using B2CEcommerceApp.Data;
using B2CEcommerceApp.Models;

[Authorize]
public class OrdersController : Controller
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    // CUSTOMER: List my orders
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orders = await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .ToListAsync();

        return View(orders);
    }

    // ADMIN: View all orders
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Manage()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .ToListAsync();

        return View(orders);
    }

    // ADMIN: Mark an order as shipped
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkShipped(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        // Add a status field to Order if not already present
        order.Status = "Shipped";
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Manage));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, string status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        order.Status = status;
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Manage));
    }

    // GET: Checkout
    public async Task<IActionResult> Checkout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var cartItems = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        ViewBag.CartItems = cartItems;

        return View(new Order());
    }

    // POST: Checkout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(Order order)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Fetch cart items
        var cartItems = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();

        if (!cartItems.Any())
        {
            ModelState.AddModelError("", "Your cart is empty!");
            ViewBag.CartItems = cartItems;
            return View(order);
        }

        if (!ModelState.IsValid)
        {
            ViewBag.CartItems = cartItems;
            return View(order);
        }

        // Create new order
        order.UserId = userId!;
        order.OrderDate = DateTime.Now;
        order.TotalAmount = cartItems.Sum(c => (c.Product?.Price ?? 0M) * c.Quantity);

        order.OrderItems = cartItems.Select(c => new OrderItem
        {
            ProductId = c.ProductId,
            Quantity = c.Quantity,
            UnitPrice = c.Product?.Price ?? 0M
        }).ToList();

        _context.Orders.Add(order);

        // Clear cart
        _context.CartItems.RemoveRange(cartItems);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Confirmation), new { id = order.Id });
    }

    // GET: Order Confirmation
    public async Task<IActionResult> Confirmation(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        return View(order);
    }
}
