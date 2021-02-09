using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SBSCTechnicalAssessmentData.Interfaces;
using SBSCTechnicalAssessmentData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSCTechnicalAssessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        //api/student
        [HttpGet]
        public IActionResult Index()
        {
            Models.Response response = new Models.Response();
            response.ResponseCode = "00";
            response.ResponseMessage = "Api Is Up";

            return Ok(new JsonResult(response));
        }

        //GET: api/student/{ID}
        [HttpGet("{id}")]
        public IActionResult GetStudentByID(int id)
        {
            Models.Response response = new Models.Response();

            if (id <= 0)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = "Invalid ID Passed";
                return NotFound(new JsonResult(response));
            }

            Student student = _studentService.GetStudentByID(id, out string message);
            if (student == null)
            {
                response.ResponseCode = "-02";
                response.ResponseMessage = message;
                return NotFound(new JsonResult(response));
            }

            response.ResponseCode = "00";
            response.ResponseMessage = "Student Record Retrieved Successfully";
            response.ResponseObject = student;
            return Ok(new JsonResult(response));
        }

        //POST: api/student
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            Models.Response response = new Models.Response();

            if (!student.IsValidEmail)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = "Email Address is not valid";
                return NotFound(new JsonResult(response));
            }

            bool IsCreated = _studentService.CreateStudent(student.Name, student.FamilyName, student.Address, student.CountryOfOrigin, student.EmailAddress, student.Age, student.Approved = false, out string message);
            if (!IsCreated)
            {
                response.ResponseCode = "-02";
                response.ResponseMessage = message;
                return NotFound(new JsonResult(response));
            }

            Student createdStudet = _studentService.GetStudentByEmail(student.EmailAddress, out string msg);
            student.Id = createdStudet.Id;

            response.ResponseCode = "00";
            response.ResponseMessage = "Student Created Successfully. To retrieve record GET: {localhost}/api/student/" + createdStudet.Id;
            response.ResponseObject = student;
            return Ok(new JsonResult(response));
        }

        //PUT: api/student/{ID}
        [HttpPut("{id}")]
        public IActionResult UpdateStudentInfo([FromBody] Student student, int id)
        {
            Models.Response response = new Models.Response();

            if (!student.IsValidEmail)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = "Email Address is not valid";
                return NotFound(new JsonResult(response));
            }

            bool IsUpdated = _studentService.UpdateStudent(id, student, out string message);
            if (!IsUpdated)
            {
                response.ResponseCode = "-02";
                response.ResponseMessage = message;
                return NotFound(new JsonResult(response));
            }

            response.ResponseCode = "00";
            response.ResponseMessage = "Student Record Updated Successfully";
            return Ok(new JsonResult(response));
        }

        //DELETE: api/student/{ID}
        [HttpDelete("{id}")]
        public IActionResult RemoveStudentByID(int id)
        {
            Models.Response response = new Models.Response();

            if (id <= 0)
            {
                response.ResponseCode = "-01";
                response.ResponseMessage = "Invalid ID Passed";
                return NotFound(new JsonResult(response));
            }

            bool IsRemoved = _studentService.RemoveStudent(id, out string message);
            if (!IsRemoved)
            {
                response.ResponseCode = "-02";
                response.ResponseMessage = message;
                return NotFound(new JsonResult(response));
            }

            response.ResponseCode = "00";
            response.ResponseMessage = "Student Removed Successfully";
            return Ok(new JsonResult(response));
        }
    }
}
