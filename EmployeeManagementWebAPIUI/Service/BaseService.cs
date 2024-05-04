using System.Runtime.CompilerServices;

using ASM.EmployeeManagement.Common.Exception;
using ASM.EmployeeManagement.DataAccess.Model;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.Common;
using ASM.EmployeeManagement.DataAccess.Exception;
using ASM.EmployeeManagement.WebAPIUI.Common.DataAccess;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using ASM.EmployeeManagement.WebAPIUI.Common.Logger;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Validation.Common;

namespace ASM.EmployeeManagement.WebAPIUI.Service
{
    /// <summary>
    /// Base class for all service classes
    /// </summary>
    abstract public class BaseService
    {
        #region Private Members
        /// <summary>
        /// Initialize flag
        /// </summary>
        private static bool _initialized = false;
        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dao"></param>
        protected BaseService(IWebAPIUICommonDao dao)
        {
            if (_initialized == false)
            {
                LogAPIHelper.Initialize();

                dao.Initialize(DataAccessHelper.GetSettingFilePath());

                _initialized = true;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="authenticationInfo"></param>
        /// <param name="dao"></param>
        /// <returns></returns>
        protected User Login(AuthenticationInfo authenticationInfo, IWebAPIUICommonDao dao)
        {
            // Input Validate
            AuthenticationInfoValidator.Validate(authenticationInfo);

            // DataAccess Validate
            User objUser = dao.Login(authenticationInfo.LoginID, authenticationInfo.Password);

            return objUser;
        }

        /// <summary>
        /// CreateResult
        /// </summary>
        /// <param name="exIn"></param>
        /// <param name="sMethodName"></param>
        /// <param name="sFilePath"></param>
        /// <param name="nLineNumber"></param>
        /// <returns></returns>
        protected Result CreateResult(System.Exception exIn, [CallerMemberName] string sMethodName = "",
           [CallerFilePath] string sFilePath = "", [CallerLineNumber] int nLineNumber = 0)
        {
            Result ret = null;

            try
            {
                DaoException daoEx = exIn as DaoException;
                WebAPIUIException apiEx = exIn as WebAPIUIException;
                EmployeeException empEx = exIn as EmployeeException;

                if (daoEx != null)
                {
                    ret = WebAPIUIException.CreateResult(daoEx);
                    LogAPIHelper.Warn(this, ret.ErrorDetail);
                }
                else if (apiEx != null)
                {
                    ret = apiEx.CreateResult();
                    LogAPIHelper.Warn(this, ret.ErrorDetail);
                }
                else if (empEx != null)
                {
                    ret = WebAPIUIException.CreateResult(empEx);
                    LogAPIHelper.Error(this, ret.ErrorDetail);
                }
                else
                {
                    ret = WebAPIUIException.CreateUnknownResult(exIn.Message);
                    LogAPIHelper.Error(this, ret.ErrorDetail);
                }
            }
            catch (System.Exception ex)
            {
                LogAPIHelper.Error(this, exIn);
                LogAPIHelper.Error(this, ex);
                ret = WebAPIUIException.CreateUnknownResult("unknown error has occurred.");
            }

            return ret;
        }

        #endregion
    }
}