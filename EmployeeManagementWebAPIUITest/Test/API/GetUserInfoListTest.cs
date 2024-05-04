using ASM.EmployeeManagement.DataAccess.Common.FilterInfo;
using ASM.EmployeeManagement.DataAccess.Common.Paging;
using ASM.EmployeeManagement.DataAccess.Dao.WebAPIUI.GetUserInfoList;
using ASM.EmployeeManagement.DataAccess.Model;
using ASM.EmployeeManagement.DataAccess.Model.WebAPI.GetUserInfoList;
using ASM.EmployeeManagement.WebAPIUI.Common.Defines;
using ASM.EmployeeManagement.WebAPIUI.Common.Exception;
using ASM.EmployeeManagement.WebAPIUI.Model.Common.Request;
using ASM.EmployeeManagement.WebAPIUI.Model.GetUserInfoList.Response;
using ASM.EmployeeManagement.WebAPIUI.Service;
using EmployeeManagementWebAPIUITest.Test.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using static ASM.EmployeeManagement.WebAPIUI.Common.Defines.Constant;

namespace EmployeeManagementWebAPIUITest.Test.API
{
	[TestClass]
	public class GetUserInfoListTest : WebAPITestBase
	{
		#region Public Methods

		/// <summary>
		/// Normal case
		/// </summary>
		[TestMethod]
		public void TestGetUserInfoList_NormalCase()
		{
			// Mock set up
			var mockDao = new Mock<IGetUserInfoListDao>();
			mockDao.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());

			mockDao.Setup(m => m.GetUserInfoList(It.IsAny<Paging>(), It.IsAny<UserFilterInfo>())).Returns(new List<UserInfoList>()
			{new UserInfoList(){
				UserID = "acty.sduser1",
				UserName = "SD User 1",
				DepartmentName = "Solution Development",
				Gender = 2,
				MaritalStatus = 1,
				DateOfBirth = Convert.ToDateTime("1987/07/07"),
				Address = null,
				Email = "acty.sduser1@gmail.com",
				PhoneNo = "+9592345604",
				JobStartDate = Convert.ToDateTime("2010/05/22")
			} ,{
				new UserInfoList()
				{
					UserID = "acty.sduser2",
					UserName = "SD User 2",
					DepartmentName = "Solution Development",
					Gender = 1,
					MaritalStatus = 1,
					DateOfBirth = Convert.ToDateTime("1977/06/04"),
					Address = null,
					Email = "acty.sduser2@gmail.com",
					PhoneNo = "+9592234544",
					JobStartDate = Convert.ToDateTime("2013/08/09")
				} }
			});

			ResGetUserInfoList response = null;
			GetUserInfoList instance = new GetUserInfoList(mockDao.Object);

			// Input 
			AuthenticationInfo authInfo = GetAuthenticationInfo("acty.hr", "khs");
			Paging newPagingObject = new Paging();
			newPagingObject.Num = 10;
			newPagingObject.SortKey = 1;
			newPagingObject.SortOrder = 1;
			newPagingObject.StartIndex = 0;
			UserFilterInfo newUserFilterInfo = new UserFilterInfo();
			newUserFilterInfo.IsExactMatchSearch = true;
			newUserFilterInfo.UserID ="acty.hr";
			newUserFilterInfo.Email = "acty.hr@gmail.com";

