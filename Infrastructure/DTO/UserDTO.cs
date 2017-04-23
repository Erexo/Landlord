using System;

namespace Infrastructure.DTO
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
