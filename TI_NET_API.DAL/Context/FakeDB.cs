using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.DAL.Context
{
    public class FakeDB
    {

        public List<Movie> Movies { get; set; }
        public List<User> Users { get; set; }
        public int IdCount { get; set; }
        public int IdUserCount { get; set; }

        public FakeDB()
        {

            Movies = new List<Movie>{
                new Movie(1, "Pulp Fiction", "Adrenaline Baby !", " Quentin Tarantino", new DateTime(1994, 10, 14)),
                new Movie(2, "Big Fish", "It's not really about fish", "Tim Burton", new DateTime(2003, 03, 03)),
                new Movie(3, "Star Wars : The Phantom Menace", "Jar Jar Binks", "Georges Lucas", new DateTime(1999, 10, 13))
                };

            IdCount = 3;


            Users = new List<User>
            {
                new User(1,"Doe","Jane","janedoe@mail.com","Test1234=",Role.Admin),
                new User(2,"Doe", "John","johndoe@mail.com", "Test1234=", Role.Moderator),
                new User(3,"Smith", "John","johnsmith@mail.com", "Test1234=", Role.User)
            };

            IdUserCount = 3;
        }
    };

}
