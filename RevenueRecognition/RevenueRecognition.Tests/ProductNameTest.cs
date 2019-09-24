using System;
using RevenueRecognition.Domain;
using Xunit;

namespace RevenueRecognization.Tests
{
    public class ProductNameTest
    {
        [Fact]
        public void productname_cannot_be_empty()
        {
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => ProductName.FromString(""));
            Assert.Equal("value\r\nParameter name: You must specify the name.", ex.Message);
        }

        [Fact]
        public void productname_cannot_contain_more_than_48chars()
        {
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => ProductName.FromString("iPhone 11 pro is the costliest phone in the market ..."));
            Assert.Equal("value\r\nParameter name: Name cannot be longer that 48 characters.", ex.Message);
        }
    }
}
