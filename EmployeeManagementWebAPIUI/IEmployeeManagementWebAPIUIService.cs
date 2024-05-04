using System.ServiceModel;
using ASM.EmployeeManagement.DataAccess.Common.FilterInfo;
using ASM.EmployeeManagement.DataAccess.Common.Paging;
using ASM.EmployeeManagement.WebAPIUI.Behavior;
using ASM.EmployeeManagement.WebAPIUI.Common.Defines;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfoList.Response;

namespace ASM.EmployeeManagement.WebAPIUI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeManagementWebAPIUIService" in both code and config file together.
    [ServiceContract(Namespace = APIInfo.NameSpace + APIInfo.CurrentServiceVersion)]
    public interface IEmployeeManagementWebAPIUIService
    {
        [OperationContract]
        [APIOperationBehavior]
        ResGetUserInfo GetUserInfo(AuthenticationInfo authInfo, string userID);
		[OperationContract]
		[APIOperationBehavior]
		ResGetUserInfoList GetUserInfoList(AuthenticationInfo authInfo, Paging iPagingPara, UserFilterInfo objFilterInfo);
	}
}
