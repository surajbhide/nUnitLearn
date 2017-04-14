using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForgetTheMilk.Controllers;
using NUnit.Framework;

namespace ConsoleVerification
{
    public class CreateTaskTests : AssertionHelper
    {
        [Test]
        public void with_only_description_and_no_due_date()
        {
            // arrange
            var input = "test task";

            // act
            Task task = new Task(input, DateTime.Today);

            // assert
            Expect(task.Description, Is.EqualTo(input));
            Expect(task.DueDate, Is.Null);
        }

        [Test]
        public void description_with_due_date()
        {
            var input = "test task may 9 - with respect 2015/05/01";

            var task = new Task(input, new DateTime(2015, 5, 01));

            Assert.AreEqual(task.Description, input);
            Assert.AreEqual(task.DueDate, new DateTime(2015, 5, 9));
        }

        [Test]
        public void duedateinpastmonth_shouldwrapyear()
        {
            var input = "test task apr 9 - with respect to 2015/05/11";

            var task = new Task(input, new DateTime(2015, 5, 11));

            Assert.AreEqual(input, task.Description);
            Assert.AreEqual(new DateTime(2016, 4, 9), task.DueDate);
        }

        [Test]
        public void duedate_incurrentmonthbutpastdate_shouldparse()
        {
            var input = "test task may 9 - with respect to 2015/05/11";

            var task = new Task(input, new DateTime(2015, 5, 11));

            Assert.AreEqual(input, task.Description);
            Assert.AreEqual(new DateTime(2016, 5, 9), task.DueDate);
        }

        [Test]
        [TestCase("Groceries jan 5", 1)]
        [TestCase("Groceries feb 5", 2)]
        [TestCase("Groceries mar 5", 3)]
        [TestCase("Groceries apr 5", 4)]
        [TestCase("Groceries may 5", 5)]
        [TestCase("Groceries jun 5", 6)]
        [TestCase("Groceries jul 5", 7)]
        [TestCase("Groceries aug 5", 8)]
        [TestCase("Groceries sep 5", 9)]
        [TestCase("Groceries oct 5", 10)]
        [TestCase("Groceries nov 5", 11)]
        [TestCase("Groceries dec 5", 12)]
        public void DifferentMonthDueDates(string input, int expectedMonth)
        {
            var task = new Task(input, default(DateTime));

            Expect(task.DueDate, Is.Not.Null);
            Expect(task.DueDate.Value.Month, Is.EqualTo(expectedMonth));
        }

        [Test]
        public void TwoDigitDate_ShouldParseBothDigits()
        {
            var input = "Groceries apr 10";
            var task = new Task(input, default(DateTime));

            Expect(task.DueDate, Is.Not.Null);
            Expect(task.DueDate.Value.Day, Is.EqualTo(10));
        }

        [Test]
        public void DayPastMonthEnd_ShouldNotParseDate()
        {
            var input = "Groceries apr 44";
            var task = new Task(input, default(DateTime));

            Expect(task.DueDate, Is.Null);
        }

        [Test]
        public void AddFeb29TaskInYearBeforeLeapYear_ParsesDueDate()
        {
            var input = "Groceries feb 29";

            var task = new Task(input, new DateTime(2015, 5, 1));

            Expect(task.DueDate, Is.EqualTo(new DateTime(2016, 2, 29)));
        }
    }
}
