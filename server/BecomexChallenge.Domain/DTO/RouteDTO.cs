namespace BecomexChallenge.Domain.DTO
{
    public class RouteDTO
    {
        public CountryDTO From { get; set; }
        public CountryDTO To { get; set; }
    }

    public class RouteOrder
    {
        public int Order { get; set; }
        public CountryDTO Country { get; set; }
    }
}
