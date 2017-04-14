using System;
using System.Collections.Generic;
using System.Globalization;
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
            var taskItem = new Task(task, DateTime.Today, new LinkValidator());
            Tasks.Add(taskItem);
            return RedirectToAction("Index");
        }
    }

    public class Task
    {
        public Task(string task, DateTime today, ILinkValidator linkValidator = null)
        {
            Description = task;
            var pattern = new Regex(@"(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)\s(\d{1,2})");
            if (pattern.IsMatch(task))
            {
                var match = pattern.Match(task);
                var month = match.Groups[1].Value;
                var mValue = DateTime.ParseExact(month, "MMM", CultureInfo.CurrentCulture).Month;
                var day = Convert.ToInt32(match.Groups[2].Value);
                var year = today.Year;
                if (mValue < today.Month ||
                    (mValue == today.Month && day < today.Day))
                {
                    year++;
                }
                if (day <= DateTime.DaysInMonth(year, mValue))
                {
                    DueDate = new DateTime(year, mValue, day);
                }
            }

            var linkpattern = new Regex(@"(http://[^\s]+)");
            if (linkpattern.IsMatch(task))
            {
                var link = linkpattern.Match(task).Groups[1].Value;
                linkValidator.Validate(link);
                Link = link;
            }
        }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime? DueDate { get; set; }
    }
}