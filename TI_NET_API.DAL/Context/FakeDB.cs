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
                new User(1,"Doe","Jane","janedoe@mail.com","$argon2id$v=19$m=65536,t=3,p=1$xdRwMHkXjTqnSXPHBcU4VQ$MmMeNWoh87pSXftXMRB1EFx3Q9bmHnmVnkGrS4R5fE8",Role.Admin),
                new User(2,"Doe", "John","johndoe@mail.com", "$argon2id$v=19$m=65536,t=3,p=1$ACxIqFNX9+dSEqyybv5scQ$6j2sfmNbtHDNmJQXOUfw5TdiI+ihdo1xICdszLm9Dgk", Role.Moderator),
                new User(3,"Smith", "John","johnsmith@mail.com", "$argon2id$v=19$m=65536,t=3,p=1$S0gY0lvJ0+vgEdGjahjHFw$pZ00OWuCAXHvEevA5PIlGdgW5f1Jc0SpkJbKXIyaqpA", Role.User)
            };

            IdUserCount = 3;
        }
    };

}
