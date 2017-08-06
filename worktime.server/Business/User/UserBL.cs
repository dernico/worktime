using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using worktime.server.Data.Model;
using worktime.server.Data.Repository;

namespace worktime.server.Business.User
{
  public class UserBL : IUserBL
  {
    private readonly IUserRepository _userRepro;
    public UserBL(IUserRepository userRepro){
      _userRepro = userRepro;
    }

    public Data.Model.User GetUser(List<Claim> claims)
    {
      var id = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
      return _userRepro.GetUser(id);
    }

    void IUserBL.UpdateOrCreateUser(ClaimsPrincipal principal)
    {
      var id = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).FirstOrDefault();
      var name = principal.Claims.Where(c => c.Type == ClaimTypes.GivenName).Select(c => c.Value).FirstOrDefault();
      var lastname = principal.Claims.Where(c => c.Type == ClaimTypes.Surname).Select(c => c.Value).FirstOrDefault();
      var displayName = principal.Claims.Where(c => c.Type == "name").Select(c => c.Value).FirstOrDefault();
      var picture = principal.Claims.Where(c => c.Type == "picture").Select(c => c.Value).FirstOrDefault();

      var user = new Data.Model.User {
        Id = id,
        FirstName = name,
        LastName = lastname,
        DisplayName = displayName,
        PictureUrl = picture,
      };

      _userRepro.CreateOrUpdate(user);
    }
  }
}