			// Get User Detail Info
			response = instance.Process(authInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoACK, response.Result.ResultCode);
			Assert.AreNotEqual(null, response.UserDetailInfoList);
			Assert.AreEqual(response.UserDetailInfoList[0].UserID, "acty.sduser1");
			Assert.AreEqual(response.UserDetailInfoList[0].UserName, "SD User 1");
			Assert.AreEqual(response.UserDetailInfoList[0].DepartmentName, "Solution Development");
			Assert.AreEqual(response.UserDetailInfoList[0].Gender, 2);
			Assert.AreEqual(response.UserDetailInfoList[0].MaritalStatus, 1);
			Assert.AreEqual(response.UserDetailInfoList[0].DateOfBirth, Convert.ToDateTime("1987/07/07"));
			Assert.AreEqual(response.UserDetailInfoList[0].Address, null);
			Assert.AreEqual(response.UserDetailInfoList[0].PhoneNo, "+9592345604");
			Assert.AreEqual(response.UserDetailInfoList[0].JobStartDate, Convert.ToDateTime("2010/05/22"));
		}
		/// <summary>
		/// Irregular Case(AuthenticationInfo)
		/// </summary>
		[TestMethod]
		public void TestGetUserInfoList_IrregularCase_AuthenticationInfo()
		{
			// Mock set up
			var mockDao = new Mock<IGetUserInfoListDao>();
			GetUserInfoList instance = new GetUserInfoList(mockDao.Object);
			Paging newPagingObject = new Paging();
			newPagingObject.Num = 10;
			newPagingObject.SortKey = 1;
			newPagingObject.SortOrder = 1;
			newPagingObject.StartIndex = 0;
			UserFilterInfo newUserFilterInfo = new UserFilterInfo();
			newUserFilterInfo.IsExactMatchSearch = true;
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = "acty.hr@gmail.com";
			// Check with null Authentication Info
			ResGetUserInfoList response = instance.Process(null, newPagingObject,newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with null LoginID
			AuthenticationInfo authenticationInfo = GetAuthenticationInfo(null, "khs");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with empty LoginID
			authenticationInfo = GetAuthenticationInfo("", "khs");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with whitespace LoginID
			authenticationInfo = GetAuthenticationInfo(" ", "khs");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with invalid LoginID length
			authenticationInfo = GetAuthenticationInfo("user12314567891231456789123145678912314567891231456789", "khs");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.LoginID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with null Password
			authenticationInfo = GetAuthenticationInfo("acty.hr", null);
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with empty Password
			authenticationInfo = GetAuthenticationInfo("acty.hr", "");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with whitespace Password
			authenticationInfo = GetAuthenticationInfo("acty.hr", " ");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with invalid Password length
			authenticationInfo = GetAuthenticationInfo("acty.hr", "user12314567891231456789123145678912314567891231456789");
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(AuthenticationInfo.Password)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);
		}

