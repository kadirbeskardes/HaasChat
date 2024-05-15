using System;

namespace HaasChat.Model
{
    internal class Chat
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string ImageURL { get; set; }
        public string VideoURL { get; set; }
        public DateTime Date { get; set; }
        public string FileURL { get; set; }
        public string FileName { get; set; }
    }
}