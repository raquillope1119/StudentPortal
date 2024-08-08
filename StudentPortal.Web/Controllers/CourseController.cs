using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models.Entities;
using StudentPortal.Web.Models;

namespace StudentPortal.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CourseController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    Title = viewModel.Title,
                    Credits = viewModel.Credits
                };

                await dbContext.Courses.AddAsync(course);
                await dbContext.SaveChangesAsync();

                TempData["Success"] = "Course created successfully.";


                return RedirectToAction("List", "Course");
            }
            else { return View(viewModel); }

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var courses = await dbContext.Courses.ToListAsync();

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await dbContext.Courses.FindAsync(id);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course viewModel)
        {
            var course = await dbContext.Courses.FindAsync(viewModel.Id);

            if (course == null)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                course.Title = viewModel.Title;
                course.Credits = viewModel.Credits;

                await dbContext.SaveChangesAsync();


                TempData["Success"] = "Course updated successfully!";


                return RedirectToAction("List", "Course");
            }

            return RedirectToAction("List", "Course");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await dbContext.Courses.FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await dbContext.Courses.FindAsync(id);
            if (course != null)
            {
                dbContext.Courses.Remove(course);
                await dbContext.SaveChangesAsync();


                TempData["Success"] = "Course deleted successfully!";
            }

            return RedirectToAction("List", "Course");
        }
    }
}
