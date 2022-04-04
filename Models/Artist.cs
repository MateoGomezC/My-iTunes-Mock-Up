using System;
using System.Collections.Generic;

namespace MyiTunes.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Purchases = new HashSet<Purchase>();
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
