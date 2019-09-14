using System;
using System.Collections.Generic;
using System.Text;

namespace JMovies.DataAccess.Entities.Enums
{
    /// <summary>
    /// Enumeration representing different Persister Job Working Types
    /// </summary>
    public enum PersisterWorkingTypeEnum
    {
        FetchByExternalID,
        UpdateInternalData
    }
}
