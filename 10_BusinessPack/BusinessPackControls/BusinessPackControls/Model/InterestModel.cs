using System.Collections.Generic;

namespace BusinessPackControls.Model
{
    public class InterestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public static List<InterestModel> AllInterests => new List<InterestModel>()
        {
            new InterestModel() { Id = 1, Name = "Computers" },
            new InterestModel() { Id = 2, Name = "Nature" },
            new InterestModel() { Id = 3, Name = "Sports" },
            new InterestModel() { Id = 4, Name = "Music" },
            new InterestModel() { Id = 5, Name = "Art" },
            new InterestModel() { Id = 6, Name = "TV" },
            new InterestModel() { Id = 7, Name = "Movies" },
            new InterestModel() { Id = 8, Name = "Travelling" },
            new InterestModel() { Id = 9, Name = "Shopping" },
            new InterestModel() { Id = 10, Name = "Books" }
        };
    }
}
