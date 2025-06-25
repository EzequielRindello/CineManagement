using CineManagement.Models;

namespace CineManagement.Services
{
    public class FileService
    {
        private readonly string _projectRoot;
        private readonly string MoviesFilePath;
        private readonly string DirectorsFilePath;

        public FileService()
        {
            // determine the project root directory dynamically
            // Adjust as necessary for your project structure
            _projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            // construct the file paths
            MoviesFilePath = Path.Combine(_projectRoot, "Data", "movies.txt");
            DirectorsFilePath = Path.Combine(_projectRoot, "Data", "directors.txt");
        }

        public List<Movie> LoadMovies()
        {
            var movies = new List<Movie>();

            try
            {
                var lines = File.ReadAllLines(MoviesFilePath);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split('|');
                        var name = parts[0].Trim();
                        var isInternational = parts.Length > 1 ? bool.Parse(parts[1].Trim()) : true;
                        movies.Add(new Movie(name, isInternational));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading Movies: {ex.Message}");
            }

            return movies;
        }

        public List<Director> LoadDirectors()
        {
            var directors = new List<Director>();

            try
            {
                var lines = File.ReadAllLines(DirectorsFilePath);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        directors.Add(new Director(line.Trim()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading Directors: {ex.Message}");
            }

            return directors;
        }

    }
}
