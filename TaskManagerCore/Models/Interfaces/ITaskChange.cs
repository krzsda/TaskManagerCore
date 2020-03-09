using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerCore.Models.Interfaces
{
    interface ITaskChange
    {
        string Type { get; set; }
        string Status { get; set; }
        string Name { get; set; }
    }
}
