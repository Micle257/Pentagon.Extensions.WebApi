// -----------------------------------------------------------------------
//  <copyright file="IListResponse.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Responses
{
    using System.Collections.Generic;

    public interface IListResponse<TContent> : IResponse<IEnumerable<TContent>>, IEnumerable<TContent> { }
}