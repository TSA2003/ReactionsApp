﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionsApp.Models
{
    public class AuthResponseModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}
