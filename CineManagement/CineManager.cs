using CineManagement.Services;
using CineManagement.UI;

namespace CineManagement
{
    public class CinemaManager
    {
        private readonly FileService _fileService;
        private readonly FunctionService _functionService;
        private readonly MenuService _menuService;

        public CinemaManager()
        {
            _fileService = new FileService();
            _functionService = new FunctionService();

            // load directors from file first
            var directors = _fileService.LoadDirectors();

            // upload movies passing the list of directors for validation
            var movies = _fileService.LoadMovies(directors);

            _menuService = new MenuService(_functionService, movies, directors);
        }

        public void Run()
        {
            try
            {
                _menuService.ShowMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}