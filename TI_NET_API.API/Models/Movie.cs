namespace TI_NET_API.API.Models
{
    public class Movie
    {
        public Movie()
        {
            
        }
        public Movie(int id, string title, string synopsis, string director, DateTime release)
        {
            Id = id;
            Title = title;
            Synopsis = synopsis;
            Director = director;
            Release = release;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Director { get; set; }
        public DateTime Release { get; set; }
    }
}
