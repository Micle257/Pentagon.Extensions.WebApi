namespace Pentagon.Extensions.WebApi.Attributes {
    using System;

    public class UrlPathParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public UrlPathParameterAttribute(string name)
        {
            Name = name;
        }
    }
}