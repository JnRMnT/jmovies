using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMovies.Entities.Interfaces
{
    public interface IValidator
    {
        bool IsValid(object[] values);
    }
}
