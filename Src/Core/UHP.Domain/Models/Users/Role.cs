﻿using System.Collections.Generic;

#nullable disable

namespace UHP.Domain.Models.Users
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
