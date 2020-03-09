using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCore.Models.Interfaces;

namespace TaskManagerCore.Models
{
    public class TaskDto : ITaskChange
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }

        public TaskDto(int id, string type, string status, string name)
        {
            this.Id = id;
            this.Type = type;
            this.Status = status;
            this.Name = name;
        }
    }
}
