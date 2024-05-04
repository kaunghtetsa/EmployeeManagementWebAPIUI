using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using static ASM.EmployeeManagement.WebAPIUI.Common.Defines.Constant;

namespace ASM.EmployeeManagement.WebAPIUI.Validation.GetUserInfo
{
    /// <summary>
    /// UserInfo Validator
    /// </summary>
    public class UserInfoValidator : APIValidatorBase
    {
        #region Constants

        /// <summary>
        /// UserID miniumn length
        /// </summary>
        public const int UserIDMinLength = 1;

        /// <summary>
        /// UserID maximum length
        /// </summary>
        public const int UserIDMaxLength = 50;

        #endregion

        #region Public method

        /// <summary>
        /// Validate UserID
        /// </summary>
        /// <param name="userID"></param>
        public static void Validate(string userID)
        {
            try
            {
                // Validate LoginID
                IsNullOrEmptyAndValidLength(userID, UserIDMinLength, UserIDMaxLength, nameof(PropertyName.UserID));
            }
            catch (InputParameterException)
            {
                throw;
            }
        }

        #endregion
    }
}