// -----------------------------------------------------------------------
//  <copyright file="ApiAuthorization.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Attributes
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
}