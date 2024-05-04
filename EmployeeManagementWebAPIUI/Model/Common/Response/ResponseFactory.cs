using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfoList.Response;

namespace ASM.EmployeeManagement.WebAPIUI.Model.Common.Response
{
    /// <summary>
    /// ResponseFactory
    /// </summary>
    public class ResponseFactory
    {
        #region Public methods

        /// <summary>
        /// GetUserInfo
        /// </summary>
        /// <param name="authInfo"></param>
        /// <returns></returns>
        public static ResGetUserInfo GetUserInfo(AuthenticationInfo authInfo)
        {
            ResGetUserInfo response = new ResGetUserInfo();
            response.ServiceReferenceVersion = GetServiceReferenceVersion(authInfo);
            return response;
        }
		public static ResGetUserInfoList GetUserInfoList(AuthenticationInfo authInfo)
		{
			ResGetUserInfoList response = new ResGetUserInfoList();
			response.ServiceReferenceVersion = GetServiceReferenceVersion(authInfo);
			return response;
		}
		#endregion

		#region Private methods

		/// <summary>
		/// GetServiceReferenceVersion
		/// </summary>
		/// <param name="authInfo"></param>
		/// <returns></returns>
		private static string GetServiceReferenceVersion(AuthenticationInfo authInfo)
        {
            if (authInfo == null)
            {
                return null;
            }
            return authInfo.ServiceReferenceVersion;
        }

        #endregion

    }
}