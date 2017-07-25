using System;

namespace TheWorld.Models
{
    public class Stop : BaseEntity
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; }
        public DateTime Arival { get; set; }
    }
}