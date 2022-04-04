using MyiTunes.Models;

namespace MyiTunes.ViewModels
{
    public class SongsTopsDetailsViewModel
    {
        public List<Song> TopSongs { get; set; }
        public List<Artist> TopArtist { get; set; }
        public List<Song> TopRated { get; set; }

        public SongsTopsDetailsViewModel(List<Song> topSongs, List<Artist> topArtist, List<Song> topRated)
        {
            TopSongs = topSongs;
            TopArtist = topArtist;
            TopRated = topRated;
        }
    }
}
