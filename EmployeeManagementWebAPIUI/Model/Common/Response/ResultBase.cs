using System.Runtime.Serialization;

namespace ASM.EmployeeManagement.WebAPIUI.Model.Common.Response
{
    [DataContract]
    public class ResultBase : ClientInfo
    {
        #region Properties

        [DataMember(Order = 1)]
        public Result Result { get; set; }

        #endregion
    }
}