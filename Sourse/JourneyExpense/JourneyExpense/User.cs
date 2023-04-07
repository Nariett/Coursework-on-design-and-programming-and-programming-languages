using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyExpense
{
    class User
    {
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public User() { }
        public User(string login, string password, string name, string surname)
        {
            this.login = login;
            this.password = password;
            this.name = name;
            this.surname = surname;
        }
    }
}
