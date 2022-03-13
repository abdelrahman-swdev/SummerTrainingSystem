﻿using Microsoft.AspNetCore.Mvc;

namespace SummerTrainingSystem.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        [HttpGet("students")]
        public ActionResult GetAllStudents()
        {
            return View();
        }

        [HttpGet("supervisors")]
        public ActionResult GetAllSupervisors()
        {
            return View();
        }

        [HttpGet("companies")]
        public ActionResult GetAllCompanies()
        {
            return View();
        }
    }
}
