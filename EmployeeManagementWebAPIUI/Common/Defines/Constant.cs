
namespace ASM.EmployeeManagement.WebAPIUI.Common.Defines
{
    /// <summary>
    /// Constant
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// Error Message for E000
        /// </summary>
        public const string ErrorMessageE000 = "It is an input check error.";

        /// <summary>
        /// Error Message for E001
        /// </summary>
        public const string ErrorMessageE001 = "User authentication failed.";

        /// <summary>
        /// Error Message for E002
        /// </summary>
        public const string ErrorMessageE002 = "Login ID or password does not match.";

        /// <summary>
        /// Error Message for E999
        /// </summary>
        public const string ErrorMessageE999 = "Unexpected error occurred.";

        /// <summary>
        /// Error Message for E003
        /// </summary>
        public const string ErrorMessageE003 = "User does not exist.";
		/// <summary>
		/// Error Message for E004
		/// </summary>
		public const string ErrorMessageE004 = "Email does not exist.";
		/// <summary>
		/// Date Format(YYYY/mm/dd)
		/// </summary>
		public const string DateFormat = "yyyy/mm/dd";

        /// <summary>
        /// Property Names
        /// </summary>
        public enum PropertyName
        {
			AllPara,
			Paging,
			FilterInfo,
			SortKey,
			SortOrder,
            UserID,
			Email,
			PagingStartIndex,
			PageSize
        }
    }
}