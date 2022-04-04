using System;
using System.Collections.Generic;

namespace MyiTunes.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public int Rate { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual User User { get; set; } = null!;

        public Rating(User user, Song song, int rate)
        {
            Song = song;
            SongId = song.Id;

            User = user;
            UserId = user.Id;

            Rate = rate;
        }

        public Rating()
        {

        }
    }
}
