namespace Domain.Models.UserModels
{
    public class UserInsertModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public int YearOfBirth { get; set; }
        public string AvatarUrl { get; set; }
    }
}
