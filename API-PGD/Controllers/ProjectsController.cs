using API_PGD.Models;
using API_PGD.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_PGD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectRepositorie _projectRepositorie;

        public ProjectsController(ProjectRepositorie projectRepositorie)
        {
            _projectRepositorie = projectRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetProjectId(Guid id)
        {
            try
            {
                List<Project> lstProjects = _projectRepositorie.GetProjectId(id);
                return Ok(lstProjects);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAllProjects()
        {
            try
            {
                List<Project> lstProjects = _projectRepositorie.GetAllProjects();
                return Ok(lstProjects);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost]
        public ActionResult RegisterProject([FromBody] Project project)
        {
            try
            {
                object result = _projectRepositorie.InsertProject(project);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateComment([FromBody] Project project)
        {
            try
            {
                string result = _projectRepositorie.UpdateProject(project);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComment(Guid id)
        {
            try
            {
                _projectRepositorie.DeleteProject(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
