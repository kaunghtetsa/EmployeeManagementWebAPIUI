using ASM.EmployeeManagement.DataAccess.Common.FilterInfo;
using ASM.EmployeeManagement.DataAccess.Common.Paging;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.GetUserInfoList;
using ASM.EmployeeManagement.DataAccess.Model;
using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfoList;
using ASM.EmployeeManagement.WebAPIUI.Common.Logger;
using ASM.EmployeeManagement.WebAPIUI.DataConvert;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfoList.Response;
using ASM.EmployeeManagement.WebAPIUI.Validation.GetUserInfoList;
using System;
using System.Collections.Generic;

namespace ASM.EmployeeManagement.WebAPIUI.Service
{
	/// <summary>
	/// GetUserInfo API
	/// </summary>
	public class GetUserInfoList : BaseService
	{
		#region member variable

		private IGetUserInfoListDao _dao;

		#endregion

		#region constructor

		/// <summary>
		/// One argument constructor
		/// </summary>
		/// <param name="dao"></param>
		public GetUserInfoList(IGetUserInfoListDao dao) : base(dao)
		{
			_dao = dao;
		}

		#endregion

		#region Public method

		/// <summary>
		/// Process
		/// </summary>
		/// <param name="authInfo"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public ResGetUserInfoList Process(AuthenticationInfo authInfo, Paging iPagingPara, UserFilterInfo objFilterInfo)
		{
			DateTime timeStart = DateTime.UtcNow;
			ResGetUserInfoList response = ResponseFactory.GetUserInfoList(authInfo);
			try
			{
				// Start log
				LogAPIHelper.APIStart(this, authInfo);

				// Authenticate Login
				User user = Login(authInfo, _dao);

				// Validate Input
				ValidateInputParameter(iPagingPara, objFilterInfo);

				// Get User Info
				List<UserInfoList> userDetails = _dao.GetUserInfoList(iPagingPara, objFilterInfo);
                System.Collections.Generic.List<UserInfoList> userDetailInfoList = null;
                // DataConvert (DAO to API)
                GetUserInfoListConverter.DaoToAPI(userDetails, out userDetailInfoList);
				response.UserDetailInfoList = userDetailInfoList;

				//Result ACK
				response.Result = new Result();
			}
			catch (Exception ex)
			{
				response.Result = CreateResult(ex);
			}
			finally
			{
				// End log
				LogAPIHelper.APIEnd(this, response.Result, timeStart);
			}

			return response;
		}

		

		#endregion

		#region Private method

		/// <summary>
		/// Validate Input Parameter
		/// </summary>
		/// <param name="userID"></param>
		private void ValidateInputParameter(Paging iPagingPara, UserFilterInfo objFilterInfo)
		{
			UserInfoListValidator.Validate(iPagingPara, objFilterInfo);
		}

		#endregion
	}
}