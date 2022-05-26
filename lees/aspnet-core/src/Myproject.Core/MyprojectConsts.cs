using Myproject.Debugging;

namespace Myproject
{
    public class MyprojectConsts
    {
        public const string LocalizationSourceName = "Myproject";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "6f2fe8dd2b544519baa490c589f48ddc";
    }
}
