using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class User
    {
        public User()
        {
            Mentee = new HashSet<Mentee>();
            Mentor = new HashSet<Mentor>();
        }

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int? YearOfBirth { get; set; }
        public string AvatarUrl { get; set; }
        public double? Balance { get; set; }
        public string Description { get; set; }
        public bool IsDisable { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Mentee> Mentee { get; set; }
        public virtual ICollection<Mentor> Mentor { get; set; }
    }
}
