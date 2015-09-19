using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Message.WebAPI.Services
{
    public class User
    {

        public User(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
