using Ecommerce.Application.Services;
using Ecommerce.Dtos.Book;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecommerc.MVC.Controllers
{
    public class CartController : Controller { 
        private readonly IBookService _bookService;

        public CartController(IBookService bookService, IAuthorServices authorService)
        {
            _bookService = bookService;
        }
    
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddToCart(int productId)
        {
            var value = HttpContext.Session.GetString("Cart");
            Cart cart;
            if (value == null)
            {
                cart = new Cart();
            }
            else
            {
                cart = JsonSerializer.Deserialize<Cart>(value) ?? new Cart();
            }


            cart.booksids.Add(productId);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));

            return RedirectToAction("Index", "Book");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Cart()
        {
            var value = HttpContext.Session.GetString("Cart");
            Cart cart;
            if (value == null)
            {
                cart = new Cart();
            }
            else
            {
                cart = JsonSerializer.Deserialize<Cart>(value) ?? new Cart();
            }

            var booksInCart = new List<BookDetailsViewModel>();

            foreach (var bookId in cart.booksids)
            {
                var book = await _bookService.GetOne(bookId);
                if (book != null)
                {
                    booksInCart.Add(book);
                }
            }

            return View(booksInCart);
        }
    }
}
