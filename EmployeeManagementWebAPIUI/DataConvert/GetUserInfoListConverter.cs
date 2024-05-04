using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfoList;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using ASM.EmployeeManagement.WebAPIUI.Common.Logger;
using System;
using System.Collections.Generic;

namespace ASM.EmployeeManagement.WebAPIUI.DataConvert
{
	public class GetUserInfoListConverter
	{
		#region (DAO -> APIUI)

		/// <summary>
		/// DaoToAPI
		/// </summary>
		/// <param name="incomeUserInfoList"></param>
		/// <param name="resUserInfoList"></param>
		public static void DaoToAPI(List<UserInfoList> incomeUserInfoList, out List<UserInfoList> resUserInfoList)
		{
			resUserInfoList = null;
			try
			{
				if (incomeUserInfoList != null)
				{
					resUserInfoList = new List<UserInfoList>();
					for (int i = 0; i < incomeUserInfoList.Count; i++)
					{
						UserInfoList tempVar = new UserInfoList();
						tempVar.UserID = incomeUserInfoList[i].UserID;
						tempVar.UserName = incomeUserInfoList[i].UserName;
						tempVar.DepartmentName = incomeUserInfoList[i].DepartmentName;
						tempVar.Gender = incomeUserInfoList[i].Gender;
						tempVar.MaritalStatus = incomeUserInfoList[i].MaritalStatus;
						tempVar.DateOfBirth = incomeUserInfoList[i].DateOfBirth;
						tempVar.Address = incomeUserInfoList[i].Address;
						tempVar.Email = incomeUserInfoList[i].Email;
						tempVar.PhoneNo = incomeUserInfoList[i].PhoneNo;
						tempVar.JobStartDate = incomeUserInfoList[i].JobStartDate;
						resUserInfoList.Add(tempVar);
					}
				}
			}
			catch (Exception ex)
			{
				LogAPIHelper.Error(null, ex);
				throw new WebAPIUIException(ex);
			}
		}

		#endregion
	}
}