using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunningActivitiesController : ControllerBase
    {
        private readonly IRunningActivityService _runningActivityService;

        public RunningActivitiesController(IRunningActivityService runningActivityService)
        {
            _runningActivityService = runningActivityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RunningActivity>>> GetRunningActivities()
        {
            var runningActivities = await _runningActivityService.GetRunningActivitiesAsync();
            return Ok(runningActivities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RunningActivity>> GetRunningActivity(int id)
        {
            var runningActivity = await _runningActivityService.GetRunningActivityByIdAsync(id);
            if (runningActivity == null)
            {
                return NotFound();
            }
            return Ok(runningActivity);
        }

        [HttpPost]
        public async Task<ActionResult<RunningActivity>> PostRunningActivity(RunningActivity runningActivity)
        {
            var createdRunningActivity = await _runningActivityService.CreateRunningActivityAsync(runningActivity);
            return CreatedAtAction(nameof(GetRunningActivity), new { id = createdRunningActivity.Id }, createdRunningActivity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRunningActivity(int id, RunningActivity runningActivity)
        {
            if (id != runningActivity.Id)
            {
                return BadRequest();
            }
            await _runningActivityService.UpdateRunningActivityAsync(runningActivity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunningActivity(int id)
        {
            await _runningActivityService.DeleteRunningActivityAsync(id);
            return NoContent();
        }
    }
}
