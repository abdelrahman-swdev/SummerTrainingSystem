﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using SummerTrainingSystemEF.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Route("api")]
    public class EndpointsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public EndpointsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpDelete("delete-account/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return Ok();
            }
            return BadRequest();
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
