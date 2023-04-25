using System;

namespace Infrastructure.BaseTools.Test
{
    public class UriToolsTest
    {

        [Theory]
        [InlineData("")]
        [InlineData("https")]
        [InlineData("google")]
        [InlineData("google.com")]
        public void Return_false_for_invalid_Uris(string Uri)
        {
            // Arrange

            // Act


            // Assert
            Assert.False(UriTools.IsValidUri(Uri));
        }

        [Theory]
        [InlineData("https://google.com")]
        [InlineData("http://google.com")]
        [InlineData("https://www.google.com")]
        [InlineData("https://google")]
        public void Return_true_for_valid_Uris(string Uri)
        {
            // Arrange

            // Act


            // Assert
            Assert.True(UriTools.IsValidUri(Uri));
        }
    }
}
