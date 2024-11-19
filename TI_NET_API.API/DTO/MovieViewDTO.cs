namespace TI_NET_API.API.DTO
{
    public class MovieViewDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Synopsis { get; set; }
        public required string Director { get; set; }
        public DateTime Release { get; set; }
    }

    public class MovieListViewDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}
