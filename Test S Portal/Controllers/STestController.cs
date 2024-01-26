using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_S_Portal.Data;
using Test_S_Portal.Models;
using Test_S_Portal.Models.Entities;

namespace Test_S_Portal.Controllers
{
    public class STestController : Controller
    {
        private readonly AppDBContext appDBContext;

        public STestController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSTestViewModel addSTestViewModel)
        {
            var stest = new StudentTest
            {
                Name = addSTestViewModel.Name,
                Email = addSTestViewModel.Email,
                Phone = addSTestViewModel.Phone,
                Subscribed = addSTestViewModel.Subscribed
            };
            await appDBContext.TestStudents.AddAsync(stest);

            await appDBContext.SaveChangesAsync();

            return RedirectToAction("List", "STest");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await appDBContext.TestStudents.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var stest = await appDBContext.TestStudents.FindAsync(id);

            return View(stest);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentTest addSTestViewModel)
        {
            var stest = await appDBContext.TestStudents.FindAsync(addSTestViewModel.Id);

            if (stest is not null)
            {
                stest.Name = addSTestViewModel.Name;
                stest.Email = addSTestViewModel.Email;
                stest.Phone = addSTestViewModel.Phone;
                stest.Subscribed = addSTestViewModel.Subscribed;

                await appDBContext.SaveChangesAsync();
            }

            return RedirectToAction("List","STest");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(StudentTest viewModel)
        {
            var stest = await appDBContext.TestStudents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if(stest is not null)
            {
                appDBContext.TestStudents.Remove(stest);
                await appDBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "STest");
        }
    }
}
