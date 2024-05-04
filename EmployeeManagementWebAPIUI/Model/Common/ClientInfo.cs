using System.Runtime.Serialization;

namespace ASM.EmployeeManagement.WebAPIUI.Model.Common
{
    /// <summary>
    /// Client Info
    /// </summary>
    [DataContract]
    public class ClientInfo
    {
        /// <summary>
        /// Service Reference Version
        /// </summary>
        public string ServiceReferenceVersion { get; set; }
    }
}