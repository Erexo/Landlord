using System;

namespace Infrastructure.DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
