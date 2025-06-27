namespace CineManagement.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Director(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }

    }
}