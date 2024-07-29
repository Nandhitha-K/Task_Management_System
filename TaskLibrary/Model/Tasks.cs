using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TaskLibrary.Model
{
        public class Tasks
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [DisplayName("Task ID")]
            public int TaskId { get; set; }

            [Required]
            [ForeignKey("Users")]
            [DisplayName("User ID")]
            public int UserId { get; set; }

            [StringLength(30)]
            [DisplayName("Task Title")]
            public string TaskTitle { get; set; }

            [DisplayName("Start Date")] 
            public DateTime? StartDate { get; set; }

            [DisplayName("End Date")]
            public DateTime? EndDate { get; set; }

            [DisplayName("Description")]
            [StringLength(50)]
            public string Description { get; set; }
            
            [Required]
            [DisplayName("Status Type")]
            public string StatusType { get; set; }
            public Users? Users { get; set; }

        }
}
