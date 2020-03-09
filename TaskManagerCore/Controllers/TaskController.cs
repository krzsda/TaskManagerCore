using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCore.Models;
using TaskManagerCore.Models.Interfaces;

namespace TaskManagerCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IMailService _mailService;

        public TaskController(ILogger<TaskController> logger, IMailService mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(TasksDataStore.Current.Tasks);
        }

        [HttpGet("{id}", Name = "GetTask")]
        public IActionResult GetTask(int id)
        {
            try
            {
                var task = TasksDataStore.Current.Tasks.FirstOrDefault(x => x.Id.Equals(id));
                if (task != null)
                {
                    return Ok(task);
                }

                _logger.LogInformation($"Task with id {id} wasn't found when accessing task.");
                return NotFound();

            }
            catch (Exception e)
            {
                _logger.LogCritical($"Exception while getting task with id {id}.", e);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody]TaskCreateDto task)
        {
            var maxTaskId = TasksDataStore.Current.Tasks.Max(x => x.Id);
            var newTask = new TaskDto(++maxTaskId, task.Type, task.Status, task.Name);
            TasksDataStore.Current.Tasks.Add(newTask);

            return CreatedAtRoute("GetTask",
                new { id = newTask.Id },
                newTask
                );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var taskToDelete = TasksDataStore.Current.Tasks.FirstOrDefault(x => x.Id.Equals(id));
            if (taskToDelete == null)
            {
                return NotFound();
            }

            TasksDataStore.Current.Tasks.Remove(taskToDelete);

            _mailService.Send("Task was deleted.",
                $"Task {taskToDelete.Name} with id {taskToDelete.Id} was deleted");

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody]TaskUpdateDto task)
        {
            var taskToUpdate = TasksDataStore.Current.Tasks.FirstOrDefault(x => x.Id.Equals(id));
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            foreach (var property in task.GetType().GetProperties())
            {
                taskToUpdate.GetType().GetProperty(property.Name).SetValue(taskToUpdate, property.GetValue(task));
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTask(int id, [FromBody]JsonPatchDocument<TaskUpdateDto> patchDoc)
        {
            var taskData = TasksDataStore.Current.Tasks.FirstOrDefault(x => x.Id.Equals(id));
            if (taskData == null)
            {
                return NotFound();
            }

            var taskToPatch = new TaskUpdateDto();

            patchDoc.ApplyTo(taskToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var property in taskToPatch.GetType().GetProperties())
            {
                if (property.GetValue(taskToPatch) != null)
                {
                    taskData.GetType().GetProperty(property.Name).SetValue(taskData, property.GetValue(taskToPatch));
                }
            }

            if (!TryValidateModel(taskToPatch))
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
