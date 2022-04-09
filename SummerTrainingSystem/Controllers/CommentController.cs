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
    [Route("comment")]
    [Authorize(Roles ="Student")]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Student> _stuRepo;
        private readonly IGenericRepository<Comment> _commentRepo;
        public CommentController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stuRepo = _unitOfWork.GenericRepository<Student>();
            _commentRepo = _unitOfWork.GenericRepository<Comment>();
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
                _commentRepo.Add(map);
                var result = await _unitOfWork.Complete();
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
