using CineManagement.Models;

namespace CineManagement.Services
{
    public class FunctionService
    {
        private List<MovieFunction> _functions;
        private ValidationService _validationService;
        private int _nextId;

        public FunctionService()
        {
            _functions = new List<MovieFunction>();
            _validationService = new ValidationService();
            _nextId = 1;
        }

        public List<MovieFunction> GetAllFunctions()
        {
            return _functions.ToList();
        }

        public bool CreateFunction(DateTime date, TimeSpan time, decimal price, Movie movie, Director director)
        {
            if (!_validationService.ValidateDateTime(date, time))
            {
                Console.WriteLine("Error: Date should be future");
                return false;
            }

            if (!_validationService.ValidatePrice(price))
            {
                Console.WriteLine("Error: Price must be greater than 0.");
                return false;
            }

            if (!_validationService.ValidateDirectorDailyLimit(_functions, director, date))
            {
                Console.WriteLine($"Error: Director {director.Name} already has 10 functions on {date:dd/MM/yyyy}.");
                return false;
            }

            if (!_validationService.ValidateMovieFunctionLimit(_functions, movie))
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

            // create a temporary list excluding the function being updated
            var tempFunctions = _functions.Where(f => f.Id != id).ToList();

            if (!_validationService.ValidateDateTime(date, time))
            {
                Console.WriteLine("Error: Date should be future");
                return false;
            }

            if (!_validationService.ValidatePrice(price))
            {
                Console.WriteLine("Error: Price must be greater than 0.");
                return false;
            }

            if (!_validationService.ValidateDirectorDailyLimit(tempFunctions, director, date))
            {
                Console.WriteLine($"Error: Director {director.Name} already has 10 functions on {date:dd/MM/yyyy}.");
                return false;
            }

            if (!_validationService.ValidateMovieFunctionLimit(tempFunctions, movie))
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

        public MovieFunction GetFunctionById(int id)
        {
            return _functions.FirstOrDefault(f => f.Id == id);
        }

    }
}