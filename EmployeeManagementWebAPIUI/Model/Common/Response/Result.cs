using ASM.EmployeeManagement.WebAPIUI.Common.Defines;
using System.Runtime.Serialization;

namespace ASM.EmployeeManagement.WebAPIUI.Model.Common.Response
{
    /// <summary>
    /// Result
    /// </summary>
    [DataContract]
    public class Result
    {
        #region Properties

        /// <summary>
        /// Result Code(1: ACK, 2: NACK)
        /// </summary>
        [DataMember(Order = 1)]
        public string ResultCode { get; set; }

        /// <summary>
        /// Error Detail
        /// </summary>
        [DataMember (Order = 2)]
        public string ErrorDetail { get; set; }

        #endregion

        #region Contructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isError"></param>
        public Result(bool isError = false)
        {
            ResultCode = isError ? APIInfo.ResultInfoNACK : APIInfo.ResultInfoACK;
        }

        /// <summary>
        /// コンストラクタ(NACK)
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="description"></param>
        public Result(string messageID, string description)
        {
            ResultCode = APIInfo.ResultInfoNACK;
            ErrorDetail = GetErrorDetail(messageID, description);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// GetErrorDetail
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private string GetErrorDetail(string messageID, string description)
        {
            return string.Format("{0} : {1}", messageID, description);
        }

        #endregion
    }
}