using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

using ASM.EmployeeManagement.Common.Exception;
using ASM.EmployeeManagement.DataAccess.Exception;
using ASM.EmployeeManagement.WebAPIUI.Common.Defines;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;

namespace ASM.EmployeeManagement.WebAPIUI.Common.Exception
{
    /// <summary>
    /// WebAPIUIException
    /// </summary>
    [Serializable]
    public class WebAPIUIException : EmployeeException
    {
        #region Message ID enum

        public enum MessageIDType
        {
            E999,              // An unexpected exception occurred.
        }

        protected const string DEFAULT_ERROR_MESSAGE = "An unexpected exception occurred.";

        #endregion

        #region Properties

        protected string MessageID { get; set; }

        public string ErrorDescription { get; set; }

        public string[] Params { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public WebAPIUIException()
        {
        }
        protected WebAPIUIException(EmployeeException ex)
            : base(ex)
        {
        }

        protected WebAPIUIException(DaoException ex)
            : base(ex)
        {
            MessageID = ex.ErrorId;
            Params = ex.ErrorParams;
        }

        public WebAPIUIException(System.Exception objEx, [CallerLineNumber] int nLineNumber = 0)
            : base(objEx)
        {
            StackFrame sf = new StackFrame(1, true);
            SetClassName(sf.GetMethod().ReflectedType.FullName);
            SetLineNumber(nLineNumber);
        }

        /// <summary>
        /// エラーコードとエラーの内容を作成する
        /// </summary>
        /// <param name="errCode"></param>
        /// <param name="sParam"></param>
        public WebAPIUIException(MessageIDType errCode, [CallerLineNumber] int nLineNumber = 0, params string[] sParam)
        {
            this.MessageID = Enum.GetName(typeof(MessageIDType), errCode);
            this.Params = sParam;

            StackFrame sf = new StackFrame(1, true);
            SetClassName(sf.GetMethod().ReflectedType.FullName);
            SetLineNumber(nLineNumber);
        }

        #endregion

        #region public method
        /// <summary>
        ///  結果オプジェクトを作成する。
        /// </summary>
        /// <returns>結果オプジェクト</returns>
        public virtual Result CreateResult()
        {
            if (this.MessageID == null)
            {
                this.MessageID = Enum.GetName(typeof(MessageIDType), MessageIDType.E999);
            }

            string errMessage = "";
            try
            {
                errMessage = GetErrMessage(this.MessageID);
            }
            catch (WebAPIUIException ex)
            {
                this.MessageID = ex.MessageID;
                errMessage = DEFAULT_ERROR_MESSAGE;
            }

            SetErrorDescription(errMessage);

            return new Result(this.MessageID, this.ErrorDescription);
        }

        /// <summary>
        /// 結果オブジェクトを作成する
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Result CreateResult(DaoException ex)
        {
            WebAPIUIException innerEx = new WebAPIUIException(ex);
            Result result = innerEx.CreateResult();

            return result;
        }

        /// <summary>
        /// 結果オプジェクトを作成する。
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Result CreateResult(EmployeeException ex)
        {
            WebAPIUIException innerEx = new WebAPIUIException(ex);
            innerEx.MessageID = Enum.GetName(typeof(MessageIDType), MessageIDType.E999);

            return innerEx.CreateResult();
        }

        /// <summary>
        ///  不明エラーの場合の結果オプジェクトを作成する。
        /// </summary>
        /// <param name="language">言語コード</param>
        /// <returns>結果オプジェクト</returns>
        public static Result CreateUnknownResult(string message)
        {
            Result ret = null;

            string codeID = Enum.GetName(typeof(MessageIDType), MessageIDType.E999);
            ret = new Result(codeID, message);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        #endregion

        #region private method
        /// <summary>
        /// エラーの内容をセットする。
        /// </summary>
        /// <param name="sMessage"></param>
        private void SetErrorDescription(string sMessage)
        {
            if (Params == null || Params.Length == 0)
            {
                ErrorDescription = sMessage;
            }
            else
            {
                ErrorDescription = string.Format("{0} ({1})", sMessage, string.Join(",", Params));
            }
        }

        /// <summary>
        /// Get error message
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        private static string GetErrMessage(string errCode)
        {
            string message = string.Empty;

            try
            {
                switch (errCode)
                {
                    case "E000":
                        message = Constant.ErrorMessageE000;
                        break;
                    case "E001":
                        message = Constant.ErrorMessageE001;
                        break;
                    case "E002":
                        message = Constant.ErrorMessageE002;
                        break;
                    case "E003":
                        message = Constant.ErrorMessageE003;
                        break;
					case "E004":
						message = Constant.ErrorMessageE004;
						break;
					default:
                        message = Constant.ErrorMessageE999;
                        break;
                }
            }
            catch (System.Exception)
            {
                throw new WebAPIUIException(MessageIDType.E999);
            }

            return message;
        }

        #endregion
    }
}