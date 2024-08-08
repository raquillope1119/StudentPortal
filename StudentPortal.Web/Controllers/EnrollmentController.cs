using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models.Entities;
using StudentPortal.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace StudentPortal.Web.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EnrollmentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void PopulateStudentList()
        {
            IEnumerable<SelectListItem> GetStudents =
                dbContext.Students.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            ViewBag.StudentsList = GetStudents;
        }

        public void PopulateCourseList()
        {
            IEnumerable<SelectListItem> GetCourses =
                dbContext.Courses.Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                });

            ViewBag.CoursesList = GetCourses;
        }

        public void PopulateGradeList()
        {
            IEnumerable <SelectListItem> GetGrades =
            Enum.GetValues(typeof(Grade)).Cast<Grade>().Select(m => new SelectListItem
            {
                Text = m.ToString(),
                Value = ((int)m).ToString()
            }).ToList();

            ViewBag.GradesList = GetGrades;
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            PopulateCourseList();
            PopulateStudentList();
            PopulateGradeList();
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEnrollmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var enrollment = new Enrollment
                {
                    CourseId = viewModel.CourseId,
                    StudentId = viewModel.StudentId,
                    Grade = viewModel.Grade

                };

                await dbContext.Enrollments.AddAsync(enrollment);
                await dbContext.SaveChangesAsync();


                TempData["Success"] = "Enrollment deleted successfully!";

                return RedirectToAction("List", "Enrollment");
            }
            PopulateCourseList();
            PopulateStudentList();
            PopulateGradeList();

            return View(viewModel);

            
        }



        [HttpGet]
        public async Task<IActionResult> List()
        {
            var enrollments = await dbContext.Enrollments.ToListAsync();
            foreach (var course in enrollments)
            {
                course.Course = await dbContext.Courses.FirstOrDefaultAsync(m => m.Id==course.CourseId);
            }

            foreach (var student in enrollments)
            {
                student.Student = await dbContext.Students.FirstOrDefaultAsync(m =>m.Id==student.StudentId);
            }

            return View(enrollments);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PopulateCourseList();
            PopulateStudentList();
            PopulateGradeList();
            var enrollment = await dbContext.Enrollments.FindAsync(id);
            return View(enrollment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Enrollment viewModel)
        {
            var enrollment = await dbContext.Enrollments.FindAsync(viewModel.Id);

            if (enrollment == null)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                enrollment.CourseId = viewModel.CourseId;
                enrollment.StudentId = viewModel.StudentId;
                enrollment.Grade = viewModel.Grade;

                await dbContext.SaveChangesAsync();

                TempData["Success"] = "Enrollment deleted successfully!";

                return RedirectToAction("List", "Enrollment");

            }
            PopulateCourseList();
            PopulateStudentList();
            PopulateGradeList();

            return View(viewModel);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            PopulateCourseList();
            PopulateStudentList();
            PopulateGradeList();

            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await dbContext.Enrollments.FirstOrDefaultAsync(m => m.Id == id);


            if (enrollment == null)
            {
                return NotFound();
            }

            var course = await dbContext.Courses.FirstOrDefaultAsync(m => m.Id == enrollment.CourseId);

            var student = await dbContext.Students.FirstOrDefaultAsync(m => m.Id == enrollment.StudentId);


            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var enrollment = await dbContext.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                dbContext.Enrollments.Remove(enrollment);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "Enrollment deleted successfully!";
            }

            return RedirectToAction("List", "Enrollment");
        }

    }
}
