﻿using HMZ.Database.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace HMZ.Database.Entities
{
    public class RolePermission : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
        public Guid PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
