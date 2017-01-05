using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FogBugzPlanner.Client.Models
{
    public class Case
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentCase { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string[] Tags { get; set; }
    }
}
