namespace Pentagon.Extensions.WebApi.Attributes {
    using System;

    public class ResponseHeaderAttribute : Attribute
    {
        public string Name { get; set; }

        public ResponseHeaderAttribute(string name)
        {
            Name = name;
        }
    }
}