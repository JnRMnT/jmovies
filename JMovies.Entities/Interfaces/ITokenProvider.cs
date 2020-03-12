using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.Entities.Interfaces
{
    public interface ITokenProvider
    {
        string IssueToken(string username);
    }
}
