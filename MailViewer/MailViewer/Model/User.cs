using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailViewer.Model
{
    public class User
    {
        public User(){ }
        public User(string mailUrl, string login, string password)
        {
            this.mailUrl = mailUrl;
            this.login = login;
            this.password = password;
        }
        public string mailUrl { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
