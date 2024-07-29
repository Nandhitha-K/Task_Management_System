using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Model
{
        public class TaskDbContext : DbContext
        {
            public TaskDbContext()
            {

            }
            public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
            {

            }
            public virtual DbSet<Users> Users { get; set; }
            public virtual DbSet<Tasks> Tasks { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB; database=TaskDB; integrated security=true");
            }
        }
}
