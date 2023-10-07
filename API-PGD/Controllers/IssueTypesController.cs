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
        private readonly IssueTypeRepositorie _issueTypeRepositorie;

        public IssueTypesController(IssueTypeRepositorie taskTypeRepositorie)
        {
            _issueTypeRepositorie = taskTypeRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetIssueTypeId(Guid id)
        {
            try
            {
                List<IssueType> lstTaskTypes = _issueTypeRepositorie.GetIssueTypeId(id);
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
                List<IssueType> lstTaskTypes = _issueTypeRepositorie.GetAllIssuesTypes();
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
                var result = _issueTypeRepositorie.InsertIssueType(taskType);
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
                string result = _issueTypeRepositorie.UpdateIssueType(taskType);
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
                _issueTypeRepositorie.DeleteIssueType(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
