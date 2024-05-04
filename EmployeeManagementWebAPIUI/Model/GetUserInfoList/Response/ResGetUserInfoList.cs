using System.Collections.Generic;
using System.Runtime.Serialization;
using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfoList;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;

namespace ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfoList.Response
{
	[DataContract]
	public class ResGetUserInfoList : ResultBase
	{
		[DataMember(Order = 2)]
		public List<UserInfoList> UserDetailInfoList { get; set; }
	}
}