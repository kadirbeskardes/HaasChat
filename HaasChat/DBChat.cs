using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HaasChat
{
    internal class DBChat
    {
        FirebaseClient client;

        public DBChat()
        {
            client = new FirebaseClient("https://haaschat-9a85d-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        /*public async Task<List<ChatRoom>> GetAllChat(User _user)
        {
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>()).Where(x => _user.chats.Contains(x.Object.Name)).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name
            }).ToList();
        }*/
        public async Task<List<ChatRoom>> GetAllChat(List<string> _chats)
        {
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>())
            .Where(x => _chats.Contains(x.Key)).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name
            }).ToList();
        }
        internal async Task<bool> isThereUser(string username, string email)
        {
            return (await client.Child("HaasChatAppUser" + "/" + username).OnceAsync<User>())
                .Any();
        }
        internal async Task<User> getUser(string _username)
        {
            return (await client.Child("HaasChatAppUser" + "/" + _username).OnceSingleAsync<User>());
        }
        public async Task<bool> newUser(User user)
        {
            await client.Child("HaasChatAppUser" + "/" + user.UserName).PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }

        public async Task<string> NewChat(string _name)
        {
            await client.Child("HaasChatApp"+"/").PostAsync(JsonConvert.SerializeObject(new ChatRoom()
            {
                Name= _name,
            }));
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>())
            .Where(x => x.Object.Name==_name).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name
            }).ToList()[0].Key;
        }
        public async Task  saveChat(ChatRoom newChat)
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
