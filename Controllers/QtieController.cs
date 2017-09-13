using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stackoverflow.Data;
using stackoverflow.Models;

namespace stackoverflow
{
    public class QtieController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QtieController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Qtie
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.QtieModel.Include(q => q.QuestionModel).Include(q => q.TagModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Qtie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qtieModel = await _context.QtieModel
                .Include(q => q.QuestionModel)
                .Include(q => q.TagModel)
                .SingleOrDefaultAsync(m => m.QtieID == id);
            if (qtieModel == null)
            {
                return NotFound();
            }

            return View(qtieModel);
        }

        // GET: Qtie/Create
        public IActionResult Create()
        {
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID");
            ViewData["TagID"] = new SelectList(_context.TagModel, "TagID", "TagID");
            return View();
        }

        // POST: Qtie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QtieID,QuestionID,TagID")] QtieModel qtieModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qtieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", qtieModel.QuestionID);
            ViewData["TagID"] = new SelectList(_context.TagModel, "TagID", "TagID", qtieModel.TagID);
            return View(qtieModel);
        }

        // GET: Qtie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qtieModel = await _context.QtieModel.SingleOrDefaultAsync(m => m.QtieID == id);
            if (qtieModel == null)
            {
                return NotFound();
            }
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", qtieModel.QuestionID);
            ViewData["TagID"] = new SelectList(_context.TagModel, "TagID", "TagID", qtieModel.TagID);
            return View(qtieModel);
        }

        // POST: Qtie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QtieID,QuestionID,TagID")] QtieModel qtieModel)
        {
            if (id != qtieModel.QtieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qtieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QtieModelExists(qtieModel.QtieID))
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
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", qtieModel.QuestionID);
            ViewData["TagID"] = new SelectList(_context.TagModel, "TagID", "TagID", qtieModel.TagID);
            return View(qtieModel);
        }

        // GET: Qtie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qtieModel = await _context.QtieModel
                .Include(q => q.QuestionModel)
                .Include(q => q.TagModel)
                .SingleOrDefaultAsync(m => m.QtieID == id);
            if (qtieModel == null)
            {
                return NotFound();
            }

            return View(qtieModel);
        }

        // POST: Qtie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qtieModel = await _context.QtieModel.SingleOrDefaultAsync(m => m.QtieID == id);
            _context.QtieModel.Remove(qtieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QtieModelExists(int id)
        {
            return _context.QtieModel.Any(e => e.QtieID == id);
        }
    }
}
