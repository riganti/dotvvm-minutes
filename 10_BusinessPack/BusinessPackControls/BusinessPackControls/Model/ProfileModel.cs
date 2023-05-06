using System.Collections.Generic;

namespace BusinessPackControls.Model
{
    public class ProfileModel
    {
        public string FullName { get; set; }

        public int CountryId { get; set; }

        public List<int> InterestIds { get; set; } = new List<int>();

        public string ProfileImageUrl { get; set; }
    }
}
