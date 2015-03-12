using System;

namespace TestHelpers
{
    public static class UrlHelper
    {
        /// <summary>
        ///     dirty little method to get correct url depending where api tests are being run. Will need to add more machines
        ///     to it as we go on. Will do for now. Better than hard coded i guess!
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            switch (Environment.MachineName.ToLower())
            {
                case "tgp-home":
                    return "http://www.firstonetoapi.dev";
                case "vostro_270_015":
                    return "http://www.firstonetoapi.dev";
                case "threemammals":
                    return "http://api.threemammals.co.uk";
            }
            throw new Exception("Couldnt find machine name for URL setting");
        }
    }
}