﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Business.Dtos
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
