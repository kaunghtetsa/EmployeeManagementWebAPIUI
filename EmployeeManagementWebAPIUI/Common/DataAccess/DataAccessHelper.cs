using System;
using System.IO;

namespace ASM.EmployeeManagement.WebAPIUI.Common.DataAccess
{
    /// <summary>
    /// DataAccess Helper class
    /// </summary>
    public class DataAccessHelper
    {
        /// <summary>
        /// DB config file path
        /// </summary>
        private const string DBConfigFile = @"App_Data\DBConnectionConfig.xml";

        /// <summary>
        /// Get DB config file path
        /// </summary>
        /// <returns></returns>
        public static string GetSettingFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DBConfigFile);
        }
    }
}