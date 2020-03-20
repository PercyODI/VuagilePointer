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
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserInfo>> GetAllUsers()
        {
            return VuagileHub.KnownUsers;
        }

        [HttpGet("{authName}")]
        public ActionResult<UserInfo> GetUserByAuthName(string authName)
        {
            var foundUser = VuagileHub.KnownUsers.FirstOrDefault(ku => ku.AuthName == authName);
            if (foundUser == null)
            {
                return NotFound();
            }
            else
            {
                return foundUser;
            }
        }
    }
}