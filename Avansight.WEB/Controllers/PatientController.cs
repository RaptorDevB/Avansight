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
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
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