		/// <summary>
		/// Irregular Case FilterInfo(UserID)
		/// </summary>
		[TestMethod]
		public void TestGetUserInfoList_IrregularCase_FilterInfo_UserID()
		{
			// Mock set up
			var mockDao = new Mock<IGetUserInfoListDao>();
			mockDao.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
			Paging newPagingObject = new Paging();
			newPagingObject.Num = 10;
			newPagingObject.SortKey = 1;
			newPagingObject.SortOrder = 1;
			newPagingObject.StartIndex = 0;
			UserFilterInfo newUserFilterInfo = new UserFilterInfo();
			newUserFilterInfo.IsExactMatchSearch = true;
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = "acty.hr@gmail.com";
			GetUserInfoList instance = new GetUserInfoList(mockDao.Object);
			AuthenticationInfo authenticationInfo = GetAuthenticationInfo("acty.hr", "khs");

			//// Check with null FilterInfo
			//ResGetUserInfoList response = instance.Process(authenticationInfo, newPagingObject, null);
			//Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			//Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.FilterInfo)), response.Result.ErrorDetail);
			//Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with empty UserID
			newUserFilterInfo.UserID = "";
			ResGetUserInfoList response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with invalid length UserID
			newUserFilterInfo.UserID = "acty.hr238456836458632856283057027354934574394375837597493579";
			newUserFilterInfo.Email = "acty.hr@gmail.com";
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with space UserID
			newUserFilterInfo.UserID = " ";
		    response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with invalid null UserID
			newUserFilterInfo.UserID = null;
			newUserFilterInfo.Email = "acty.hr@gmail.com";
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.UserID)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

		}

		/// <summary>
		/// Irregular Case FilterInfo(Email)
		/// </summary>
		[TestMethod]
		public void TestGetUserInfoList_IrregularCase_FilterInfo_Email()
		{
			var mockDao = new Mock<IGetUserInfoListDao>();
			mockDao.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
			Paging newPagingObject = new Paging();
			newPagingObject.Num = 10;
			newPagingObject.SortKey = 1;
			newPagingObject.SortOrder = 1;
			newPagingObject.StartIndex = 0;
			UserFilterInfo newUserFilterInfo = new UserFilterInfo();
			newUserFilterInfo.IsExactMatchSearch = true;
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = "acty.hr@gmail.com";
			GetUserInfoList instance = new GetUserInfoList(mockDao.Object);
			AuthenticationInfo authenticationInfo = GetAuthenticationInfo("acty.hr", "khs");

			// Check with empty Email
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = "";
			ResGetUserInfoList response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.Email)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with invalid length Email
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = "acty.hr234234523542345234523453245235423453254253423543254235423542354@gmail.com";
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.Email)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with space Email
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = " ";
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.Email)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

			// Check with null Email
			newUserFilterInfo.UserID = "acty.hr";
			newUserFilterInfo.Email = null;
			response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
			Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
			Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.Email)), response.Result.ErrorDetail);
			Assert.AreEqual(null, response.UserDetailInfoList);

		}

		/// <summary>
		/// Irregular Case(PagingInfo)
		/// </summary>
		//[TestMethod]
		//public void TestGetUserInfoList_IrregularCase_PagingInfo()
		//{
		//	var mockDao = new Mock<IGetUserInfoListDao>();
		//	mockDao.Setup(m => m.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(new User());
		//	Paging newPagingObject = new Paging();
		//	newPagingObject.Num = 10;
		//	newPagingObject.SortKey = 1;
		//	newPagingObject.SortOrder = 1;
		//	newPagingObject.StartIndex = 0;
		//	UserFilterInfo newUserFilterInfo = new UserFilterInfo();
		//	newUserFilterInfo.IsExactMatchSearch = true;
		//	newUserFilterInfo.UserID = "acty.hr";
		//	newUserFilterInfo.Email = "acty.hr@gmail.com";
		//	GetUserInfoList instance = new GetUserInfoList(mockDao.Object);
		//	AuthenticationInfo authenticationInfo = GetAuthenticationInfo("acty.hr", "khs");

		//// Check with null Paging&FilterInfo
		//ResGetUserInfoList response = instance.Process(authenticationInfo, null, null);
		//Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
		//Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.AllPara)), response.Result.ErrorDetail);
		//Assert.AreEqual(null, response.UserDetailInfoList);

		//// Check with null Paging
		//response = instance.Process(authenticationInfo, null, newUserFilterInfo);
		//Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
		//Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.Paging)), response.Result.ErrorDetail);
		//Assert.AreEqual(null, response.UserDetailInfoList);


		//	// Check with PagingSize=-1
		//	newPagingObject.Num = -1;
		//	ResGetUserInfoList response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
		//	Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
		//	Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.PageSize)), response.Result.ErrorDetail);
		//	Assert.AreEqual(null, response.UserDetailInfoList);

		//	// Check with SortKey=-1
		//	newPagingObject.SortKey = -1;
		//	response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
		//	Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
		//	Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.SortKey)), response.Result.ErrorDetail);
		//	Assert.AreEqual(null, response.UserDetailInfoList);

		//	// Check with SortOrder=-1
		//	newPagingObject.SortOrder = -1;
		//	response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
		//	Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
		//	Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.SortOrder)), response.Result.ErrorDetail);
		//	Assert.AreEqual(null, response.UserDetailInfoList);

		//	// Check with StartIndex=-1
		//	newPagingObject.StartIndex = -1;
		//	response = instance.Process(authenticationInfo, newPagingObject, newUserFilterInfo);
		//	Assert.AreEqual(APIInfo.ResultInfoNACK, response.Result.ResultCode);
		//	Assert.AreEqual(string.Format("{0} : {1} ({2})", InputParameterException.MessageIDType.E000.ToString(), Constant.ErrorMessageE000, nameof(PropertyName.PagingIndex)), response.Result.ErrorDetail);
		//	Assert.AreEqual(null, response.UserDetailInfoList);
		//}

		#endregion Public Methods
	}

}