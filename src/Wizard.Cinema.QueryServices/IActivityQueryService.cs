﻿using System.Collections.Generic;
using Infrastructures;
using Wizard.Cinema.QueryServices.DTOs.Activity;

namespace Wizard.Cinema.QueryServices
{
    public interface IActivityQueryService
    {
        ActivityInfo Query(long activityId);

        IEnumerable<ActivityInfo> Query(long[] activityIds);

        PagedData<ActivityInfo> QueryPaged(SearchActivityCondition search);
    }
}
