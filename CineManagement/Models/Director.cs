namespace CineManagement.Models
{
    public class Director
    {
        public string Name { get; set; }

        public Director(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}