namespace CineManagement.Models
{
    public class MovieFunction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Price { get; set; }
        public Movie Movie { get; set; }
        public Director Director { get; set; }

        public MovieFunction(int id, DateTime date, TimeSpan time, decimal price, Movie movie, Director director)
        {
            Id = id;
            Date = date;
            Time = time;
            Price = price;
            Movie = movie;
            Director = director;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {Movie.Name} | Director: {Director.Name} | {Date:dd/MM/yyyy} {Time:hh\\:mm} | ${Price}";
        }

    }
}