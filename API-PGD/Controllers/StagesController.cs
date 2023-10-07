using API_PGD.Models;
using API_PGD.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API_PGD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StagesController : ControllerBase
    {
        private readonly StageRepositorie _stageRepositorie;

        public StagesController(StageRepositorie stageRepositorie)
        {
            _stageRepositorie = stageRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetStageId(Guid id)
        {
            try
            {
                List<Stage> lstStages = _stageRepositorie.GetStageId(id);
                return Ok(lstStages);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpGet]
        public ActionResult GetAllStages()
        {
            try
            {
                List<Stage> lstStages = _stageRepositorie.GetAllStages();
                return Ok(lstStages);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost]
        public ActionResult RegisterStage([FromBody] Stage stage)
        {
            try
            {
                object result = _stageRepositorie.InsertStage(stage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateStage([FromBody] Stage stage)
        {
            try
            {
                string result = _stageRepositorie.UpdateStage(stage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStage(Guid id)
        {
            try
            {
                _stageRepositorie.DeleteStage(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
