using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using JMovies.Models;
using JMovies.Providers;

namespace JMovies
{
    public partial class Startup
    {
        // OAuthAuthorization öğesini kullanmak için uygulamayı etkinleştirin. Sonrasında Web API'larınızı güvenli hale getirebilirsiniz
        static Startup()
        {
            PublicClientId = "web";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // Kimlik doğrulamayı yapılandırma hakkında daha fazla bilgi için lütfen https://go.microsoft.com/fwlink/?LinkId=301864 adresini ziyaret edin.
        public void ConfigureAuth(IAppBuilder app)
        {
            // Veritabanı bağlamını, kullanıcı yöneticisini ve oturum açma yöneticisini istek başına tek bir örnek kullanacak şekilde yapılandırın
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Oturum açmış kullanıcı hakkında bilgi toplamak için uygulamada çerez kullanımını etkinleştirin
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Kullanıcı oturum açtığında güvenlik damgasının uygulama tarafından doğrulanmasını sağlar.
                    // Bu, parola değiştirdiğinizde veya hesabınıza dış oturum eklediğinizde kullanılan bir güvenlik özelliğidir.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(20),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            // Üçüncü taraf oturum açma sağlayıcısıyla oturum açan bir kullanıcı hakkında geçici olarak bilgi toplamak için çerez kullanın
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // İki öğeli kimlik doğrulama işleminde ikinci öğeyi doğrulama sırasında uygulama tarafından kullanıcı bilgilerinin geçici olarak depolanmasını sağlar.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Uygulamanın, telefon veya e-posta gibi ikinci oturum açma doğrulama öğesini hatırlamasını sağlar.
            // Bu seçeneği işaretlerseniz, oturum açma işlemi sırasındaki ikinci doğrulama adımınız oturum açtığınız cihazda hatırlanır.
            // Bu, oturum açarken kullandığınız Beni Anımsa seçeneğine benzer.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Kullanıcıların kimliğini doğrulamak üzere uygulamanın taşıyıcı belirteçleri kullanmasını sağlayın
            app.UseOAuthBearerTokens(OAuthOptions);

            // Üçüncü taraf oturum sağlayıcılarla oturum açmaya olanak tanımak için aşağıdaki satırlardan açıklamayı kaldırın
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
