using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace Sample
{
    public class Movie
    {

        private string name;
        public string Name { get; set; }

        private int id;
        public int Id { get; set; }

        private string ratingNumber;
        public string RatingNumber { get; set; }

        private BitmapSource _bitmap;
        public BitmapSource Bitmap
        {
            get; set;
        }

        private double rating;
        public double Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                rating = rating / 2;
            }
        }


    }

    public class MainMovie : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value, "Name"); }
        }

        private string ratingNumber;
        public string RatingNumber { get { return ratingNumber; } set { SetField(ref ratingNumber, value, "RatingNumber"); } }


        private double rating;
        public double Rating
        {
            get
            {
                return rating;
            }
            set
            {
                SetField(ref rating, value, "Rating");
                rating = rating / 2;
            }
        }

        private string reviewCount;

        public string ReviewCount
        {
            get
            {
                return reviewCount;
            }
            set
            {
                SetField(ref reviewCount, value, "ReviewCount");
            }
        }


        private string releaseYear;

        public string ReleaseYear
        {
            get
            {
                return releaseYear;
            }
            set
            {
                SetField(ref releaseYear, value, "ReleaseYear");
            }
        }

        private string duration;

        public string Duration
        {
            get
            {
                return duration;
            }
            set
            {
                SetField(ref duration, value, "Duration");
            }
        }


        private string overview;

        public string Overview
        {
            get
            {
                return overview;
            }
            set
            {
                SetField(ref overview, value, "Overview");
            }
        }

        private string trailerLink;

        public string TrailerLink
        {
            get
            {
                return trailerLink;
            }
            set
            {
                SetField(ref trailerLink, value, "TrailerLink");
            }
        }

        private BitmapSource _bitmap;
        public BitmapSource Bitmap
        {
            get { return _bitmap; }
            set { SetField(ref _bitmap, value, "Bitmap"); }
        }

        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                SetField(ref id, value, "Id");
            }
        }

    }
    public class Service
    {
        public static MainMovie MainMovieee;
        public static FastObservableCollection<Movie> PopularMovies = new FastObservableCollection<Movie>();
        public static FastObservableCollection<Movie> TopRatedMovies = new FastObservableCollection<Movie>();
        public static FastObservableCollection<Movie> UpComingMovies = new FastObservableCollection<Movie>();
        public static FastObservableCollection<Movie> NowPlayingMovies = new FastObservableCollection<Movie>();
        public static TMDbClient client = new TMDbClient("35eadc5a50894652fbf75ac8333b1991");
        public static async Task GetPopularMoviesAsync(string language, int page)
        {
            using (FastObservableCollection<Movie> iDelayed = PopularMovies.DelayNotifications())
            {
                PopularMovies.Clear();
                var popularMovies = await client.GetMoviePopularListAsync(language, page);
                foreach (var movie in popularMovies.Results)
                {
                    Movie mov = new Movie()
                    {
                        Bitmap = GetImage(client.GetImageUrl("w500", movie.PosterPath).AbsoluteUri),
                        Id = movie.Id,
                        Name = movie.Title,
                        Rating = movie.VoteAverage,
                        RatingNumber = movie.VoteAverage.ToString(),
                    };
                    if (PopularMovies.Any(x => x.Id == movie.Id))
                    {

                    }
                    else
                    {

                        iDelayed.Add(mov);
                    }
                }
            }
        }
        public static async Task GetTopRatedMovies(string language, int page)
        {
            using (FastObservableCollection<Movie> iDelayed = TopRatedMovies.DelayNotifications())
            {
                TopRatedMovies.Clear();
                var topRatedMovs = await client.GetMovieTopRatedListAsync(language, page);
                foreach (var movie in topRatedMovs.Results)
                {
                    Movie mov = new Movie()
                    {
                        Bitmap = GetImage(client.GetImageUrl("w500", movie.PosterPath).AbsoluteUri),
                        Id = movie.Id,
                        Name = movie.Title,
                        Rating = movie.VoteAverage,
                        RatingNumber = movie.VoteAverage.ToString(),
                    };
                    if (TopRatedMovies.Any(x => x.Id == movie.Id))
                    {

                    }
                    else
                    {
                        iDelayed.Add(mov);
                    }
                }
            }
        }
        public static async Task GetUpComingMovies(string language, int page)
        {
            using (FastObservableCollection<Movie> iDelayed = UpComingMovies.DelayNotifications())
            {
                UpComingMovies.Clear();
                var upComingMovs = await client.GetMovieUpcomingListAsync(language, page);
                foreach (var movie in upComingMovs.Results)
                {
                    Movie mov = new Movie()
                    {
                        Bitmap = GetImage(client.GetImageUrl("w500", movie.PosterPath).AbsoluteUri),
                        Id = movie.Id,
                        Name = movie.Title,
                        Rating = movie.VoteAverage,
                        RatingNumber = movie.VoteAverage.ToString(),
                    };
                    if (UpComingMovies.Any(x => x.Id == movie.Id))
                    {

                    }
                    else
                    {
                        iDelayed.Add(mov);
                    }
                }
            }
        }
        public static async Task GetNowPlayingMovies(string language, int page)
        {
            using (FastObservableCollection<Movie> iDelayed = NowPlayingMovies.DelayNotifications())
            {
                NowPlayingMovies.Clear();
                var nowPlayingMovs = await client.GetMovieNowPlayingListAsync(language, page);
                foreach (var movie in nowPlayingMovs.Results)
                {
                    Movie mov = new Movie()
                    {
                        Bitmap = GetImage(client.GetImageUrl("w500", movie.PosterPath).AbsoluteUri),
                        Id = movie.Id,
                        Name = movie.Title,
                        Rating = movie.VoteAverage,
                        RatingNumber = movie.VoteAverage.ToString(),
                    };
                    if (NowPlayingMovies.Any(x => x.Id == movie.Id))
                    {

                    }
                    else
                    {
                        iDelayed.Add(mov);
                    }
                }
            }
        }

        public static BitmapImage GetImage(string url)
        {
            var bitmapImage = new BitmapImage(new Uri(url, UriKind.Absolute));
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.CreateOptions = BitmapCreateOptions.DelayCreation;
            return bitmapImage;

        }

        public static async Task GetMainMovieDetail(string language)
        {
            if (PopularMovies.Count > 0)
            {
                var mainPopularMovie = PopularMovies.FirstOrDefault();
                var mainMovie = await client.GetMovieAsync(mainPopularMovie.Id, language);

                var MainMovieMov = new MainMovie()
                {
                    Bitmap = GetImage(client.GetImageUrl(client.Config.Images.BackdropSizes[client.Config.Images.BackdropSizes.Count - 2], mainMovie.BackdropPath).AbsoluteUri),
                    Name = mainMovie.Title,
                    Duration = mainMovie.Runtime.ToString() + " min",
                    Overview = mainMovie.Overview,
                    Rating = mainMovie.VoteAverage,
                    RatingNumber = mainMovie.VoteAverage.ToString(),
                    ReleaseYear = mainMovie.ReleaseDate.Value.Year.ToString(),
                    ReviewCount = mainMovie.VoteCount.ToString() + " Reviews",
                    Id = mainMovie.Id
                };
                MainMovieee = MainMovieMov;
            }
            else
            {
                var popularMovies = await client.GetMoviePopularListAsync(language, 1);
                var mainPopularMovie = popularMovies.Results.FirstOrDefault();
                var mainMovie = await client.GetMovieAsync(mainPopularMovie.Id, language);

                var MainMovieMov = new MainMovie()
                {
                    Bitmap = GetImage(client.GetImageUrl(client.Config.Images.BackdropSizes[client.Config.Images.BackdropSizes.Count - 2], mainMovie.BackdropPath).AbsoluteUri),
                    Name = mainMovie.Title,
                    Duration = mainMovie.Runtime.ToString() + " min",
                    Overview = mainMovie.Overview,
                    Rating = mainMovie.VoteAverage,
                    RatingNumber = mainMovie.VoteAverage.ToString(),
                    ReleaseYear = mainMovie.ReleaseDate.Value.Year.ToString(),
                    ReviewCount = mainMovie.VoteCount.ToString() + " Reviews",
                    Id = mainMovie.Id
                };

                MainMovieee = MainMovieMov;
            }
        }
    }
}
