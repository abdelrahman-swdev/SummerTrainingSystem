using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemEF.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SummerTrainingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api")]
    public class EndpointsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EndpointsController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("students")]
        public IActionResult GetStudents()
        {
            // get records per page
            var pageSize = int.Parse(Request.Form["length"]);

            // get skiped records
            var skip = int.Parse(Request.Form["start"]);

            // get search term
            var searchTerm = Request.Form["search[value]"];

            // get order direction
            var orderDirection = Request.Form["order[0][dir]"];

            // get clicked column index to order
            var columnIndex = Request.Form["order[0][column]"];

            // get clicked column name to order
            var columnName = Request.Form["columns[" + columnIndex + "][name]"];

            // apply filtering with search term if it has value
            IQueryable<Student> students = _context.Students.Where(m => string.IsNullOrEmpty(searchTerm) || 
                m.FirstName.Contains(searchTerm) ||
                m.LastName.Contains(searchTerm) ||
                m.Email.Contains(searchTerm) ||
                m.UniversityID.ToString().Contains(searchTerm) ||
                m.Department.Name.Contains(searchTerm)
                ).Include(s => s.Department);


            // apply ordering
            if (!(string.IsNullOrEmpty(orderDirection) && string.IsNullOrEmpty(columnIndex) && string.IsNullOrEmpty(columnName)))
            {
                students = students.OrderBy($"{columnName} {orderDirection}");
            }

            // get total records count
            var recordsTotal = students.Count();

            // implements pagination
            var data = students.Skip(skip).Take(pageSize).ToList();

            // return json object as a result
            var jsonData = new
            {
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            return Ok(jsonData);
        }

        [HttpPost("supervisors")]
        public IActionResult GetSupervisors()
        {
            // get records per page
            var pageSize = int.Parse(Request.Form["length"]);

            // get skiped records
            var skip = int.Parse(Request.Form["start"]);

            // get search term
            var searchTerm = Request.Form["search[value]"];

            // get order direction
            var orderDirection = Request.Form["order[0][dir]"];

            // get clicked column index to order
            var columnIndex = Request.Form["order[0][column]"];

            // get clicked column name to order
            var columnName = Request.Form["columns[" + columnIndex + "][name]"];

            // apply filtering with search term if it has value
            IQueryable<Supervisor> supervisors = _context.Supervisors.Where(m => string.IsNullOrEmpty(searchTerm) || 
                m.FirstName.Contains(searchTerm) ||
                m.LastName.Contains(searchTerm) ||
                m.Email.Contains(searchTerm) ||
                m.UniversityID.ToString().Contains(searchTerm) ||
                m.Department.Name.Contains(searchTerm)
                ).Include(s => s.Department);


            // apply ordering
            if (!(string.IsNullOrEmpty(orderDirection) && string.IsNullOrEmpty(columnIndex) && string.IsNullOrEmpty(columnName)))
            {
                supervisors = supervisors.OrderBy($"{columnName} {orderDirection}");
            }

            // get total records count
            var recordsTotal = supervisors.Count();

            // implements pagination
            var data = supervisors.Skip(skip).Take(pageSize).ToList();

            // return json object as a result
            var jsonData = new
            {
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            return Ok(jsonData);
        }


        [HttpPost("companies")]
        public IActionResult GetCompanies()
        {
            // get records per page
            var pageSize = int.Parse(Request.Form["length"]);

            // get skiped records
            var skip = int.Parse(Request.Form["start"]);

            // get search term
            var searchTerm = Request.Form["search[value]"];

            // get order direction
            var orderDirection = Request.Form["order[0][dir]"];

            // get clicked column index to order
            var columnIndex = Request.Form["order[0][column]"];

            // get clicked column name to order
            var columnName = Request.Form["columns[" + columnIndex + "][name]"];

            // apply filtering with search term if it has value
            IQueryable<HrCompany> companies = _context.HrCompanies.Where(m => string.IsNullOrEmpty(searchTerm) || 
                m.Name.Contains(searchTerm) ||
                m.FoundedAt.ToString("yyyy").Contains(searchTerm) ||
                m.City.Contains(searchTerm) ||
                m.CompanySize.SizeRange.Contains(searchTerm)
                ).Include(s => s.CompanySize);


            // apply ordering
            if (!(string.IsNullOrEmpty(orderDirection) && string.IsNullOrEmpty(columnIndex) && string.IsNullOrEmpty(columnName)))
            {
                companies = companies.OrderBy($"{columnName} {orderDirection}");
            }

            // get total records count
            var recordsTotal = companies.Count();

            // implements pagination
            var data = companies.Skip(skip).Take(pageSize).ToList();

            // return json object as a result
            var jsonData = new
            {
                recordsFiltered = recordsTotal,
                recordsTotal,
                data
            };

            return Ok(jsonData);
        }
    }
}
