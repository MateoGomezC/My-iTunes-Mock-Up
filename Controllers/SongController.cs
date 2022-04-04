using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyiTunes.Models;
using MyiTunes.ViewModels;

namespace MyiTunes.Controllers
{
    public class SongController : Controller
    {
        iTunesContext db;

        public SongController()
        {
            db = new iTunesContext();
        }
        // GET: SongController
        public ActionResult Index()
        {
            var songs = db.Songs.ToList();
            return View(songs);
        }

        public ActionResult Tops()
        {

            var TopSongs = db.Songs.OrderByDescending(p => p.Purchases.Count()).Take(3).ToList();
            var TopSellingArtist = db.Artists.OrderByDescending(p => p.Purchases.Count()).Take(3).ToList();
            var TopRated = db.Songs.OrderByDescending(s => s.AverageRating).Take(3).ToList();

            SongsTopsDetailsViewModel tops = new SongsTopsDetailsViewModel(TopSongs, TopSellingArtist, TopRated);

            return View(tops);
        }
 
    }
}
