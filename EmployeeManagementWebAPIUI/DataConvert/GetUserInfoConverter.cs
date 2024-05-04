using System;

using ASM.EmployeeManagement.WebAPIUI.Common.Logger;
using ASM.EmployeeManagement.WebAPIUI.Common.Defines;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfo;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;
using System.Collections.Generic;

namespace ASM.EmployeeManagement.WebAPIUI.DataConvert
{
    /// <summary>
    /// GetUserInfo Converter
    /// </summary>
    public class GetUserInfoConverter
    {
        #region (DAO -> APIUI)

        /// <summary>
        /// DaoToAPI
        /// </summary>
        /// <param name="userDetails"></param>
        /// <param name="userDetailInfo"></param>
        public static void DaoToAPI(UserDetails userDetails, out UserDetailInfo userDetailInfo)
        {
            userDetailInfo = null;
            try
            {
                if(userDetails != null)
                {
                    userDetailInfo = new UserDetailInfo()
                    {
                        UserID = userDetails.UserID,
                        UserName = userDetails.UserName,
                        DepartmentName = userDetails.DepartmentName,
                        Gender = userDetails.Gender,
                        DateOfBirth = userDetails.DateOfBirth.ToString(Constant.DateFormat),
                        Address = userDetails.Address,
                        Email = userDetails.Email,
                        PhoneNo = userDetails.PhoneNo,
                        JobStartDate = userDetails.JobStartDate?.ToString(Constant.DateFormat)
                    };
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