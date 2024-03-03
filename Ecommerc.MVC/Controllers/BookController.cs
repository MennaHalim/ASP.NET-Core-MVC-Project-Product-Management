using Microsoft.AspNetCore.Mvc;
using Ecommerce.Application.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ecommerce.Dtos.Author;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Dtos.Book;

namespace Ecommerc.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorServices _AuthorService;

        public BookController(IBookService bookService, IAuthorServices authorService)
        {
            _bookService = bookService;
            _AuthorService = authorService;
        }

        // GET: Book
        [Authorize]
        public async Task<IActionResult> Index(int items = 10, int pagenumber = 1)
        {
            var result = await _bookService.GetAllPagination(items, pagenumber);

            if (User.IsInRole("Admin"))
            {
                return View("AdminHomePage", result.Entities);
            }
            else
            {
                return View("UserHomePage", result.Entities);
            }
        }
        

        // GET: Book/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetOne(id);
            if (book == null)
            {
                return NotFound();
            }

            if (User.IsInRole("Admin"))
            {
                return View("AdminDetails", book);
            }
            else
            {
                return View("UserDetails", book);
            }
        }

        // GET: Book/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var result = await _AuthorService.GetAll();
            var auth = result.Entities ?? new List<AuthorListViewModel>();
            ViewBag.Authors = new SelectList(auth, "Id", "Name");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BookCreateViewModel book)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookService.Create(book);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(book);
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetEditViewModel(id);
            if (book == null)
            {
                return NotFound();
            }
            var result = await _AuthorService.GetAll();
            var auth = result.Entities ?? new List<AuthorListViewModel>();
            ViewBag.Authors = new SelectList(auth, "Id", "Name");
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, BookEditViewModel book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _bookService.Edit(book);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(book);
        }

        // GET: Book/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetOne(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookService.GetOne(id);
            if (book == null)
            {
                return NotFound();
            }
            var result = await _bookService.HardDelete(id);
            if (result.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
            return View(book);
        }
        
    }
}
