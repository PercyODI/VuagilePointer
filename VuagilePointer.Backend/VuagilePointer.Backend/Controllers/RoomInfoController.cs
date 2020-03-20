using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuagilePointer.Backend.Hubs;

namespace VuagilePointer.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomInfoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<RoomInfo>> GetAllRooms()
        {
            return VuagileHub.Rooms;
        }

        [HttpGet]
        public ActionResult<RoomInfo> GetRoomByName(string roomName)
        {
            var foundRoom = VuagileHub.Rooms.FirstOrDefault(r => r.Name == roomName);
            if (foundRoom == null)
            {
                return NotFound();
            }
            else
            {
                return foundRoom;
            }
        }
    }
}