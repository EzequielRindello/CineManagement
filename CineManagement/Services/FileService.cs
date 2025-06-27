using CineManagement.Models;

namespace CineManagement.Services
{
    public class FileService
    {
        private readonly string _projectRoot;
        private readonly string _moviesFilePath;
        private readonly string _directorsFilePath;

        public FileService()
        {
            // determine the project root directory dynamically
            // adjust as necessary for your project structure
            _projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            // construct the file paths
            _moviesFilePath = Path.Combine(_projectRoot, "Data", "movies.txt");
            _directorsFilePath = Path.Combine(_projectRoot, "Data", "directors.txt");
        }

        public List<Director> LoadDirectors()
        {
            var directors = new List<Director>();
            try
            {
                var lines = File.ReadAllLines(_directorsFilePath);
                int directorId = 1;
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        directors.Add(new Director(directorId++, line.Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading Directors: {ex.Message}");
            }
            return directors;
        }

        public List<Movie> LoadMovies(List<Director> directors)
        {
            const string InternationalKeyword = "INTERNATIONAL";
            var movies = new List<Movie>();
            try
            {
                var lines = File.ReadAllLines(_moviesFilePath);
                int movieId = 1;

                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split('|');
                        if (parts.Length < 2)
                        {
                            Console.WriteLine($"Invalid movie format: {line}. Expected format: 'Movie Name | Director ID [| INTERNATIONAL]'");
                            continue;
                        }

                        var movieName = parts[0].Trim();

                        if (!int.TryParse(parts[1].Trim(), out int directorId))
                        {
                            Console.WriteLine($"Invalid director ID for movie '{movieName}': {parts[1]}");
                            continue;
                        }

                        // verify if the director exists
                        if (!directors.Any(d => d.Id == directorId))
                        {
                            Console.WriteLine($"Director with ID {directorId} not found for movie '{movieName}'");
                            continue;
                        }

                        // verify if it is international (it can be in position 2 or 3)
                        var isInternational = parts.Length > 2 &&
                            parts.Skip(2).Any(p => p.Trim().Equals(InternationalKeyword, StringComparison.OrdinalIgnoreCase));

                        movies.Add(new Movie(movieId++, movieName, isInternational, directorId));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading Movies: {ex.Message}");
            }
            return movies;
        }
    }
}