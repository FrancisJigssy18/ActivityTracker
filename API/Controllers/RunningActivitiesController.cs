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

        /// <summary>
        /// This C# function uses an HTTP GET request to retrieve a list of running activities asynchronously
        /// and returns them as a response.
        /// </summary>
        /// <returns>
        /// The GetRunningActivities method is returning a list of RunningActivity objects as an ActionResult.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IList<RunningActivity>>> GetRunningActivities()
        {
            var runningActivities = await _runningActivityService.GetRunningActivitiesAsync();
            return Ok(runningActivities);
        }

        /// <summary>
        /// This C# function retrieves a running activity by its ID asynchronously and returns it as an
        /// ActionResult.
        /// </summary>
        /// <param name="id">The `id` parameter in the `GetRunningActivity` method is used to specify the unique
        /// identifier of the running activity that the user wants to retrieve. This method is an HTTP GET
        /// endpoint that retrieves a running activity based on the provided `id`.</param>
        /// <returns>
        /// The GetRunningActivity method is returning an ActionResult of type RunningActivity. If the
        /// runningActivity is found, it will return an Ok response with the runningActivity object. If the
        /// runningActivity is not found (null), it will return a NotFound response.
        /// </returns>
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

        /// <summary>
        /// This C# function creates a new running activity and returns the created activity with a HTTP 201
        /// Created status.
        /// </summary>
        /// <param name="RunningActivity">The `CreateRunningActivity` method is a POST endpoint in a web API
        /// controller that creates a new running activity. The method takes a `RunningActivity` object as a
        /// parameter, which likely contains information about the running activity such as distance, duration,
        /// date, etc.</param>
        /// <returns>
        /// The CreateRunningActivity method is returning an ActionResult of type RunningActivity. It is
        /// creating a new RunningActivity object by calling the CreateRunningActivityAsync method from the
        /// _runningActivityService. Then, it returns a CreatedAtAction result with the created RunningActivity
        /// object and a reference to the GetRunningActivity method to retrieve the newly created
        /// RunningActivity by its ID.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<RunningActivity>> CreateRunningActivity(RunningActivity runningActivity)
        {
            var createdRunningActivity = await _runningActivityService.CreateRunningActivityAsync(runningActivity);
            return CreatedAtAction(nameof(GetRunningActivity), new { id = createdRunningActivity.Id }, createdRunningActivity);
        }

        /// <summary>
        /// This C# function updates a running activity with the specified ID.
        /// </summary>
        /// <param name="id">The `id` parameter in the `UpdatedRunningActivity` method represents the unique
        /// identifier of the running activity that is being updated. It is used to identify the specific
        /// running activity that needs to be updated in the system.</param>
        /// <param name="RunningActivity">The `RunningActivity` parameter in the `UpdatedRunningActivity` method
        /// represents the data that will be used to update an existing running activity. It likely contains
        /// properties such as Id, Distance, Duration, Date, and any other relevant information about the
        /// running activity. This parameter will be passed in the request</param>
        /// <returns>
        /// The method is returning an `IActionResult` type. If the `id` does not match the
        /// `runningActivity.Id`, it will return a BadRequest response. If the update operation is successful,
        /// it will return a NoContent response.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatedRunningActivity(int id, RunningActivity runningActivity)
        {
            if (id != runningActivity.Id)
            {
                return BadRequest();
            }
            await _runningActivityService.UpdateRunningActivityAsync(runningActivity);
            return NoContent();
        }

        /// <summary>
        /// This C# function deletes a running activity by its ID and returns a No Content response.
        /// </summary>
        /// <param name="id">The `id` parameter in the `DeleteRunningActivity` method is used to identify the
        /// specific running activity that needs to be deleted. This parameter is passed in the route of the
        /// HTTP request to specify which running activity should be deleted.</param>
        /// <returns>
        /// No content is being returned.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunningActivity(int id)
        {
            await _runningActivityService.DeleteRunningActivityAsync(id);
            return NoContent();
        }
    }
}
