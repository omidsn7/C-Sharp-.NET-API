using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI
{
    public class SuperHero
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Place { get; set; } = String.Empty;
    }
}
