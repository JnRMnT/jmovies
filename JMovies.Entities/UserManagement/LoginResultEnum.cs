using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.UserManagement
{
    public enum LoginResultEnum
    {
        Undefined,
        Successful,
        WrongUsernameOrPassword,
        UserBlocked
    }
}
