using CineManagement.Models;

namespace CineManagement.Services
{
    public class FunctionService
    {
        private readonly List<MovieFunction> _functions;
        private readonly ValidationService _validationService;
        private int _nextId;

        public FunctionService()
        {
            _functions = new List<MovieFunction>();
            _validationService = new ValidationService();
            _nextId = 1;
        }

        public List<MovieFunction> GetAllFunctions() => _functions;

        #region Validation Methods
        public bool ValidateDirectorDailyLimit(Director director, DateTime date)
        {
            var count = _functions
                .Where(f => f.Director.Name == director.Name && f.Date.Date == date.Date)
                .Count();

            return count < 10;
        }

        private bool ValidateDirectorDailyLimit(Director director, DateTime date, List<MovieFunction> functions)
        {
            var count = functions
                .Where(f => f.Director.Name == director.Name && f.Date.Date == date.Date)
                .Count();

            return count < 10;
        }

        public bool ValidateMovieFunctionLimit(Movie movie)
        {
            if (!movie.IsInternational) return true;

            var count = _functions
                .Where(f => f.Movie.Name == movie.Name)
                .Count();

            return count < 8;
        }

        private bool ValidateMovieFunctionLimit(Movie movie, List<MovieFunction> functions)
        {
            if (!movie.IsInternational) return true;

            var count = functions
                .Where(f => f.Movie.Name == movie.Name)
                .Count();

            return count < 8;
        }
        #endregion

        #region ABM Methods
        public bool CreateFunction(DateTime date, TimeSpan time, decimal price, Movie movie, Director director)
        {
            var functionDateTime = date.Add(time);

            if (!ValidationService.ValidateDateTime(functionDateTime))
            {
                Console.WriteLine("\nError: Date should be in the future.");
                return false;
            }

            if (!ValidationService.ValidatePrice(price))
            {
                Console.WriteLine("Error: Price must be greater than 0.");
                return false;
            }

            if (!ValidateDirectorDailyLimit(director, date))
            {
                Console.WriteLine($"Error: Director {director.Name} already has 10 functions on {date:dd/MM/yyyy}.");
                return false;
            }

            if (!ValidateMovieFunctionLimit(movie))
            {
                Console.WriteLine($"Error: Movie {movie.Name} already has 8 functions assigned.");
                return false;
            }

            var function = new MovieFunction(_nextId++, date, time, price, movie, director);
            _functions.Add(function);
            Console.WriteLine("Function created successfully.");
            return true;
        }

        public bool UpdateFunction(int id, DateTime date, TimeSpan time, decimal price, Movie movie, Director director)
        {
            var function = _functions.FirstOrDefault(f => f.Id == id);
            if (function == null)
            {
                Console.WriteLine("Error: Function not found.");
                return false;
            }

            var tempFunctions = _functions.Where(f => f.Id != id).ToList();
            var functionDateTime = date.Add(time);

            if (!ValidationService.ValidateDateTime(functionDateTime))
            {
                Console.WriteLine("Error: Date should be in the future.");
                return false;
            }

            if (!ValidationService.ValidatePrice(price))
            {
                Console.WriteLine("Error: Price must be greater than 0.");
                return false;
            }

            if (!ValidateDirectorDailyLimit(director, date, tempFunctions))
            {
                Console.WriteLine($"Error: Director {director.Name} already has 10 functions on {date:dd/MM/yyyy}.");
                return false;
            }

            if (!ValidateMovieFunctionLimit(movie, tempFunctions))
            {
                Console.WriteLine($"Error: Movie {movie.Name} already has 8 functions assigned.");
                return false;
            }

            function.Date = date;
            function.Time = time;
            function.Price = price;
            function.Movie = movie;
            function.Director = director;

            Console.WriteLine("Function updated successfully.");
            return true;
        }

        public bool DeleteFunction(int id)
        {
            var function = _functions.FirstOrDefault(f => f.Id == id);
            if (function == null)
            {
                Console.WriteLine("Error: Function not found.");
                return false;
            }

            _functions.Remove(function);
            Console.WriteLine("Function deleted successfully.");
            return true;
        }

        public MovieFunction? GetFunctionById(int id)
        {
            return _functions.FirstOrDefault(f => f.Id == id);
        }
        #endregion
    }
}
