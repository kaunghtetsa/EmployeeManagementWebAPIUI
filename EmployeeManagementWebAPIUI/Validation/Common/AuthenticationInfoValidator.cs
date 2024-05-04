using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;

namespace ASM.EmployeeManagement.WebAPIUI.Validation.Common
{
    public class AuthenticationInfoValidator : APIValidatorBase
    {
        #region Constants

        /// <summary>
        /// LoginID miniumn length
        /// </summary>
        public const int LoginIDMinLength = 1;

        /// <summary>
        /// LoginID maximum length
        /// </summary>
        public const int LoginIDMaxLength = 50;

        /// <summary>
        /// Password minimum length
        /// </summary>
        public const int PasswordMinLength = 1;

        /// <summary>
        /// Password maxium length
        /// </summary>
        public const int PasswordMaxLength = 50;

        #endregion

        /// <summary>
        /// Validate Auth Information
        /// </summary>
        /// <param name="authenticationInfo"></param>
        public static void Validate(AuthenticationInfo authenticationInfo)
        {
            try
            {
                if (authenticationInfo == null)
                {
                    throw new InputParameterException(InputParameterException.MessageIDType.E000, new string[] { nameof(AuthenticationInfo) });
                }

                // Validate LoginID
                IsNullOrEmptyAndValidLength(authenticationInfo.LoginID, LoginIDMinLength, LoginIDMaxLength, nameof(AuthenticationInfo.LoginID));

                // Validate Password
                IsNullOrEmptyAndValidLength(authenticationInfo.Password, PasswordMinLength, LoginIDMaxLength, nameof(AuthenticationInfo.Password));
            }
            catch (InputParameterException)
            {
                throw;
            }
        }
    }
}