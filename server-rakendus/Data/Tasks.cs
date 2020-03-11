using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool IsChecked { get; set; }
        public string UserId { get; set; }
    }
}
