﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBooks.Data;
using MVCBooks.Models;

namespace MVCBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly MVCBooksContext _context;

        public BooksController(MVCBooksContext context)
        {
            _context = context;
        }

            // GET: Books
            public async Task<IActionResult> Index(string bookGenre, string searchString)
            {
                // Use LINQ to get list of genres.
                IQueryable<string> genreQuery = from b in _context.Book
                                                orderby b.Genre
                                                select b.Genre;
                var books = from b in _context.Book
                            select b;

                if (!string.IsNullOrEmpty(searchString))
                {
                    books = books.Where(s => s.Title.Contains(searchString));
                }

                if (!string.IsNullOrEmpty(bookGenre))
                {
                    books = books.Where(x => x.Genre == bookGenre);
                }

                var bookGenreVM = new BookGenreViewModel
                {
                    Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                    Books = await books.ToListAsync()
                };

                return View(bookGenreVM);
            }

            // GET: Books/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = await _context.Book
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            }

            // GET: Books/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Books/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price, Rating")] Book book)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(book);
            }

            // GET: Books/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = await _context.Book.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

            // POST: Books/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price, Rating")] Book book)
            {
                if (id != book.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(book);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BookExists(book.Id))
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
                return View(book);
            }

            // GET: Books/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = await _context.Book
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            }

            // POST: Books/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var book = await _context.Book.FindAsync(id);
                if (book != null)
                {
                    _context.Book.Remove(book);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            private bool BookExists(int id)
            {
                return _context.Book.Any(e => e.Id == id);
            }
        }
    }