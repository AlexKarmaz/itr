using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security.Facebook;
using Owin;
using WebApplication2.Models;
using Microsoft.Owin.Security;

namespace WebApplication2
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication(
         new TwitterAuthenticationOptions
         {
             ConsumerKey = "5eH3zxPrxA5bXOUbUK4TBxKmX",
             ConsumerSecret = "xJJ136KVe5yTdYHD3rRuQqjY6OhPdLZoV8q4LueQ46ld7xG9hR",
             BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(
             new[]
            {
              "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
               "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
               "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
               "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
               "4EB6D578499B1CCF5F581EAD56BE3D9B6744A5E5", // VeriSign Class 3 Primary CA - G5
               "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
               "B13EC36903F8BF4701D498261A0802EF63642BC3", // DigiCert High Assurance EV Root CA
               "B77DDB6867D3B325E01C90793413E15BF0E44DF2"
            })
         });

            app.UseVkontakteAuthentication("5617395", "phtjP6LuR8V5rPms8dkm", "email");

            app.UseFacebookAuthentication(
               appId: "1625341067758609",
              appSecret: "7a2057d9f9206fcbff55b82fb0bfb5d8");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "886220459251-5fmffijtlsq0ba1c9ddb7plqa1q8mv0d.apps.googleusercontent.com",
                ClientSecret = "C5y0R8BiRNhcjlLff31XpGju"
            });
        }
    }
}