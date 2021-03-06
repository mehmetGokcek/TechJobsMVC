﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobsMVC.Controllers
{
    public class SearchController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ListController.ColumnChoices;
            return View();
        }

        // TODO #3: Create an action method to process a search request and render the updated search view. 
        [HttpPost]
        public IActionResult Results(string searchType, string searchTerm)
        {
            TempData["searchType"] = searchType; //temporarily holding searchtype/column value until it is used by List Controller
            TempData["searchTerm"] = searchTerm;//temporarily holding searchTerm value until it is used by List Controller

            return Redirect("/list/jobs"); //passing parameters to List Controller
        }
    }
}
