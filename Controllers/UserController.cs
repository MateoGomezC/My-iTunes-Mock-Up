using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyiTunes.Models;

namespace MyiTunes.Controllers
{
    public class UserController : Controller
    {
        iTunesContext db;

        public UserController()
        {
            db = new iTunesContext();
        }


        public ActionResult Index1()
        {
            return View();
        }
        // GET: UserController
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult TopUpWallet(int? userId, int? amount)
        {
            ViewBag.UserId = userId;

            if (userId != null && amount != null)
            {
                try
                {
                    User user = db.Users.First(u => u.Id == userId);
                    user.Wallet += amount;
                    ViewBag.WalletAmount = user.Wallet;
                    ViewBag.Amount = amount;
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }
            db.SaveChanges();
            return View();
        }

        public ActionResult BuySong(int userId, int? songId)
        {
            List<Song> songs = db.Songs.Include(s => s.Purchases).ThenInclude(u => u.User).ToList();
            User userSelected = db.Users.First(u => u.Id == userId);

            if (songId != null)
            {
                Song songSelected = db.Songs.First(s => s.Id == songId);
                Artist artistSelected = db.Artists.First(a => a.Id == songSelected.ArtistId);
                if (userSelected.Wallet >= songSelected.Price)
                {
                    Purchase newPurchase = new Purchase(songSelected, userSelected, artistSelected);
                    userSelected.Wallet -= songSelected.Price;
                    songSelected.Purchases.Add(newPurchase);
                    userSelected.Purchases.Add(newPurchase);
                    artistSelected.Purchases.Add(newPurchase);

                    db.SaveChanges();

                }
                else
                {
                    ViewBag.NotMoney = true;
                }
            }


            ViewBag.UserId = userId;
            return View(songs);
        }

        public ActionResult SongsByUser(int? userId)
        {
            ViewBag.Users = new SelectList(db.Users, "Id", "FullName");
            ViewBag.UserId = userId;

            if (userId != null)
            {
                User user = db.Users.Include(p => p.Purchases).ThenInclude(p => p.Song).ThenInclude(s => s.Artist).First(u => u.Id == userId);
                var purchases = user.Purchases.OrderBy(p => p.Song.Artist.FullName).GroupBy(p => p.Song.Artist);
                return View(purchases);
            }
            return View();
        }

        public ActionResult RateSong(int? userId)
        {
            ViewBag.UserId = userId;
            if (userId != null)
            {
                try
                {
                    User user = db.Users.Include(u => u.Purchases).ThenInclude(u => u.Song).ThenInclude(u => u.Artist).First(u => u.Id == userId);
                    var songs = user.Purchases;
                    return View(songs);
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult RateSongs(int? userId, int? songId, int? rate)
        {
            if (userId != null && songId != null && rate != null)
            {
                if (rate <= 0 || rate > 10)
                {
                    return RedirectToAction("Error", new { message = "Rating must be between 1 to 10" });
                }
                else
                {
                    try
                    {
                        User user = db.Users.First(u => u.Id == userId);
                        Song song = db.Songs.Include(s => s.Ratings).First(s => s.Id == songId);

                        List<Rating> preexistingRating = song.Ratings.Where(s => s.UserId == userId).ToList();

                        if (preexistingRating.Any())
                        {
                            preexistingRating.First().Rate = (int)rate;
                            song.CalculateAverage();
                        }
                        else
                        {
                            Rating newRating = new Rating(user, song, (int)rate);
                            song.AddNewRating(newRating);
                            user.Ratings.Add(newRating);
                        }

                        db.SaveChanges();

                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Error", new { message = "Error adding new rating to database." });
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Refund(int? userId, int? songId)
        {
            if (userId != null && songId != null)
            {
                try
                {
                    User user = db.Users.First(u => u.Id == userId);
                    Song song = db.Songs.First(s => s.Id == songId);
                    Purchase purchase = db.Purchases.First(u => u.UserId == userId && u.SongId == songId);
                    int totalDays = (int)(DateTime.Now - purchase.TransactionDate).TotalDays;
                    if(totalDays <= 30)
                    {
                        db.Purchases.Remove(purchase);
                        song.Purchases.Remove(purchase);
                        user.Purchases.Remove(purchase);

                        Refund refund = new Refund(user, song, song.Price);
                        song.Refunds.Add(refund);
                        user.Refunds.Add(refund);

                        user.Wallet += song.Price;
                        db.SaveChanges();
                        return View();
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", new { message = "Error adding new rating to database." });
                }
            }
            return View();
        }

        public ActionResult Error(string? message)
        {
            if(message != null)
            {
                return View(message);
            }
            return View();
        }
    }
}
