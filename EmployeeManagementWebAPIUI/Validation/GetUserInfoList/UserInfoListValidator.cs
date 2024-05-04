using ASM.EmployeeManagement.DataAccess.Common.FilterInfo;
using ASM.EmployeeManagement.DataAccess.Common.Paging;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using static ASM.EmployeeManagement.WebAPIUI.Common.Defines.Constant;

namespace ASM.EmployeeManagement.WebAPIUI.Validation.GetUserInfoList
{
	/// <summary>
	/// UserInfo Validator
	/// </summary>
	public class UserInfoListValidator : APIValidatorBase
	{
		#region Constants

		/// <summary>
		/// UserID miniumn length
		/// </summary>
		public const int EmailMinLength = 1;

		/// <summary>
		/// UserID maximum length
		/// </summary>
		public const int EmailMaxLength = 50;
		/// <summary>
		/// UserID minimum length
		/// </summary>
		public const int UserIDMinLength = 1;

		/// <summary>
		/// UserID maximum length
		/// </summary>
		public const int UserIDMaxLength = 50;

		/// <summary>
		/// Page Start Index miniumn value
		/// </summary>
		public const int PageStartIndexMinValue = 0;

		/// <summary>
		/// Page Start Index maxiumn value
		/// </summary>
		public const int PageStartIndexMaxValue = 1000;
		/// <summary>
		/// Page Size Index maxiumn value
		/// </summary>
		public const int PageSizeMinValue = 1;

		/// <summary>
		/// Page Size Index maxiumn value
		/// </summary>
		public const int PageSizeMaxValue = 1000;
		#endregion

		#region Public method

		/// <summary>
		/// Validate UserID
		/// </summary>
		/// <param name="userID"></param>
		public static void Validate(Paging iPagingPara, UserFilterInfo objFilterInfo)
		{
			try
			{
				// Validate LoginID
				if(objFilterInfo.IsExactMatchSearch)
					IsNullOrEmptyAndValidLength(objFilterInfo.UserID, UserIDMinLength, UserIDMaxLength, nameof(PropertyName.UserID));
				// Validate Email
				if (objFilterInfo.IsExactMatchSearch)
					IsNullOrEmptyAndValidLength(objFilterInfo.Email, EmailMinLength, EmailMaxLength, nameof(PropertyName.Email));
				// Validate PagingStartIndex
				IsValidIntervalValue(iPagingPara.StartIndex, PageStartIndexMinValue, PageStartIndexMaxValue, nameof(PropertyName.PagingStartIndex));
				// Validate PagingSize
				IsValidIntervalValue(iPagingPara.Num, PageSizeMinValue, PageSizeMaxValue, nameof(PropertyName.PageSize));
				
			}
			catch (InputParameterException)
			{
				throw;
			}
		}

		

		#endregion
	}
}