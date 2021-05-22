using Avansight.Domain.BLL;
using Avansight.Domain.Entities;
using Avansight.Domain.Services;
using Avansight.WEB.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Avansight.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var testPatientData = new SimulatePatientViewModel();
            testPatientData.SampleSize = 30;
            testPatientData.GenderMale = 5;
            testPatientData.GenderFemale = 10;
            testPatientData.Age20s = 1;
            testPatientData.Age30s = 5;
            testPatientData.Age40s = 3;
            testPatientData.Age50s = 2;
            testPatientData.Age60s = 4;
            //new PatientService().InsertRecords(testPatientData);
            return View();
        }

        public IActionResult Insert()
        {
            return View(new SimulatePatientViewModel());
        }

        [HttpPost]
        public JsonResult Insert(SimulatePatientViewModel SimulatePatientViewModel)
        {
            new PatientService().InsertRecords(SimulatePatientViewModel);
            return Json(SimulatePatientViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
