// -----------------------------------------------------------------------
//  <copyright file="UrlQueryParameterAttribute.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Attributes
{
    using System;

    public class UrlQueryParameterAttribute : Attribute
    {
        public UrlQueryParameterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}