using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FortyLife.Core
{
    public static class Secrets
    {
        public static string GetRecaptchaClientKey()
        {
            var path = HttpRuntime.AppDomainAppPath + @"\App_Data\recaptcha-client-key.txt";

            if (File.Exists(path)) return File.ReadAllText(path);

            // Didn't find a file, so make one - throw an error to inform the programmer/user
            File.AppendAllText(path, "Replace the contents of this file with a client key to access recaptcha services!");
            throw new Exception("The password file recaptcha-client-key.txt in App_Data didn't exist, but now it does. " +
                                "Replace its contents with an reCaptcha client key (NO EXTRA WHITESPACE) and try running this web app again.");
        }
    }
}
