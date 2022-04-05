using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerTrainingSystem.Models;
using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SummerTrainingSystem.Controllers
{
    [Authorize(Roles ="Student")]
    [Route("comment")]
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Student> _stuRepo;
        private readonly IGenericRepository<Comment> _commentRepo;
        public CommentController(IGenericRepository<Comment> commentrepo,
            IGenericRepository<Student> stuRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _stuRepo = stuRepo;
            _commentRepo = commentrepo;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] CommentJsonVM model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var comment = new CommentVM()
                {
                    HrCompanyId = model.HrCompanyId,
                    Message = model.Comment,
                    CreateAt = DateTime.Now,
                    StudentId = loggedInUser
                };
                var map = _mapper.Map<Comment>(comment);
                var result = _commentRepo.Add(map);
                if (result > 0)
                {
                    var student = await _stuRepo.GetByStringIdAsync(loggedInUser);
                    return Ok(new { 
                        firstname = student.FirstName, 
                        lastname = student.LastName, 
                        comment = comment.Message, 
                        profilePicUrl = student.ProfilePictureUrl 
                    });
                }
            }
            return BadRequest();
        }
    }
}
