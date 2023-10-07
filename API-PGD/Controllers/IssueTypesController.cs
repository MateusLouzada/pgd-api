using API_PGD.Models;
using API_PGD.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PGD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTypesController : ControllerBase
    {
        private readonly IssueTypeRepositorie _taskTypeRepositorie;

        public IssueTypesController(IssueTypeRepositorie taskTypeRepositorie)
        {
            _taskTypeRepositorie = taskTypeRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetIssueTypeId(Guid id)
        {
            try
            {
                List<IssueType> lstTaskTypes = _taskTypeRepositorie.GetIssueTypeId(id);
                return Ok(lstTaskTypes);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAllIssueTypes()
        {
            try
            {
                List<IssueType> lstTaskTypes = _taskTypeRepositorie.GetAllIssuesTypes();
                return Ok(lstTaskTypes);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult RegisterIssueType([FromBody] IssueType taskType)
        {
            try
            {
                var result = _taskTypeRepositorie.InsertIssueType(taskType);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateIssueType([FromBody] IssueType taskType)
        {
            try
            {
                string result = _taskTypeRepositorie.UpdateIssueType(taskType);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteIssueType(Guid id)
        {
            try
            {
                _taskTypeRepositorie.DeleteIssueType(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
