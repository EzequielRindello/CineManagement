
using CineManagement.Services;
using CineManagement.UI;

namespace CineManagement
{
    public class CinemaManager
    {
        private FileService _fileService;
        private FunctionService _functionService;
        private MenuService _menuService;

        public CinemaManager()
        {
            _fileService = new FileService();
            _functionService = new FunctionService();

            var movies = _fileService.LoadMovies();
            var directors = _fileService.LoadDirectors();

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