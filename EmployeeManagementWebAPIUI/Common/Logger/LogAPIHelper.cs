using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using ASM.EmployeeManagement.Common.Logger;
using ASM.EmployeeManagement.Common.Exception;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;

namespace ASM.EmployeeManagement.WebAPIUI.Common.Logger
{
    public class LogAPIHelper : LogHelper
    {
        /// <summary>
        /// ログ設定ファイルパス
        /// </summary>
        private const string LogConfigFile = @"App_Data\LogConfig.xml";

        /// <summary>
        /// 設定ファイルパス
        /// </summary>
        private static string _path = "";
        public static string GetSettingFilePath()
        {
            return _path;
        }

        /// 初期化
        /// </summary>
        public static void Initialize()
        {
            _path = GetFilePath(LogConfigFile);
            LogHelper.Initialize(_path);

            PrintVersions();
        }

        /// <summary>
        /// API開始(Developer & Spec)
        /// </summary>
        /// <param name="objCaller"></param>
        /// <param name="AuthenticationInfo"></param>
        /// <param name="sMethodName"></param>
        /// <param name="sFilePath"></param>
        /// <param name="nLineNumber"></param>
        public static void APIStart(object objCaller, AuthenticationInfo authInfo,
                                    [CallerMemberName] string sMethodName = "",
                                    [CallerFilePath] string sFilePath = "",
                                    [CallerLineNumber] int nLineNumber = 0)
        {
            string sLogMsg = "[API START] ";

            if (authInfo != null)
            {
                sLogMsg += string.Format("LoginID({0})",
                    authInfo.LoginID);
            }

            Develop(objCaller, sLogMsg, sMethodName, sFilePath, nLineNumber);
        }

        /// <summary>
        /// API終了(Developer & Spec)
        /// </summary>
        /// <param name="objCaller"></param>
        /// <param name="result"></param>
        /// <param name="timeStart"></param>
        /// <param name="sMethodName"></param>
        /// <param name="sFilePath"></param>
        /// <param name="nLineNumber"></param>
        public static void APIEnd(object objCaller, Result result, DateTime timeStart,
                                    [CallerMemberName] string sMethodName = "",
                                    [CallerFilePath] string sFilePath = "",
                                    [CallerLineNumber] int nLineNumber = 0)
        {
            string sLogMsg = string.Format("[API END  ]");

            if (result != null && result.ResultCode != null)
            {
                sLogMsg += string.Format(" ResultCode={0}", result.ResultCode);
            }

            if (result != null && result.ErrorDetail != null)
            {
                sLogMsg += string.Format(", ErrorDetail={0}",
                    result.ErrorDetail);
            }

            TimeSpan timeDif = DateTime.UtcNow - timeStart;
            sLogMsg += string.Format("\t{0}ms", (int)timeDif.TotalMilliseconds);

            Develop(objCaller, sLogMsg, sMethodName, sFilePath, nLineNumber);
        }

        /// <summary>
        /// <summary>
        /// 利用DLLのバージョンをログ出力する
        /// </summary>
        private static void PrintVersions()
        {
            PrintVersion("me", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location));
            PrintDllVersion("EmployeeManagementCommon");
            PrintDllVersion(EmployeeManagement.DataAccess.Common.Defines.Constants.Name);
        }

        /// <summary>
        /// ファイルパスを取得する
        /// </summary>
        /// <param name="sFile"></param>
        /// <returns></returns>
        private static string GetFilePath(string sFile)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sFile);
        }


    }
}