using System.Runtime.Serialization;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Response;

namespace ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response
{
    /// <summary>
    /// Response of GetUserInfo
    /// </summary>
    [DataContract]
    public class ResGetUserInfo : ResultBase
    {
        [DataMember(Order = 2)]
        public UserDetailInfo UserDetailInfo { get; set; }
    }
}