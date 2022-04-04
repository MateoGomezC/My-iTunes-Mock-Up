using System;
using System.Collections.Generic;

namespace MyiTunes.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public int ArtistId { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Artist Artist { get; set; } = null!;

        public Purchase(Song song, User user, Artist artist)
        {
            Artist = artist;
            ArtistId = artist.Id;

            User = user;
            UserId = user.Id;

            Song = song;
            SongId = song.Id;

            Amount = song.Price;

            TransactionDate = DateTime.Now;
        }

        public Purchase()
        {

        }
    }
}
