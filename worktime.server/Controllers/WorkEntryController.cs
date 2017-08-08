using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using worktime.server.Business.User;
using worktime.server.Business.WorkEntrys;
using worktime.server.Data.Model;

namespace worktime.server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkEntryController : Controller
    {
        private readonly IWorkEntrysBL _bl;
        private readonly IUserBL _user;

        public WorkEntryController(IWorkEntrysBL bl, IUserBL user)
        {
            _bl = bl;
            _user = user;
        }
        
        // GET api/workentry
        [HttpGet]
        public List<WorkEntry> Get()
        {
            var user = _user.GetUser(this.User.Claims.ToList());
            return _bl.GetWorkentrys(user.Id);
        }

        // POST api/workentry
        [HttpPost]
        public void Post([FromBody]WorkEntry entry)
        {
            var user = _user.GetUser(this.User.Claims.ToList());
            _bl.SaveWorkEntry(user.Id, entry);
        }

        // POST api/workentry
        [HttpPut]
        public void Put([FromBody]WorkEntry entry)
        {
            var user = _user.GetUser(this.User.Claims.ToList());
            _bl.SaveWorkEntry(user.Id, entry);
        }

        // DELETE api/workentry
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var user = _user.GetUser(this.User.Claims.ToList());
            _bl.DeleteWorkEntry(user.Id, id.ToString());
        }
    }
}