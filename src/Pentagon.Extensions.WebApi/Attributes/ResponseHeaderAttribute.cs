// -----------------------------------------------------------------------
//  <copyright file="ResponseHeaderAttribute.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.WebApi.Attributes
{
    using System;

    public class ResponseHeaderAttribute : Attribute
    {
        public ResponseHeaderAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}