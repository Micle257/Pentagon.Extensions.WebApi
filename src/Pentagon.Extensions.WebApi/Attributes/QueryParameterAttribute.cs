// -----------------------------------------------------------------------
//  <copyright file="ApiAuthorization.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi
{
    using System;

    public class UrlQueryParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public UrlQueryParameterAttribute(string name)
        {
            Name = name;
        }
    }

    public class UrlPathParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public UrlPathParameterAttribute(string name)
        {
            Name = name;
        }
    }

    public class ResponseHeaderAttribute : Attribute
    {
        public string Name { get; set; }

        public ResponseHeaderAttribute(string name)
        {
            Name = name;
        }
    }
}