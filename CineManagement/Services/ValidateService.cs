using CineManagement.Models;

namespace CineManagement.Services
{
    public class ValidationService
    {
        // validates the client requirements
        public bool ValidateDirectorDailyLimit(List<MovieFunction> functions, Director director, DateTime date)
        {
            // the maximum number of functions a director can have per day is 10
            var functionsForDirectorOnDate = functions
                .Where(f => f.Director.Name == director.Name && f.Date.Date == date.Date)
                .Count();

            return functionsForDirectorOnDate < 10;
        }

        public bool ValidateMovieFunctionLimit(List<MovieFunction> functions, Movie movie)
        {
            // no limit for national movies
            if (!movie.IsInternational) return true;

            // international movies can have a maximum of 8 functions per day
            var functionsForMovie = functions
                .Where(f => f.Movie.Name == movie.Name)
                .Count();

            return functionsForMovie < 8;
        }

        public bool ValidateDateTime(DateTime date, TimeSpan time)
        {
            // the function date and time must be in the future
            var functionDateTime = date.Add(time);
            return functionDateTime > DateTime.Now;
        }

        public bool ValidatePrice(decimal price)
        {
            // the price must be greater than 0
            return price > 0;
        }

    }
}