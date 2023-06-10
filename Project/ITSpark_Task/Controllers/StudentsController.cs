using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITSpark_Task;
using Microsoft.VisualBasic;
using ITSpark_Task.Models.BussinessLayer.HelperClasses;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Host;
using ITSpark_Task.Models;
using System.Diagnostics;
using ITSpark_Task.Services;
using System.Collections;
using ITSpark_Task.Models.BussinessLayer;

namespace ITSpark_Task.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolSystemContext _context;
        private readonly LanguageService _Localization;

        public StudentsController(SchoolSystemContext context , LanguageService Localization)
        {
            _context = context;
            _Localization = Localization;
        }

        #region Localization
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion

        #region Localization Privacy & Error
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
        #endregion

        // GET: Students
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            if (_context.Students != null)
            {
                #region Initialize All Words In Page
                // 
                ViewBag.WelcomeMassege = _Localization.Getkey("Welcome To Student DashBaord");
                ViewBag.Students = _Localization.Getkey("Students");
                ViewBag.DashBoard = _Localization.Getkey("DashBoard");
                ViewBag.Name = _Localization.Getkey("Name");
                ViewBag.birthDate = _Localization.Getkey("BirthDate");
                ViewBag.City = _Localization.Getkey("City");
                ViewBag.Gender = _Localization.Getkey("Gender");
                ViewBag.Search = _Localization.Getkey("Search");
                ViewBag.StudentName = _Localization.Getkey("Student Name");
                ViewBag.date = _Localization.Getkey("mm/dd/yyyy");
                ViewBag.edit = _Localization.Getkey("Edit");
                ViewBag.delete = _Localization.Getkey("Delete");
                ViewBag.Details = _Localization.Getkey("Details");
                ViewBag.CreateNew = _Localization.Getkey("Create New");
                ViewBag.Male = _Localization.Getkey("Male");
                ViewBag.Female = _Localization.Getkey("Female");
                ViewBag.ALL = _Localization.Getkey("ALL");
                ViewBag.Languages = _Localization.Getkey("Languages");
                ViewBag.StudentsDashBoard = _Localization.Getkey("Students DashBoard");
                ViewBag.English = _Localization.Getkey("English");
                ViewBag.Arabic = _Localization.Getkey("Arabic");
                ViewBag.BirthDateFrom = _Localization.Getkey("BirthDate Form");
                ViewBag.BirthDateTo = _Localization.Getkey("BirthDate To");

                #endregion
                ViewBag.StudentList = await _context.Students.ToListAsync();
                return View();
            }
            else
            {
                return Problem("Entity set 'SchoolSystemContext.Students' is null.");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index(StudentToSearch studentToSearch)
        {
            if (ModelState.IsValid)
            {
                #region Initialize All Words In Page
                // 
                ViewBag.WelcomeMassege = _Localization.Getkey("Welcome To Student DashBaord");
                ViewBag.Students = _Localization.Getkey("Students");
                ViewBag.DashBoard = _Localization.Getkey("DashBoard");
                ViewBag.Name = _Localization.Getkey("Name");
                ViewBag.birthDate = _Localization.Getkey("BirthDate");
                ViewBag.City = _Localization.Getkey("City");
                ViewBag.Gender = _Localization.Getkey("Gender");
                ViewBag.Search = _Localization.Getkey("Search");
                ViewBag.StudentName = _Localization.Getkey("Student Name");
                ViewBag.date = _Localization.Getkey("mm/dd/yyyy");
                ViewBag.edit = _Localization.Getkey("Edit");
                ViewBag.delete = _Localization.Getkey("Delete");
                ViewBag.Details = _Localization.Getkey("Details");
                ViewBag.CreateNew = _Localization.Getkey("Create New");
                ViewBag.Male = _Localization.Getkey("Male");
                ViewBag.Female = _Localization.Getkey("Female");
                ViewBag.ALL = _Localization.Getkey("ALL");
                ViewBag.Languages = _Localization.Getkey("Languages");
                ViewBag.StudentsDashBoard = _Localization.Getkey("Students DashBoard");
                ViewBag.English = _Localization.Getkey("English");
                ViewBag.Arabic = _Localization.Getkey("Arabic");
                ViewBag.BirthDateFrom = _Localization.Getkey("BirthDate Form");
                ViewBag.BirthDateTo = _Localization.Getkey("BirthDate To");

                #endregion
                var query = _context.Students.AsQueryable();

                if (!string.IsNullOrEmpty(studentToSearch.Name))
                {
                    query = query.Where(s => s.Name == studentToSearch.Name);
                }

                if (studentToSearch.Gender != null && studentToSearch.Gender != "All" && studentToSearch.Gender.Any())
                {
                     query = query.Where(s=>s.Gender == studentToSearch.Gender);
                }

                if (studentToSearch.City != null && studentToSearch.City != "All" && studentToSearch.City.Any())
                {
                    query = query.Where(s => s.City == studentToSearch.City);
                }

                query = query.Where(s => s.BirthDate >= studentToSearch.BirthDateFrom && s.BirthDate <= studentToSearch.BirthDateTo);

                var studentList = await query.ToListAsync();
                ViewBag.StudentList = studentList;
                return View();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            #region Initialize All Words In Page
            // 
            ViewBag.WelcomeMassege = _Localization.Getkey("Welcome To Student DashBaord");
            ViewBag.Students = _Localization.Getkey("Students");
            ViewBag.DashBoard = _Localization.Getkey("DashBoard");
            ViewBag.Name = _Localization.Getkey("Name");
            ViewBag.birthDate = _Localization.Getkey("BirthDate");
            ViewBag.City = _Localization.Getkey("City");
            ViewBag.Gender = _Localization.Getkey("Gender");
            ViewBag.Search = _Localization.Getkey("Search");
            ViewBag.StudentName = _Localization.Getkey("Student Name");
            ViewBag.date = _Localization.Getkey("mm/dd/yyyy");
            ViewBag.edit = _Localization.Getkey("Edit");
            ViewBag.delete = _Localization.Getkey("Delete");
            ViewBag.Details = _Localization.Getkey("Details");
            ViewBag.CreateNew = _Localization.Getkey("Create New");
            ViewBag.Male = _Localization.Getkey("Male");
            ViewBag.Female = _Localization.Getkey("Female");
            ViewBag.ALL = _Localization.Getkey("ALL");
            ViewBag.Languages = _Localization.Getkey("Languages");
            ViewBag.StudentsDashBoard = _Localization.Getkey("Students DashBoard");
            ViewBag.English = _Localization.Getkey("English");
            ViewBag.Arabic = _Localization.Getkey("Arabic");

            #endregion

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            #region Initialize All Words In Page
            // 
            ViewBag.WelcomeMassege = _Localization.Getkey("Welcome To Student DashBaord");
            ViewBag.Students = _Localization.Getkey("Students");
            ViewBag.DashBoard = _Localization.Getkey("DashBoard");
            ViewBag.Name = _Localization.Getkey("Name");
            ViewBag.birthDate = _Localization.Getkey("BirthDate");
            ViewBag.City = _Localization.Getkey("City");
            ViewBag.Gender = _Localization.Getkey("Gender");
            ViewBag.Search = _Localization.Getkey("Search");
            ViewBag.StudentName = _Localization.Getkey("Student Name");
            ViewBag.date = _Localization.Getkey("mm/dd/yyyy");
            ViewBag.edit = _Localization.Getkey("Edit");
            ViewBag.delete = _Localization.Getkey("Delete");
            ViewBag.Details = _Localization.Getkey("Details");
            ViewBag.CreateNew = _Localization.Getkey("Create New");
            ViewBag.Male = _Localization.Getkey("Male");
            ViewBag.Female = _Localization.Getkey("Female");
            ViewBag.ALL = _Localization.Getkey("ALL");
            ViewBag.Languages = _Localization.Getkey("Languages");
            ViewBag.StudentsDashBoard = _Localization.Getkey("Students DashBoard");
            ViewBag.English = _Localization.Getkey("English");
            ViewBag.Arabic = _Localization.Getkey("Arabic");
            ViewBag.BacktoList = _Localization.Getkey("Back To List");


            #endregion

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            #region Initialize arabic Words 

            ViewBag.WelcomeMassege = _Localization.Getkey("Welcome To Student DashBaord");
            ViewBag.Students = _Localization.Getkey("Students");
            ViewBag.Student = _Localization.Getkey("Student");
            ViewBag.DashBoard = _Localization.Getkey("DashBoard");
            ViewBag.Name = _Localization.Getkey("Name");
            ViewBag.birthDate = _Localization.Getkey("BirthDate");
            ViewBag.City = _Localization.Getkey("City");
            ViewBag.Gender = _Localization.Getkey("Gender");
            ViewBag.Search = _Localization.Getkey("Search");
            ViewBag.StudentName = _Localization.Getkey("Student Name");
            ViewBag.date = _Localization.Getkey("mm/dd/yyyy");
            ViewBag.edit = _Localization.Getkey("Edit");
            ViewBag.delete = _Localization.Getkey("Delete");
            ViewBag.Details = _Localization.Getkey("Details");
            ViewBag.CreateNew = _Localization.Getkey("Create New");
            ViewBag.Male = _Localization.Getkey("Male");
            ViewBag.Female = _Localization.Getkey("Female");
            ViewBag.ALL = _Localization.Getkey("ALL");
            ViewBag.Languages = _Localization.Getkey("Languages");
            ViewBag.StudentsDashBoard = _Localization.Getkey("Students DashBoard");
            ViewBag.English = _Localization.Getkey("English");
            ViewBag.Arabic = _Localization.Getkey("Arabic");
            ViewBag.Create = _Localization.Getkey("Create");
            ViewBag.BacktoList = _Localization.Getkey("Back To List");

            #endregion

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,City,BirthDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            #region Initialize All Words In Page
            // 
            ViewBag.WelcomeMassege = _Localization.Getkey("Welcome To Student DashBaord");
            ViewBag.Students = _Localization.Getkey("Students");
            ViewBag.DashBoard = _Localization.Getkey("DashBoard");
            ViewBag.Name = _Localization.Getkey("Name");
            ViewBag.birthDate = _Localization.Getkey("BirthDate");
            ViewBag.City = _Localization.Getkey("City");
            ViewBag.Gender = _Localization.Getkey("Gender");
            ViewBag.Search = _Localization.Getkey("Search");
            ViewBag.StudentName = _Localization.Getkey("Student Name");
            ViewBag.date = _Localization.Getkey("mm/dd/yyyy");
            ViewBag.edit = _Localization.Getkey("Edit");
            ViewBag.delete = _Localization.Getkey("Delete");
            ViewBag.Details = _Localization.Getkey("Details");
            ViewBag.CreateNew = _Localization.Getkey("Create New");
            ViewBag.Male = _Localization.Getkey("Male");
            ViewBag.Female = _Localization.Getkey("Female");
            ViewBag.ALL = _Localization.Getkey("ALL");
            ViewBag.Languages = _Localization.Getkey("Languages");
            ViewBag.StudentsDashBoard = _Localization.Getkey("Students DashBoard");
            ViewBag.English = _Localization.Getkey("English");
            ViewBag.Arabic = _Localization.Getkey("Arabic");
            ViewBag.BacktoList = _Localization.Getkey("Back To List");
            ViewBag.Save = _Localization.Getkey("Save");


            #endregion

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,City,BirthDate")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SchoolSystemContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }


       



    }
}
