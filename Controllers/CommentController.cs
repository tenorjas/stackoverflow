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
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CommentModel.Include(c => c.AnswerModel).Include(c => c.QuestionModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentModel = await _context.CommentModel
                .Include(c => c.AnswerModel)
                .Include(c => c.QuestionModel)
                .SingleOrDefaultAsync(m => m.CommentID == id);
            if (commentModel == null)
            {
                return NotFound();
            }

            return View(commentModel);
        }

        // GET: Comment/Create
        public IActionResult Create()
        {
            ViewData["AnswerID"] = new SelectList(_context.AnswerModel, "AnswerID", "AnswerID");
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID");
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentID,Body,UserId,PostDate,QuestionID,AnswerID")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnswerID"] = new SelectList(_context.AnswerModel, "AnswerID", "AnswerID", commentModel.AnswerID);
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", commentModel.QuestionID);
            return View(commentModel);
        }

        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentModel = await _context.CommentModel.SingleOrDefaultAsync(m => m.CommentID == id);
            if (commentModel == null)
            {
                return NotFound();
            }
            ViewData["AnswerID"] = new SelectList(_context.AnswerModel, "AnswerID", "AnswerID", commentModel.AnswerID);
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", commentModel.QuestionID);
            return View(commentModel);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentID,Body,UserId,PostDate,QuestionID,AnswerID")] CommentModel commentModel)
        {
            if (id != commentModel.CommentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentModelExists(commentModel.CommentID))
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
            ViewData["AnswerID"] = new SelectList(_context.AnswerModel, "AnswerID", "AnswerID", commentModel.AnswerID);
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", commentModel.QuestionID);
            return View(commentModel);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentModel = await _context.CommentModel
                .Include(c => c.AnswerModel)
                .Include(c => c.QuestionModel)
                .SingleOrDefaultAsync(m => m.CommentID == id);
            if (commentModel == null)
            {
                return NotFound();
            }

            return View(commentModel);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentModel = await _context.CommentModel.SingleOrDefaultAsync(m => m.CommentID == id);
            _context.CommentModel.Remove(commentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentModelExists(int id)
        {
            return _context.CommentModel.Any(e => e.CommentID == id);
        }
    }
}
