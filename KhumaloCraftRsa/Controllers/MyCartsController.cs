using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KhumaloCraftRsa.Data;
using KhumaloCraftRsa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace KhumaloCraftRsa.Controllers
{
    [Authorize]
    public class MyCartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public MyCartsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: MyCarts
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.MyCarts
                .Include(c => c.Product)
                .ToListAsync();

            return View(cartItems);
        }

        // POST: MyCarts/AddCart
        [HttpPost]
        public async Task<IActionResult> AddCart(int productId)
        {
            // Fetch the product from the database using the productId
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound(); // Handle the case where the product is not found
            }

            var cartItem = new MyCart
            {
                ProductId = productId,
                
            };

            var orders = new Order
            {
                ProductID = productId,
                UserId = userManager.GetUserName(User),
                Quantity = 1,
                OrderDate = DateTime.Now,
                ProductName = product.Name,
                Approved = false


            };



            _context.Add(cartItem);
            _context.Add(orders);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: MyCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myCart = await _context.MyCarts
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.MyCartId == id);

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
            var myCart = await _context.MyCarts.FindAsync(id);
            if (myCart != null)
            {
                _context.MyCarts.Remove(myCart);
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
            await ClearCartForCurrentUser();

            return View();
        }

        // Method to clear the cart for the current user
        private async Task ClearCartForCurrentUser()
        {
            // Clear all items from the cart
            var cartItems = await _context.MyCarts.ToListAsync();
            _context.MyCarts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}