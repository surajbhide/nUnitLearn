using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ForgetTheMilk.Controllers
{
    public class TaskController : Controller
    {
        public static readonly List<Task> Tasks = new List<Task>();

        public ActionResult Index()
        {
            return View(Tasks);
        }

        [HttpPost]
        public ActionResult Add(string task)
        {
            var taskItem = new Task { Description = task };
            var pattern = new Regex(@"may\s(\d)");
            if (pattern.IsMatch(task))
            {
                var match = pattern.Match(task);
                var day = Convert.ToInt32(match.Groups[1].Value);
                taskItem.DueDate = new DateTime(DateTime.Today.Year, 5, day);
            }
            Tasks.Add(taskItem);
            return RedirectToAction("Index");
        }
    }

    public class Task
    {
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}