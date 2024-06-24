using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KCSRSA.Data;
using KCSRSA.Models;
using Microsoft.AspNetCore.Authorization; // Added

namespace KCSRSA.Controllers
{
    [Authorize]
    public class MyCartsController : Controller
    {
        private readonly KCSRSAContext _context;

        public MyCartsController(KCSRSAContext context)
        {
            _context = context;
        }

        // GET: MyCarts
        public async Task<IActionResult> Index()
        {
            int currentUserId = GetCurrentUserId(); // Replace this with your actual method to get current user ID
            var cartItems = await _context.MyCart
                .Include(c => c.Product)
                .Where(c => c.UserId == currentUserId)
                .ToListAsync();

            return View(cartItems);
        }

        // POST: MyCarts/AddCart
        [HttpPost]
        public async Task<IActionResult> AddCart(int productId)
        {
            int currentUserId = GetCurrentUserId(); // Replace this with your actual method to get current user ID

            var cartItem = new MyCart
            {
                ProductId = productId,
                UserId = currentUserId
            };

            _context.MyCart.Add(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: MyCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myCart = await _context.MyCart
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myCart == null)
            {
                return NotFound();
            }

            return View(myCart);
        }

        // POST: MyCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myCart = await _context.MyCart.FindAsync(id);
            if (myCart != null)
            {
                _context.MyCart.Remove(myCart);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MyCarts/Checkout
        public async Task<IActionResult> Checkout()
        {
            // Here you can implement the logic to thank the customer and inform them about their order processing.
            ViewData["CheckoutMessage"] = "Thank you for your order! Your order is being processed.";

            // Implement logic to clear the user's cart after checkout.
            int currentUserId = GetCurrentUserId(); // Implement a method to get the current user ID from authentication
            await ClearCartForCurrentUser(currentUserId);

            return View();
        }

        // Method to clear the cart for the current user
        private async Task ClearCartForCurrentUser(int userId)
        {
            var cartItems = await _context.MyCart.Where(c => c.UserId == userId).ToListAsync();
            _context.MyCart.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        private int GetCurrentUserId()
        {
            // Replace this method with your actual implementation to get the current user's ID from authentication.
            // For example, if you're using ASP.NET Core Identity:
            // return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // For demonstration, returning a placeholder user ID.
            return 1;
        }
    }
}
