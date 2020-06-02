using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Users
    {
        public Users()
        {
            UserChannels = new HashSet<UserChannels>();
            UserSharings = new HashSet<UserSharings>();
        }

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
        public bool IsMentor { get; set; }
        public string AvatarUrl { get; set; }
        public int Balance { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual ICollection<UserChannels> UserChannels { get; set; }
        public virtual ICollection<UserSharings> UserSharings { get; set; }
    }
}
