using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace smileRed25.Helpers
{
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        const string isRemembered = "IsRemembered";
        const string isPowerOn = "IsPowerON";
        const string isTitleName = "IsTitleName";
        const string isLogin = "IsLogin";
        const string isCart = "IsCart";
        const string isCountOrderID= "IsCountOrderID";
        const string addOrder = "AddOrder";
        const string newAddress = "NewAddress";
        const string activeAddress = "ActiveAddress";
        private const string tokenType = "TokenType";
        private const string accessToken = "AccessToken";
        private const string userASP = "UserASP";
        static readonly string stringDefault = string.Empty;
        private static readonly bool  booleanDefault = false;
        #endregion

        public static string IsRemembered
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRemembered, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRemembered, value);
            }
        }


        public static string IsPowerON
        {
            get
            {
                return AppSettings.GetValueOrDefault(isPowerOn, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isPowerOn, value);
            }
        }

        public static string IsTitleName
        {
            get
            {
                return AppSettings.GetValueOrDefault(isTitleName, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isTitleName, value);
            }
        }

        public static string IsLogin
        {
            get
            {
                return AppSettings.GetValueOrDefault(isLogin, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isLogin, value);
            }
        }

        public static string IsCart
        {
            get
            {
                return AppSettings.GetValueOrDefault(isCart, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isCart, value);
            }
        }

        public static string IsCountOrderID
        {
            get
            {
                return AppSettings.GetValueOrDefault(isCountOrderID, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isCountOrderID, value);
            }
        }

        public static string AddOrder
        {
            get
            {
                return AppSettings.GetValueOrDefault(addOrder, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(addOrder, value);
            }
        }

        public static string NewAddress
        {
            get
            {
                return AppSettings.GetValueOrDefault(newAddress, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(newAddress, value);
            }
        }

        public static string ActiveAddress
        {
            get
            {
                return AppSettings.GetValueOrDefault(activeAddress, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(activeAddress, value);
            }
        }

        public static string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault(tokenType, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tokenType, value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(accessToken, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(accessToken, value);
            }
        }

        public static string UserASP
        {
            get
            {
                return AppSettings.GetValueOrDefault(userASP, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userASP, value);
            }
        }

    }
}