using System;
using System.Linq;

using ASM.EmployeeManagement.WebAPIUI.Common.Logger;

namespace ASM.EmployeeManagement.WebAPIUI.Common.Defines
{
    /// <summary>
    /// Application Information
    /// </summary>
    public static class APIInfo
    {
        #region Public Members

        /// <summary>
        /// NameSpace
        /// </summary>
        public const string NameSpace = "http://tempuri.org/";

        /// <summary>
        /// Service Name
        /// </summary>
        public const string ServiceName = "IEmployeeManagementWebAPIUIService";

        /// <summary>
        /// Application Version
        /// </summary>
        public const string CurrentServiceVersion = "V1.0.0";

        /// <summary>
        /// Authentication ACK
        /// </summary>
        public const string ResultInfoACK = "1";

        /// <summary>
        /// Authentication NACK
        /// </summary>
        public const string ResultInfoNACK = "2";

        /// <summary>
        /// Default language
        /// </summary>
        public const string DefaultLanguage = "en";

        #endregion

        #region Public Methods

        /// <summary>
        /// Upgrade the URL to the current version of the APIUI
        /// </summary>
        /// <param name="url"></param>
        /// <param name="oldVersion">Out the older version of the APIUI</param>
        /// <returns></returns>
        public static string URLUpgrade(string url, out string oldVersion)
        {
            oldVersion = null;

            if (string.IsNullOrWhiteSpace(url))
            {
                return url;
            }

            try
            {
                string sURLParts = url.Replace(NameSpace, "");

                string[] asURLParts = sURLParts.Split('/');
                string[] asFinalURLParts = new string[asURLParts.Length + 1];
                asFinalURLParts[0] = CurrentServiceVersion;

                if (asURLParts.Length > 0)
                {
                    int nCopyStartIndex = -1;

                    if (asURLParts[0] == ServiceName)
                    {
                        nCopyStartIndex = 0;
                    }
                    else
                    {
                        nCopyStartIndex = 1;
                        oldVersion = asURLParts[0];
                    }

                    Array.Copy(asURLParts, nCopyStartIndex, asFinalURLParts, 1, asURLParts.Length - nCopyStartIndex);
                }

                string sURL = string.Join("/", asFinalURLParts);
                sURL = string.Concat(NameSpace, sURL);
                sURL = sURL.TrimEnd(new char[] { '/', ' ' });

                return sURL;
            }
            catch (System.Exception ex)
            {
                LogAPIHelper.Error(null, ex);
                throw;
            }
        }

        /// <summary>
        /// It will downgrade the url to the some specific version
        /// </summary>
        /// <param name="url"></param>
        /// <param name="oldVersion"></param>
        /// <returns></returns>
        public static string URLDowngrade(string url, string oldVersion)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return url;
            }

            try
            {
                string sURLParts = url.Replace(NameSpace, "");

                string[] asURLParts = sURLParts.Split('/');
                string[] asFinalArray = new string[asURLParts.Length + 1];

                int nDestinationCopyStart = 0;

                if (!string.IsNullOrEmpty(oldVersion))
                {
                    asFinalArray[nDestinationCopyStart] = oldVersion;
                    nDestinationCopyStart++;
                }

                int nCopyStartIndex = -1;

                if (asURLParts.Length > 0)
                {
                    if (asURLParts[0] == ServiceName)
                    {
                        nCopyStartIndex = 0;
                    }
                    else
                    {
                        nCopyStartIndex = 1;
                    }
                }

                if (nCopyStartIndex > -1)
                {
                    Array.Copy(asURLParts, nCopyStartIndex, asFinalArray, nDestinationCopyStart, asURLParts.Length - nCopyStartIndex);
                }

                string sUrl = string.Join("/", asFinalArray.Where(W => W != null));
                sUrl = string.Concat(NameSpace, sUrl);

                return sUrl;
            }
            catch (System.Exception ex)
            {
                LogAPIHelper.Error(null, ex);
                throw;
            }
        }

        #endregion
    }
}