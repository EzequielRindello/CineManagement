namespace CineManagement.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInternational { get; set; }
        public int DirectorId { get; set; }

        public Movie(int id, string name, bool isInternational, int directorId)
        {
            Id = id;
            Name = name;
            IsInternational = isInternational;
            DirectorId = directorId;
        }

        public override string ToString()
        {
            return $"{Name} ({(IsInternational ? "Internacional" : "Nacional")})";
        }

    }
}