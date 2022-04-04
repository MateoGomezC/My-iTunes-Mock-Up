using System;
using System.Collections.Generic;

namespace MyiTunes.Models
{
    public partial class User
    {
        public User()
        {
            Purchases = new HashSet<Purchase>();
            Ratings = new HashSet<Rating>();
            Refunds = new HashSet<Refund>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public float? Wallet { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
