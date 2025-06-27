using CineManagement.Models;
using CineManagement.Services;
using System.Globalization;

namespace CineManagement.UI
{
    public class MenuService
    {
        #region Fields and Constructor
        private readonly FunctionService _functionService;
        private readonly List<Movie> _movies;
        private readonly List<Director> _directors;

        public MenuService(FunctionService functionService, List<Movie> movies, List<Director> directors)
        {
            _functionService = functionService;
            _movies = movies;
            _directors = directors;
        }
        #endregion

        #region Menu Methods
        public void ShowMainMenu()
        {
            // if the movies or directors list is empty, prompt the user to add them
            if (_movies == null || _movies.Count == 0 || _directors == null || _directors.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("\nNo movies/directors available.");
                Console.WriteLine("\nPlease add movies and directors first in the respective files located at: 'Data/movies.txt' and 'Data/directors.txt'");
                Console.WriteLine("\nExpected format for directors (one per line):");
                Console.WriteLine(" - Director Name");
                Console.WriteLine("Example:");
                Console.WriteLine("  Francis Ford Coppola");
                Console.WriteLine("\nExpected format for movies (one per line):");
                Console.WriteLine(" - National:       Movie Name | Director ID");
                Console.WriteLine(" - International:  Movie Name | Director ID | INTERNATIONAL");
                Console.WriteLine("Examples (assuming Francis Ford Coppola has ID 1):");
                Console.WriteLine("  - El Secreto de sus Ojos | 1");
                Console.WriteLine("  - Avatar | 1 | INTERNATIONAL");
                Console.WriteLine("\nNote: Director ID corresponds to the line number in directors.txt");
                Console.WriteLine();
                return;
            }

            // Main menu loop once the movies and directors are loaded
            while (true)
            {
                Console.Clear();
                Console.WriteLine("**** DEMO MOVIE FUNCTION MANAGEMENT SYSTEM ****");
                Console.WriteLine();
                Console.WriteLine("1. Create function");
                Console.WriteLine("2. Modify function");
                Console.WriteLine("3. Delete function");
                Console.WriteLine("4. View all functions");
                Console.WriteLine("5. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateFunction();
                        break;
                    case "2":
                        ModifyFunction();
                        break;
                    case "3":
                        DeleteFunction();
                        break;
                    case "4":
                        ShowAllFunctions();
                        break;
                    case "5":
                        Console.WriteLine("\nExiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateFunction()
        {
            Console.Clear();
            Console.WriteLine("**** CREATE FUNCTION ****");
            Console.WriteLine();

            var movie = SelectMovie();
            if (movie == null) return;

            // Automatically get the director based on the movie's DirectorId
            var director = GetDirectorById(movie.DirectorId);
            if (director == null)
            {
                Console.WriteLine($"Error: Director with ID {movie.DirectorId} not found for movie '{movie.Name}'.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nSelected director: {director.Name}");

            var date = GetDate();
            if (date == null) return;

            var time = GetTime();
            if (time == null) return;

            var price = GetPrice();
            if (price == null) return;

            _functionService.CreateFunction(date.Value, time.Value, price.Value, movie, director);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void ModifyFunction()
        {
            Console.Clear();
            Console.WriteLine("**** MODIFY FUNCTION ****");
            Console.WriteLine();

            ShowAllFunctions(false);

            Console.Write("\nEnter the ID of the function to modify: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var existingFunction = _functionService.GetFunctionById(id);
            if (existingFunction == null)
            {
                Console.WriteLine("Function not found. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nCurrent function: {existingFunction}");
            Console.WriteLine("\nEnter the new data:");

            var movie = SelectMovie();
            if (movie == null) return;

            // automatically get the director based on the movie's DirectorId
            // to avoid user confusion and human error
            var director = GetDirectorById(movie.DirectorId);
            if (director == null)
            {
                Console.WriteLine($"Error: Director with ID {movie.DirectorId} not found for movie '{movie.Name}'.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nSelected director: {director.Name}");

            var date = GetDate();
            if (date == null) return;

            var time = GetTime();
            if (time == null) return;

            var price = GetPrice();
            if (price == null) return;

            _functionService.UpdateFunction(id, date.Value, time.Value, price.Value, movie, director);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DeleteFunction()
        {
            Console.Clear();
            Console.WriteLine("**** DELETE FUNCTION ****");
            Console.WriteLine();

            ShowAllFunctions(false);

            Console.Write("\nEnter the ID of the function to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var function = _functionService.GetFunctionById(id);
            if (function == null)
            {
                Console.WriteLine("Function not found. Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nFunction to delete: {function}");
            Console.Write("Are you sure? (y/n): ");
            var confirm = Console.ReadLine()?.ToLower();

            if (confirm == "y" || confirm == "yes")
            {
                _functionService.DeleteFunction(id);
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void ShowAllFunctions(bool waitForKey = true)
        {
            if (waitForKey)
            {
                Console.Clear();
                Console.WriteLine("**** ALL FUNCTIONS ****");
                Console.WriteLine();
            }

            var functions = _functionService.GetAllFunctions();
            // order functions by date and time
            var orderedFunctions = functions.OrderBy(f => f.Date).ThenBy(f => f.Time);

            if (functions.Count > 0)
            {
                // display the ordered functions
                foreach (var function in orderedFunctions)
                {
                    Console.WriteLine(function);
                }
            }
            else
            {
                Console.WriteLine("No functions registered.");
            }

            if (waitForKey)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
        #endregion

        #region Input Methods
        private Movie? SelectMovie()
        {
            Console.WriteLine("Available movies:");
            for (int i = 0; i < _movies.Count; i++)
            {
                var director = GetDirectorById(_movies[i].DirectorId);
                var directorName = director?.Name ?? "Unknown Director";
                Console.WriteLine($"{i + 1}. {_movies[i]} - Director: {directorName}");
            }

            Console.Write("Select a movie: ");
            if (int.TryParse(Console.ReadLine(), out int movieIndex) &&
                movieIndex >= 1 && movieIndex <= _movies.Count)
            {
                return _movies[movieIndex - 1];
            }

            Console.WriteLine("Invalid selection. Press any key to continue...");
            Console.ReadKey();
            return null;
        }

        private static DateTime? GetDate()
        {
            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.Write("\nEnter the date (dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime InputDate))
                {
                    return InputDate;
                }

                Console.WriteLine("Invalid date. Try again.");
            }

            Console.WriteLine("Returning to menu. Press any key to continue...");
            Console.ReadKey();
            return null;
        }

        private static TimeSpan? GetTime()
        {
            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.Write("Enter the time (HH:mm): ");
                if (TimeSpan.TryParseExact(Console.ReadLine(), @"hh\:mm",
                    CultureInfo.InvariantCulture, out TimeSpan InputTime))
                {
                    return InputTime;
                }

                Console.WriteLine("Invalid time. Try again.");
            }

            Console.WriteLine("Returning to menu. Press any key to continue...");
            Console.ReadKey();
            return null;
        }

        private static decimal? GetPrice()
        {
            for (int attempts = 0; attempts < 3; attempts++)
            {
                Console.Write("Enter the price: $");
                if (decimal.TryParse(Console.ReadLine(), out decimal InputPrice))
                {
                    return InputPrice;
                }

                Console.WriteLine("Invalid price. Try again.");
            }

            Console.WriteLine("Returning to menu. Press any key to continue...");
            Console.ReadKey();
            return null;
        }
        #endregion

        #region Helper Methods
        private Director? GetDirectorById(int directorId)
        {
            return _directors.FirstOrDefault(d => d.Id == directorId);
        }
        #endregion
    }
}