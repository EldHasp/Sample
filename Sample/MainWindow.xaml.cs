using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sample.Annotations;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Service.client.GetConfigAsync();
            this.DataContext = this;
        }
        public FastObservableCollection<Movie> PopularMovies
        {
            get
            {
                return Service.PopularMovies;
            }
        }
        public FastObservableCollection<Movie> TopRatedMovies
        {
            get
            {
                return Service.TopRatedMovies;
            }
        }

        public FastObservableCollection<Movie> UpcomingMovies
        {
            get
            {
                return Service.UpComingMovies;
            }
        }

        public FastObservableCollection<Movie> NowPlayingMovies
        {
            get
            {
                return Service.NowPlayingMovies;
            }
        }
        private async Task GetMainMovieDetails()
        {
            await Service.GetMainMovieDetail("en");
            MainMovieTvShow.DataContext = Service.MainMovieee;
        }
        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var tasks = new List<Task>();
            if (Service.MainMovieee == null)
            {
                tasks.Add(GetMainMovieDetails());
            }
            else
            {
                MainMovieTvShow.DataContext = Service.MainMovieee;
            }
            if (Service.PopularMovies.Count == 0)
            {
                tasks.Add(Service.GetPopularMoviesAsync("en", 1));
            }

            if (Service.TopRatedMovies.Count == 0)
            {
                tasks.Add(Service.GetTopRatedMovies("en", 1));
            }
            var tasks2 = new List<Task>();

            if (Service.UpComingMovies.Count == 0)
            {
                tasks2.Add(Service.GetUpComingMovies("en", 1));
            }

            if (Service.NowPlayingMovies.Count == 0)
            {
                tasks2.Add(Service.GetNowPlayingMovies("en", 1));
            }

            await Task.WhenAll(tasks);
        }

        private void MainMovieTvShow_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ExploreMorePopularMovies_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ExploreMorePopularMovies_OnMouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void ExploreMorePopularMovies_OnMouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void PopularMoviesDisplay_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TopRatedMoviesDisplay_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void UpcomingMoviesDisplay_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void NowPlayingMoviesDisplay_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

    }
}