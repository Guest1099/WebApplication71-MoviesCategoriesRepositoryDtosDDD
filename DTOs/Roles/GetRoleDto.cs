﻿using System.Collections.Generic;
using WebApplication71.DTOs.Users;
using WebApplication71.Models;

namespace WebApplication71.DTOs.Roles
{
    public class GetRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }


        public List <GetUserDto> Users { get; set; }
    }
}
