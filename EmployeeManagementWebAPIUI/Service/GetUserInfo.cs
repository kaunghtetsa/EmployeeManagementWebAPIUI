using System;

using ASM.EmployeeManagement.DataAccess.Model;
using ASM.EmployeeManagement.WebAPIUI.DataConvert;
using ASM.EmployeeManagement.WebAPIUI.Common.Logger;
using ASM.EmployeeManagement.WebAPIUI.Validation.GetUserInfo;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.GetUserInfo;
using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfo;

namespace ASM.EmployeeManagement.WebAPIUI.Service
{
    /// <summary>
    /// GetUserInfo API
    /// </summary>
    public class GetUserInfo : BaseService
    {
        #region member variable

        private IGetUserInfoDao _dao;

        #endregion

        #region constructor

        /// <summary>
        /// One argument constructor
        /// </summary>
        /// <param name="dao"></param>
        public GetUserInfo(IGetUserInfoDao dao) : base(dao)
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
        public ResGetUserInfo Process(AuthenticationInfo authInfo, string userID)
        {
            DateTime timeStart = DateTime.UtcNow;
            ResGetUserInfo response = ResponseFactory.GetUserInfo(authInfo);
            try
            {
                // Start log
                LogAPIHelper.APIStart(this, authInfo);

                // Authenticate Login
                User user = Login(authInfo, _dao);

                // Validate Input
                ValidateInputParameter(userID);

                // Get User Info
                UserDetails userDetails = _dao.GetUserInfo(userID);
                UserDetailInfo userDetailInfo;
                // DataConvert (DAO to API)
                GetUserInfoConverter.DaoToAPI(userDetails, out userDetailInfo);
                response.UserDetailInfo = userDetailInfo;

                // Result ACK
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
        private void ValidateInputParameter(string userID)
        {
            UserInfoValidator.Validate(userID);
        }

        #endregion
    }
}