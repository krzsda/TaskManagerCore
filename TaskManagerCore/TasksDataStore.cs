using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCore.Models;

namespace TaskManagerCore
{
    public class TasksDataStore
    {
        public static TasksDataStore Current { get; } = new TasksDataStore();

        public List<TaskDto> Tasks { get; set; }

        public TasksDataStore()
        {
            Tasks = new List<TaskDto>()
            {
                new TaskDto(1, "typ1", "start", "Test1"),
                new TaskDto(2, "typ2", "stop", "Test2"),
                new TaskDto(3, "typ3", "In progress", "Test3"),
            };
        }
    }
}
