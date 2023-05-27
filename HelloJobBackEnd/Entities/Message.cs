using HelloJobBackEnd.Entities.Base;

namespace HelloJobBackEnd.Entities
{
    public class Message : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
