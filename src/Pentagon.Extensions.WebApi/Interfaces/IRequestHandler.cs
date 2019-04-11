// -----------------------------------------------------------------------
//  <copyright file="IRequestHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces;
    using Requests;
    using Responses;

    public interface IRequestHandler
    {
        /// <summary> Executes the single item request with no data sending. </summary>
        /// <typeparam name="T"> Type of content object. </typeparam>
        /// <param name="request"> The execute request. </param>
        /// <param name="cancellationToken"> The <see cref="CancellationToken" /> to observe while waiting for the task to complete. </param>
        /// <returns> <see cref="TraktResponse{TContent}" /> instance with response information. </returns>
        Task<IResponse<T, THeaders>> ExecuteSingleItemRequest<T, THeaders>(IRequest<T> request, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders;

        Task<IListResponse<TContent, THeaders>> ExecuteListRequestAsync<TContent, THeaders>(IRequest<TContent> request, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders;

        Task<IPagedResponse<TContent, THeaders>> ExecutePagedRequest<TContent, THeaders>(IRequest<TContent> request, CancellationToken cancellationToken = default)
                where THeaders : IApiResponseHeaders;
    }
}