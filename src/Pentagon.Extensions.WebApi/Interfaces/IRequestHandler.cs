// -----------------------------------------------------------------------
//  <copyright file="IRequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Requests;
    using Responses;

    public interface IRequestHandler
    {
        Task<TResponse> ExecuteSingleRequest<TResponse, T>(IRequest<T> request, CancellationToken cancellationToken = default)
                where TResponse : IResponse<T>, new();

        Task<TResponse> ExecuteManyRequest<TResponse, T>(IRequest<T> request, CancellationToken cancellationToken = default)
                where TResponse : IListResponse<T>, new();
    }
}