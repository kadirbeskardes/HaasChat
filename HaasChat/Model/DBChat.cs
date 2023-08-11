using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HaasChat.Model
{
    internal class DBChat
    {
        FirebaseClient client;

        public DBChat()
        {
            client = new FirebaseClient("https://haaschat-9a85d-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        public async Task<List<ChatRoom>> GetAllChat(List<string> _chats)
        {
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>())
            .Where(x => _chats.Contains(x.Key)).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name,
                Admins = item.Object.Admins,
                Partpicatinas = item.Object.Partpicatinas,
                ImageURL=item.Object.ImageURL
            }).ToList();
        }


        public async Task<ChatRoom> GetChat(string key)
        {
            return await client.Child("HaasChatApp" + "/" + key).OnceSingleAsync<ChatRoom>();
        }
        internal async Task<User> isThereUser(string username)
        {
            return (await client.Child("HaasChatAppUser" + "/" + username).OnceSingleAsync<User>());
        }
        internal async Task<User> getUser(string _username)
        {
            return await client.Child("HaasChatAppUser" + "/" + _username).OnceSingleAsync<User>();
        }
        public async Task<bool> newUser(User user)
        {
            await client.Child("HaasChatAppUser" + "/" + user.UserName).PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }

        public async Task<string> NewChat(string _name, string _username,string url)
        {
            await client.Child("HaasChatApp" + "/").PostAsync(JsonConvert.SerializeObject(new ChatRoom()
            {
                Name = _name,
                Admins = new ObservableCollection<string> { _username },
                ImageURL=url
            }));
            return (await client.Child("HaasChatApp").OnceAsync<ChatRoom>())
            .Where(x => x.Object.Name == _name).Select((item) => new ChatRoom
            {
                Key = item.Key,
                Name = item.Object.Name
            }).ToList()[0].Key;
        }
        public async Task saveChat(ChatRoom _newChat, string _key)
        {
            await client.Child("HaasChatApp" + "/" + _key).PutAsync(JsonConvert.SerializeObject(_newChat));
        }

        public async Task addParToChat(string _username, string _key)
        {
            var room = await client.Child("HaasChatApp" + "/" + _key).OnceSingleAsync<ChatRoom>();
            if (room.Partpicatinas == null)
            {
                room.Partpicatinas = new ObservableCollection<string>();
            }
            room.Partpicatinas.Add(_username);
            await client.Child("HaasChatApp").Child(_key).Child("Partpicatinas").PutAsync(room.Partpicatinas);
        }
        public async Task SendMessage(Chat ch, string _room)
        {
            await client.Child("HaasChatApp/" + _room + "/Message").PostAsync(ch);
        }
        public ObservableCollection<Chat> chats(string _room)
        {
            return client.Child("HaasChatApp/" + _room + "/Message").AsObservable<Chat>().AsObservableCollection();
        }
    }
}
