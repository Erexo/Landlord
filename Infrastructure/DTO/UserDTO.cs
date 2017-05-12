using Core.Domain;
using System;
using System.Collections.Generic;

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
        public IList<House> Houses { get; set; }
        public House Location { get; set; }
    }
}
