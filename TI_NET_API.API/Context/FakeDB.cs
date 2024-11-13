
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using TI_NET_API.API.Models;

namespace TI_NET_API.API.Context
{
    public static class FakeDB
    {

        public static int IdCount = 3;

        public static List<Movie> Movies = new List<Movie>{
            new Movie(1, "Pulp Fiction", "Adrenaline Baby !"," Quentin Tarantino", new DateTime(1994,10,14)),
            new Movie(2, "Big Fish", "It's not really about fish", "Tim Burton", new DateTime(2003, 03, 03)),
            new Movie(3, "Star Wars : The Phantom Menace", "Jar Jar Binks", "Georges Lucas", new DateTime(1999, 10,13))
        };


        
        
    }
}
