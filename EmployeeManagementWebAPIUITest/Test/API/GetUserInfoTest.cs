using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ASM.EmployeeManagement.WebAPIUI.Service;
using ASM.EmployeeManagement.DataAccess.Model;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.GetUserInfo;
using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfo;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfo.Response;
using ASM.EmployeeManagement.WebAPIUI.Common.Defines;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using static ASM.EmployeeManagement.WebAPIUI.Common.Defines.Constant;
using EmployeeManagementWebAPIUITest.Test.Common;

namespace EmployeeManagementWebAPIUITest.Test.API
{
    /// <summary>
    /// Test class of GetUserInfo
    /// </summary>
    [TestClass]
    public class GetUserInfoTest : WebAPITestBase
    {
        #region Public Methods

        /// <summary>
        /// Normal case
        /// </summary>
        [TestMethod]
        public void TestGetUserInfo_NormalCase()
        {
            // Mock set up
            var mockDao = new Mock<IGetUserInfoDao>();
            mockDao.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());

            mockDao.Setup(m => m.GetUserInfo(It.IsAny<string>())).Returns(new UserDetails() {
                UserID = "acty.sduser1",
                UserName = "SD User 1",
                DepartmentName = "Solution Development",
                Gender = 2,
                DateOfBirth = Convert.ToDateTime("1987/07/07"),
                Address = null,
                Email = "acty.sduser1@gmail.com",
                PhoneNo = "+9592345604",
                JobStartDate = Convert.ToDateTime("2010/05/22")
            });

            ResGetUserInfo response = null;
            GetUserInfo instance = new GetUserInfo(mockDao.Object);

            // Input 
            AuthenticationInfo authInfo = GetAuthenticationInfo("acty.hr", "khs");

            // Get User Detail Info
            response = instance.Process(authInfo, "acty.sduser1");
            Assert.AreEqual(APIInfo.ResultInfoACK, response.Result.ResultCode);
            Assert.AreNotEqual(null, response.UserDetailInfo);
            Assert.AreEqual(response.UserDetailInfo.UserID, "acty.sduser1");
            Assert.AreEqual(response.UserDetailInfo.UserName, "SD User 1");
            Assert.AreEqual(response.UserDetailInfo.DepartmentName, "Solution Development");
            Assert.AreEqual(response.UserDetailInfo.Gender, 2);
            Assert.AreEqual(response.UserDetailInfo.DateOfBirth, Convert.ToDateTime("1987/07/07").ToString(Constant.DateFormat));
            Assert.AreEqual(response.UserDetailInfo.Address, null);
            Assert.AreEqual(response.UserDetailInfo.PhoneNo, "+9592345604");
            Assert.AreEqual(response.UserDetailInfo.JobStartDate, Convert.ToDateTime("2010/05/22").ToString(Constant.DateFormat));  
        }

        /// <summary>
        /// Irregular Case(AuthenticationInfo)
        /// </summary>
        [TestMethod]
        public void TestGetUserInfo_IrregularCase_AuthenticationInfo()
        {
            // Mock set up
            var mockDao = new Mock<IGetUserInfoDao>();
            GetUserInfo instance = new GetUserInfo(mockDao.Object);

            // Check with null Authentication Info
            ResGetUserInfo response = instance.Process(null, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with null LoginID
            AuthenticationInfo authenticationInfo = GetAuthenticationInfo(null, "khs");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with empty LoginID
            authenticationInfo = GetAuthenticationInfo("", "khs");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with whitespace LoginID
            authenticationInfo = GetAuthenticationInfo(" ", "khs");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with invalid LoginID length
            authenticationInfo = GetAuthenticationInfo("user12314567891231456789123145678912314567891231456789", "khs");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with null Password
            authenticationInfo = GetAuthenticationInfo("acty.hr", null);
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with empty Password
            authenticationInfo = GetAuthenticationInfo("acty.hr", "");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with whitespace Password
            authenticationInfo = GetAuthenticationInfo("acty.hr", " ");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with invalid Password length
            authenticationInfo = GetAuthenticationInfo("acty.hr", "user12314567891231456789123145678912314567891231456789");
            response = instance.Process(authenticationInfo, "acty.hr");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);
        }

        /// <summary>
        /// Irregular Case(UserID)
        /// </summary>
        [TestMethod]
        public void TestGetUserInfo_IrregularCase_UserID()
        {
            // Mock set up
            var mockDao = new Mock<IGetUserInfoDao>();
            mockDao.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());

            GetUserInfo instance = new GetUserInfo(mockDao.Object);
            AuthenticationInfo authenticationInfo = GetAuthenticationInfo("acty.hr", "khs");

            // Check with null UserID
            ResGetUserInfo response = instance.Process(authenticationInfo, null);
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with empty UserID
            response = instance.Process(authenticationInfo, "");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);

            // Check with invalid length UserID
            response = instance.Process(authenticationInfo, "user12314567891231456789123145678912314567891231456789");
            Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
            Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
            Assert.AreEqual(null, response.UserDetailInfo);
        }

        #endregion
    }
}
