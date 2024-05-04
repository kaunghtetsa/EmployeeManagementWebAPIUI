using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ASM.EmployeeManagement.WebAPIUI.Common.Exception
{
    /// <summary>
    /// InputParameterException
    /// </summary>
    [Serializable]
    public class InputParameterException : WebAPIUIException
    {
        #region Message ID enum

        public new enum MessageIDType
        {
            E000,       // Input check error
        }

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ErrorColumn"></param>
        /// <param name="messageID"></param>
        public InputParameterException(MessageIDType messageID, string[] errParam, [System.Runtime.CompilerServices.CallerLineNumber] int nLineNumber = 0)
        {
            StackFrame sf = new StackFrame(1, true);
            SetClassName(sf.GetMethod().ReflectedType.FullName);
            SetLineNumber(nLineNumber);
            Initialize(messageID, errParam);
        }

        #endregion        

        #region override method

        /// <summary>
        /// GetObjectData
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
        /// Initialize
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="errParam"></param>
        private void Initialize(MessageIDType messageID, string[] errParam)
        {
            MessageID = Enum.GetName(typeof(MessageIDType), messageID);
            Params = errParam;
        }

        #endregion    
    }
}