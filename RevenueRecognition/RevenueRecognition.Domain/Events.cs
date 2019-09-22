using System;
using System.Collections.Generic;
using System.Text;

namespace RevenueRecognition.Domain
{
    public static class Events
    {
        public class ProductCreated
        {
            public Guid ProductId { get; set; }
            public string Type { get; set; }
        }
        public class ProductNameChanged
        {
            public Guid ProductId { get; set; }
            public string Name { get; set; }
        }
    }
}
