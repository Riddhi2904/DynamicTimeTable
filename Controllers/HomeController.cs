using System.Diagnostics;
using DynamicTimeTable.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicTimeTable.Controllers
{
    public class HomeController : Controller
    {

        private static inputHour _inputData;
        private static SubjectHours _subjectData;

        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(inputHour model)
        {
            if (!ModelState.IsValid) return View(model);
            _inputData = model;
            return RedirectToAction("SubjectHours");
        }

        public IActionResult SubjectHours()
        {
            var model = new SubjectHours
            {
                TotalHours = _inputData.TotalHours
            };
            for (int i = 0; i < _inputData.TotalSubjects; i++)
            {
                model.Subjects.Add(new SubjectHourEntry());
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult SubjectHours(SubjectHours model)
        {
            int sum = model.Subjects.Sum(s => s.Hours);
            if (sum != model.TotalHours)
            {
                ModelState.AddModelError("", $"Total hours must equal {_inputData.TotalHours}");
                return View(model);
            }

            _subjectData = model;
            return RedirectToAction("Generate");
        }

        public IActionResult Generate()
        {
            var totalCells = _inputData.WorkingDays * _inputData.SubjectsPerDay;
            var allSubjects = new List<string>();

            foreach (var subject in _subjectData.Subjects)
            {
                allSubjects.AddRange(Enumerable.Repeat(subject.SubjectName, subject.Hours));
            }

            allSubjects = allSubjects.OrderBy(x => Guid.NewGuid()).ToList(); // shuffle

            string[,] table = new string[_inputData.SubjectsPerDay, _inputData.WorkingDays];
            int index = 0;

            for (int row = 0; row < _inputData.SubjectsPerDay; row++)
            {
                for (int col = 0; col < _inputData.WorkingDays; col++)
                {
                    table[row, col] = allSubjects[index++];
                }
            }

            var viewModel = new TimeTable
            {
                Subjects = _subjectData.Subjects.Select(s => s.SubjectName).ToList(),
                WorkingDays = _inputData.WorkingDays,
                SubjectsPerDay = _inputData.SubjectsPerDay,
                Timetable = table
            };

            return View(viewModel);
        }



        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
