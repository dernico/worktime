using System.Collections.Generic;
using System.Security.Claims;

namespace worktime.server.Business.User
{
  public interface IUserBL
  {
    void UpdateOrCreateUser(ClaimsPrincipal principal);
    Data.Model.User GetUser(List<Claim> claims);
  }
}