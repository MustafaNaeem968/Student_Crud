using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore.TestBase;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Json;
using Abp.MultiTenancy;
using Abp.Web.Models;
using Myproject.EntityFrameworkCore;
using Myproject.Models.TokenAuth;
using Myproject.Web.Startup;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shouldly;

namespace Myproject.Web.Tests
{
    public abstract class MyprojectWebTestBase : AbpAspNetCoreIntegratedTestBase<Startup>
    {
        protected static readonly Lazy<string> ContentRootFolder;

        static MyprojectWebTestBase()
        {
            ContentRootFolder = new Lazy<string>(WebContentDirectoryFinder.CalculateContentRootFolder, true);
        }
        
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return base
                .CreateWebHostBuilder()
                .UseContentRoot(ContentRootFolder.Value)
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(MyprojectWebMvcModule).Assembly.FullName);
        }

        #region Get response

        protected async Task<T> GetResponseAsObjectAsync<T>(string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode);
            return JsonConvert.DeserializeObject<T>(strResponse, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        protected async Task<string> GetResponseAsStringAsync(string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetResponseAsync(url, expectedStatusCode);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> GetResponseAsync(string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await Client.GetAsync(url);
            response.StatusCode.ShouldBe(expectedStatusCode);
            return response;
        }

        #endregion
        
        #region Authenticate
        
        /// <summary>
        /// /api/TokenAuth/Authenticate
        /// TokenAuthController
        /// </summary>
        /// <param name="tenancyName"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async Task AuthenticateAsync(string tenancyName, AuthenticateModel input)
        {
            if (tenancyName.IsNullOrWhiteSpace())
            { 
                var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName));
                if (tenant != null)
                {
                    AbpSession.TenantId = tenant.Id;
                    Client.DefaultRequestHeaders.Add("Abp.TenantId", tenant.Id.ToString());  //Set TenantId
                }
            }

            var response = await Client.PostAsync("/api/TokenAuth/Authenticate",
                new StringContent(input.ToJsonString(), Encoding.UTF8, "application/json"));
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var result =
                JsonConvert.DeserializeObject<AjaxResponse<AuthenticateResultModel>>(
                    await response.Content.ReadAsStringAsync());
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Result.AccessToken);
            
            AbpSession.UserId = result.Result.UserId;
        }
        
        #endregion
        
        #region Login

        protected void LoginAsHostAdmin()
        {
            LoginAsHost(AbpUserBase.AdminUserName);
        }

        protected void LoginAsDefaultTenantAdmin()
        {
            LoginAsTenant(AbpTenantBase.DefaultTenantName, AbpUserBase.AdminUserName);
        }

        protected void LoginAsHost(string userName)
        {
            AbpSession.TenantId = null;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for host.");
            }

            AbpSession.UserId = user.Id;
        }

        protected void LoginAsTenant(string tenancyName, string userName)
        {
            var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName));
            if (tenant == null)
            {
                throw new Exception("There is no tenant: " + tenancyName);
            }

            AbpSession.TenantId = tenant.Id;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for tenant: " + tenancyName);
            }

            AbpSession.UserId = user.Id;
        }

        #endregion


        #region UsingDbContext

        protected void UsingDbContext(Action<MyprojectDbContext> action)
        {
            using (var context = IocManager.Resolve<MyprojectDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected T UsingDbContext<T>(Func<MyprojectDbContext, T> func)
        {
            T result;

            using (var context = IocManager.Resolve<MyprojectDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected async Task UsingDbContextAsync(Func<MyprojectDbContext, Task> action)
        {
            using (var context = IocManager.Resolve<MyprojectDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected async Task<T> UsingDbContextAsync<T>(Func<MyprojectDbContext, Task<T>> func)
        {
            T result;

            using (var context = IocManager.Resolve<MyprojectDbContext>())
            {
                result = await func(context);
                await context.SaveChangesAsync(true);
            }

            return result;
        }

        #endregion

        #region ParseHtml

        protected IHtmlDocument ParseHtml(string htmlString)
        {
            return new HtmlParser().ParseDocument(htmlString);
        }

        #endregion
    }
}