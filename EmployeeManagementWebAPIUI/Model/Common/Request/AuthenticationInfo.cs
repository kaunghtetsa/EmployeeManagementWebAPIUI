using System.Runtime.Serialization;

namespace ASM.EmployeeManagement.WebAPIUI.Model.Common.Request
{
    [DataContract]
    public class AuthenticationInfo : ClientInfo
    {
        /// <summary>
        /// LoginID
        /// </summary>
        [DataMember(Order = 1)]
        public string LoginID { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [DataMember(Order = 2)]
        public string Password { get; set; }
    }
}