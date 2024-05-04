using ASM.EmployeeManagement.WebAPIUI.Behavior;
using ASM.EmployeeManagement.WebAPIUI.Service;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;

using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfoList.Response;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.GetUserInfo;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.GetUserInfoList;
using ASM.EmployeeManagement.DataAccess.Common.Paging;
using ASM.EmployeeManagement.DataAccess.Common.FilterInfo;

namespace ASM.EmployeeManagement.WebAPIUI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeManagementWebAPIUIService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeManagementWebAPIUIService.svc or EmployeeManagementWebAPIUIService.svc.cs at the Solution Explorer and start debugging.
    [APIServiceBehavior]
    public class EmployeeManagementWebAPIUIService : IEmployeeManagementWebAPIUIService
    {
        /// <summary>
        /// GetUserInfo
        /// </summary>
        /// <param name="authInfo"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ResGetUserInfo GetUserInfo(AuthenticationInfo authInfo, string userID)
        {
            IGetUserInfoDao dao = new GetUserInfoDao();
            GetUserInfo instance = new GetUserInfo(dao);
            return instance.Process(authInfo, userID);
        }
		public ResGetUserInfoList GetUserInfoList(AuthenticationInfo authInfo, Paging iPagingPara, UserFilterInfo objFilterInfo)
		{
			IGetUserInfoListDao dao = new GetUserInfoListDao();
			GetUserInfoList instance = new GetUserInfoList(dao);
			return instance.Process(authInfo, iPagingPara, objFilterInfo);
		}
	}
}
