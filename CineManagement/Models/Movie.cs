namespace CineManagement.Models
{
    public class Movie
    {
        public string Name { get; set; }
        public bool IsInternational { get; set; }

        public Movie(string name, bool isInternational = true)
        {
            Name = name;
            IsInternational = isInternational;
        }

        public override string ToString()
        {
            return $"{Name} ({(IsInternational ? "Internacional" : "Nacional")})";
        }

    }
}