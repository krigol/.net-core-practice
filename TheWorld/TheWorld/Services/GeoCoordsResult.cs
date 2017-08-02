namespace TheWorld.Services
{
    public class GeoCoordsResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}