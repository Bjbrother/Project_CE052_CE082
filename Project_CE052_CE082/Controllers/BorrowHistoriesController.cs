using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryForYou.Data;
using LibraryForYou.Models;

namespace LibraryForYou.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BorrowHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BorrowHistories.Include(b => b.Book).Include(b => b.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BorrowHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowHistories
                .Include(b => b.Book)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BorrowHistoryId == id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            return View(borrowHistory);
        }

        // GET: BorrowHistories/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Blogs, "BookId", "Title");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Blogs, "BookId", "SerialNumber", borrowHistory.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", borrowHistory.CustomerId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowHistories.FindAsync(id);
            if (borrowHistory == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Blogs, "BookId", "SerialNumber", borrowHistory.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", borrowHistory.CustomerId);
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (id != borrowHistory.BorrowHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowHistoryExists(borrowHistory.BorrowHistoryId))
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
            ViewData["BookId"] = new SelectList(_context.Blogs, "BookId", "SerialNumber", borrowHistory.BookId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", borrowHistory.CustomerId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowHistory = await _context.BorrowHistories
                .Include(b => b.Book)
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BorrowHistoryId == id);
            if (borrowHistory == null)
            {
                return NotFound();
            }

            return View(borrowHistory);
        }

        // POST: BorrowHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowHistory = await _context.BorrowHistories.FindAsync(id);
            _context.BorrowHistories.Remove(borrowHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowHistoryExists(int id)
        {
            return _context.BorrowHistories.Any(e => e.BorrowHistoryId == id);
        }
    }
}
