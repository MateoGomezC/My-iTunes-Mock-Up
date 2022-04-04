using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyiTunes.Models;

namespace MyiTunes.Controllers
{
    public class ArtistController : Controller
    {
        iTunesContext db;

        public ArtistController()
        {
            db = new iTunesContext();
        }

        // GET: ArtistController
        public ActionResult Index()
        {
            var artists = db.Artists.Include(a => a.Songs);
            var songs = db.Songs.GroupBy(s => s.ArtistId);
            return View(artists);
        }

        public ActionResult SongsByArtist()
        {
            var songs = db.Songs.Include(s => s.Artist).Where(s => s.ArtistId == s.Artist.Id).GroupBy(s => s.Artist.Id);
            return View(songs);
        }

        public ActionResult SelectArtist(int? artistId)
        {

            ViewBag.Artists = new SelectList(db.Artists, "Id", "FullName");

            if (artistId != null)
            {
                try
                {
                    List<Song> songs = db.Songs.Include(p => p.Purchases).Where(s => s.ArtistId == artistId).ToList();
                    return View(songs);
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }
            return View();
        }
        // GET: ArtistController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArtistController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtistController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtistController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
