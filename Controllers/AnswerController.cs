using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stackoverflow.Data;
using stackoverflow.Models;

namespace stackoverflow
{
    public class AnswerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnswerController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _userManager = um;
        }

        // GET: Answer
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AnswerModel.Include(a => a.QuestionModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Answer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerModel = await _context.AnswerModel
                .Include(a => a.QuestionModel)
                .SingleOrDefaultAsync(m => m.AnswerID == id);
            if (answerModel == null)
            {
                return NotFound();
            }

            return View(answerModel);
        }

        // GET: Answer/Create
        public IActionResult Create()
        {
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID");
            return View();
        }

        // POST: Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromRoute]int id, [FromForm]string answer)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var newAnswer = new AnswerModel {QuestionID = id, Body = answer, ApplicationUserId = user.Id};
            _context.AnswerModel.Add(newAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details","Question",new{id});
        }

        // GET: Answer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerModel = await _context.AnswerModel.SingleOrDefaultAsync(m => m.AnswerID == id);
            if (answerModel == null)
            {
                return NotFound();
            }
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", answerModel.QuestionID);
            return View(answerModel);
        }

        // POST: Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerID,Body,UserId,PostDate,QuestionID,VoteCount")] AnswerModel answerModel)
        {
            if (id != answerModel.AnswerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerModelExists(answerModel.AnswerID))
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
            ViewData["QuestionID"] = new SelectList(_context.QuestionModel, "QuestionID", "QuestionID", answerModel.QuestionID);
            return View(answerModel);
        }

        // GET: Answer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answerModel = await _context.AnswerModel
                .Include(a => a.QuestionModel)
                .SingleOrDefaultAsync(m => m.AnswerID == id);
            if (answerModel == null)
            {
                return NotFound();
            }

            return View(answerModel);
        }

        // POST: Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answerModel = await _context.AnswerModel.SingleOrDefaultAsync(m => m.AnswerID == id);
            _context.AnswerModel.Remove(answerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerModelExists(int id)
        {
            return _context.AnswerModel.Any(e => e.AnswerID == id);
        }
    }
}
