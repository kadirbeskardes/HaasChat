using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HaasChat.Model
{
    internal class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> chats { get; set; }
    }
}
