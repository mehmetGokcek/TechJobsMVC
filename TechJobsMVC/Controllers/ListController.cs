using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechJobsMVC.Data;
using TechJobsMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsMVC.Controllers
{
    public class ListController : Controller
    {
        internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"employer", "Employer"},
            {"location", "Location"},
            {"positionType", "Position Type"},
            {"coreCompetency", "Skill"}
        };

        internal static Dictionary<string, List<JobField>> TableChoices = new Dictionary<string, List<JobField>>()
        {
            {"employer", JobData.GetAllEmployers()},
            {"location", JobData.GetAllLocations()},
            {"positionType", JobData.GetAllPositionTypes()},
            {"coreCompetency", JobData.GetAllCoreCompetencies()}
        };

        public IActionResult Index()
        {
            ViewBag.columns = ColumnChoices;
            ViewBag.tableChoices = TableChoices;
            ViewBag.employers = JobData.GetAllEmployers();
            ViewBag.locations = JobData.GetAllLocations();
            ViewBag.positionTypes = JobData.GetAllPositionTypes();
            ViewBag.skills = JobData.GetAllCoreCompetencies();

            return View();
        }

        // list jobs by column and value
        public IActionResult Jobs(string column, string value)
        {
            List<Job> jobs;

            if (TempData["searchType"] != null)
            {
                column = (string)TempData["searchType"]; //accepting column parameter from Search Controller
                value = (string)TempData["searchTerm"]; // accepting value paremeter from Search Controller
            }



            if (column.ToLower().Equals("all"))
            {
                if (String.IsNullOrWhiteSpace(value) || value == "all") //check to see if there is any search term entered in the search
                {
                    jobs = JobData.FindAll(); // if there is no search term entered by the user show all jobs
                    ViewBag.title = "ALL JOBS";
                }
                else {
                    jobs = JobData.FindByValue(value); // when a search term is entered by the user, search through everything.
                    ViewBag.title = "JOBS WITH " + ColumnChoices[column].ToUpper() + ": " + value.ToUpper();

                }
            }

            else if (String.IsNullOrWhiteSpace(value)) 
            {
                jobs = null;
                return Redirect("/List");

            }

            else
            {
                jobs = JobData.FindByColumnAndValue(column, value);

                ViewBag.title = "JOBS WITH " + ColumnChoices[column].ToUpper() + ": " + value.ToUpper();
            }
            ViewBag.jobs = jobs;

            return View();
        }

    }
}
