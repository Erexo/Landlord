using Core.Domain;
using System;
using System.Collections.Generic;

namespace Infrastructure.DTO
{
    public class ExtendedUserDTO : UserDTO
    {
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Email { get; set; }
        public IList<House> Houses { get; set; }
    }
}
