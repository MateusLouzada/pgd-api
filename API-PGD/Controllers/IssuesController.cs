using API_PGD.Models;
using API_PGD.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PGD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueRepositorie _issueRepositorie;

        public IssuesController(IssueRepositorie taskRepositorie)
        {
            _issueRepositorie = taskRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetIssueId(Guid id)
        {
            try
            {
                List<Issue> lstTasks = _issueRepositorie.GetIssueId(id);
                return Ok(lstTasks);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAllIssues(string? stageId)
        {
            
            try
            {
                List<Issue> lstTasks = _issueRepositorie.GetAllIssues(stageId);
                return Ok(lstTasks);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult RegisterIssue([FromBody] Issue task)
        {
            try
            {
                object result = _issueRepositorie.InsertIssue(task);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public  ActionResult UpdateIssue([FromBody] Issue task)
        {
            try
            {
                string result = _issueRepositorie.UpdateIssue(task);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteIssue(Guid id)
        {
            try
            {
                _issueRepositorie.DeleteIssue(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
