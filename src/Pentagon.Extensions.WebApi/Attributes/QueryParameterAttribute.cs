// -----------------------------------------------------------------------
//  <copyright file="ApiAuthorization.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;

    class UrlQueryParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public UrlQueryParameterAttribute(string name)
        {
            Name = name;
        }
    }

    class UrlPathParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public UrlPathParameterAttribute(string name)
        {
            Name = name;
        }
    }
}