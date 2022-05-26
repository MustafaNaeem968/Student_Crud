using System.Threading.Tasks;
using Myproject.Models.TokenAuth;
using Myproject.Web.Controllers;
using Shouldly;
using Xunit;

namespace Myproject.Web.Tests.Controllers
{
    public class HomeController_Tests: MyprojectWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}