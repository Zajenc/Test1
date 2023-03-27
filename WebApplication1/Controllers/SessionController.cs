using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;

namespace AnnouncmentSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public string GetInfo()
        {
            
            ArrayList info = new ArrayList();
             
            if(string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionVar.SessionUserKey)))
            {
                HttpContext.Session.SetString(SessionVar.SessionUserKey, "Current User");
                HttpContext.Session.SetString(SessionVar.SessionIDKey, Guid.NewGuid().ToString());

            }
            var username = HttpContext.Session.GetString(SessionVar.SessionUserKey);
            var id = HttpContext.Session.GetString(SessionVar.SessionIDKey);

            info.Add(username);
            info.Add(id);

            return  (string)info[1];
        }





    }
}
