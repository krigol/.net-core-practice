using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public class Trip : BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }

        public ICollection<Stop> Stops { get; set; }
    }
}
