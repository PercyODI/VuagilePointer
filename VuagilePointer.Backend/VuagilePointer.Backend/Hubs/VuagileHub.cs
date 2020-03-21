using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace VuagilePointer.Backend.Hubs
{
    public class VuagileHub : Hub
    {
        public static List<RoomInfo> Rooms = new List<RoomInfo>();

        public static List<UserInfo> KnownUsers = new List<UserInfo>()
        {
            new UserInfo()
            {
                AuthName = "pearse.hutson",
                FirstName = "Pearse",
                LastName = "Hutson"
            },
            new UserInfo()
            {
                AuthName = "paul.hartman",
                FirstName = "Paul",
                LastName = "Hartman"
            },
            new UserInfo()
            {
                AuthName = "stephen.brink",
                FirstName = "Stephen",
                LastName = "Brink"
            },
        };
        public async Task AddUserToRoom(string userAuthName, string roomName)
        {
            var foundRoom = Rooms.FirstOrDefault(r => r.Name == roomName);
            if (foundRoom != null)
            {
                var foundUser = KnownUsers.FirstOrDefault(ku => ku.AuthName == userAuthName.ToLower());
                if (foundUser != null)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    foundUser.ConnectionId = Context.ConnectionId;
                    foundRoom.Name = roomName;
                    foundRoom.Users.Add(foundUser);
                }
            }
        }

        public async Task CreateRoom(string roomName)
        {
            var foundRoom = Rooms.FirstOrDefault(r => r.Name == roomName);
            if (foundRoom == null)
            {
                var newRoom = new RoomInfo()
                {
                    Name = roomName
                };

                Rooms.Add(newRoom);
            }
        }

        public async Task SetTopic(string roomName, string Topic)
        {
            var foundRoom = Rooms.FirstOrDefault(r => r.Name == roomName);
            if (foundRoom != null)
            {
                var newTopic = new TopicInfo()
                {
                    Name = Topic
                };
                foundRoom.CurrentTopic = newTopic;
                foundRoom.Topic.Add(newTopic);
                await Clients.Group(roomName).SendCoreAsync("setTopic", new object[] {foundRoom.Topic});
            }
        }

        public async Task SetFlipDateTime(string roomName, DateTime flipTime)
        {
            var foundRoom = Rooms.FirstOrDefault(r => r.Name == roomName);
            if (foundRoom != null)
            {
                await Clients.Group(roomName).SendCoreAsync("flipDateTime", new object[] {flipTime});
            }
        }
    }

    public class RoomInfo
    {
        public string Name { get; set; }
        public List<UserInfo> Users { get; set; } = new List<UserInfo>();
        public TopicInfo CurrentTopic { get; set; }
        public List<TopicInfo> Topic { get; set; } = new List<TopicInfo>();
    }

    public class TopicInfo
    {
        public string Name { get; set; }
        public Dictionary<UserInfo, int> PointsByUser { get; set; } = new Dictionary<UserInfo, int>();
    }

    public class UserInfo
    {
        public string ConnectionId { get; set; }
        public string AuthName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}