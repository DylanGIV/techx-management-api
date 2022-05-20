using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechxManagementApi.Authorization;
using TechxManagementApi.Helpers;

[TestClass]
public class JwtUtilsTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GenerateJwtToken_WhenAccountIsNull_ArgumentNullException()
    {
        // act
        var context = new DataContext(new Mock<IConfiguration>().Object);

        var service = new JwtUtils(context, new Mock<IOptions<AppSettings>>().Object);

        // sut
        service.GenerateJwtToken(null);
    }
}
