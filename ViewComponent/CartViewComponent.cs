using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using B2CEcommerceApp.Data;

namespace B2CEcommerceApp.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CartViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                ViewBag.Count = 0;
                return View();
            }

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            var count = await _context.CartItems
                .Where(c => c.UserId == userId)
                .SumAsync(c => c.Quantity);

            ViewBag.Count = count;
            return View();
        }
    }
}
