using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HaasChat
{
    internal class DBChat
    {
        FirebaseClient client;

        public DBChat()
        {
            client = new FirebaseClient("https://haaschat-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<List<ChatRoom>> GetAllChat(User _user)
        {
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>()).Where(x => _user.chats.Contains(x.Object.Name)).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name
            }).ToList();
        }
        public async Task<List<ChatRoom>> GetAllChat()
        {
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>()).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name
            }).ToList();
        }
        public async Task<User> getUser(string _username)
        {
            return (await client.Child("HaasChatAppUser").OnceAsync<User>()).Where(x => x.Object.UserName==_username).Select((item) => new User
            {
                UserName= item.Object.UserName,
                chats=item.Object.chats
            }).ToList()[0];
        }
        public async Task newUser(User user)
        {
            await client.Child("HaasChatAppUser").PostAsync(user);
        }
        public async Task saveChat(ChatRoom newChat)
        {
            await client.Child("HaasChatApp").PostAsync(newChat);
        }
        public async Task SendMessage(Chat ch, string _room)
        {
            await client.Child("HaasChatApp/" + _room + "/Message").PostAsync<Chat>(ch);
        }
        public ObservableCollection<Chat> chats(string _room)
        {
            return client.Child("HaasChatApp/" + _room + "/Message").AsObservable<Chat>().AsObservableCollection<Chat>();
        }
    }
}
