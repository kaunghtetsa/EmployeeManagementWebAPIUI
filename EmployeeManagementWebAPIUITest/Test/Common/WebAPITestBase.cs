using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;

namespace EmployeeManagementWebAPIUITest.Test.Common
{
    /// <summary>
    /// WebAPITestBase
    /// </summary>
    public class WebAPITestBase
    {
        #region Protected methods

        /// <summary>
        /// GetAuthenticationInfo
        /// </summary>
        /// <param name="loginID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected AuthenticationInfo GetAuthenticationInfo(string loginID, string password)
        {
            AuthenticationInfo info = new AuthenticationInfo()
            {
                LoginID = loginID,
                Password = password
            };

            return info;
        }

        #endregion

    }
}
