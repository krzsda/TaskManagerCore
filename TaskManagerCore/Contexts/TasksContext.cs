using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerCore.Contexts
{
    public sealed class TasksContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public TasksContext(DbContextOptions<TasksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
