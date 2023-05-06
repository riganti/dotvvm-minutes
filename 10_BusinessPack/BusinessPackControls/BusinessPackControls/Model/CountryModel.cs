using System.Collections.Generic;

namespace BusinessPackControls.Model
{
    public class CountryModel
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string FlagUrl { get; set; }


        public static List<CountryModel> AllCountries => new List<CountryModel>()
        {
            new CountryModel() { Id = 1, Name = "Czech Republic", FlagUrl = "https://flagcdn.com/w40/cz.png" },
            new CountryModel() { Id = 2, Name = "United Kingdom", FlagUrl = "https://flagcdn.com/w40/gb.png" },
            new CountryModel() { Id = 3, Name = "United States", FlagUrl = "https://flagcdn.com/w40/us.png" },
            new CountryModel() { Id = 4, Name = "France", FlagUrl = "https://flagcdn.com/w40/fr.png" }
        };
    }
}
