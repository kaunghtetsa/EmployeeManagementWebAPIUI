using System.Runtime.Serialization;

namespace ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response
{
    /// <summary>
    /// User Detail Info
    /// </summary>
    [DataContract]
    public class UserDetailInfo
    {
        [DataMember(Order = 1)]
        public string UserID { get; set; }

        [DataMember(Order = 2)]
        public string UserName { get; set; }

        [DataMember(Order = 3)]
        public string DepartmentName { get; set; }

        [DataMember(Order = 4)]
        public short Gender { get; set; }

        [DataMember(Order = 5)]
        public string DateOfBirth { get; set; }

        [DataMember(Order = 6)]
        public string Address { get; set; }

        [DataMember(Order = 7)]
        public string Email { get; set; }

        [DataMember(Order = 8)]
        public string PhoneNo { get; set; }

        [DataMember(Order = 9)]
        public string JobStartDate { get; set; }

		[DataMember(Order = 10)]
		public bool MaritalStatus { get; set; }
		
	}
}