// -----------------------------------------------------------------------
//  <copyright file="UrlPathParameterAttribute.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Attributes
{
    using System;

    public class UrlPathParameterAttribute : Attribute
    {
        public UrlPathParameterAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}