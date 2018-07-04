// -----------------------------------------------------------------------
//  <copyright file="IRequestHandlerFactory.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Abstractions
{
    public interface IRequestHandlerFactory
    {
        IRequestHandler Create();
    }
}