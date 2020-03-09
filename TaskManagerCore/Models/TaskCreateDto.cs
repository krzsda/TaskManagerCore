using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCore.Models.Interfaces;

namespace TaskManagerCore.Models
{
    public class TaskCreateDto : ITaskChange
    {
        [Required(ErrorMessage = "You should provide a Type")]
        [MaxLength(20)]
        public string Type { get; set; }

        [Required(ErrorMessage = "You should provide a Status")]
        [MaxLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "You should provide a Name")]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
