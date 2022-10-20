using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ToDoTask : EntityBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EUrgency Urgency { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool Done { get; set; }
        public string Content { get; set; }
    }
}
