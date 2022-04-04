using System;
using System.Collections.Generic;

namespace MyiTunes.Models
{
    public partial class Refund
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        public Refund(User user, Song song, float amount)
        {
            User = user;
            UserId = user.Id;

            Song = song;
            SongId = song.Id;

            Amount = amount;

            TransactionDate = DateTime.Now;
        }

        public Refund()
        {

        }
    }
}
