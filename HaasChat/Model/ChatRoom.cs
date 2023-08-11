using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HaasChat.Model
{
    internal class ChatRoom
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public ObservableCollection<string> Admins { get; set; }

        public ObservableCollection<string> Partpicatinas { get; set; }

    }
}
