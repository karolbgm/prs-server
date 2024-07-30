using Microsoft.EntityFrameworkCore;
using prs_server.Controllers;
using prs_server.Data;
using prs_server.Models;
using Moq.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;


namespace prsTest;

public class PRSTest 
{
    User user = default!; //this will give my tests an instance of user
    //UsersController userCtrl = default!;
    Request request = default!;
    //RequestsController requestCtrl = default!;

    public PRSTest() 
    {
        user = new User();
        request = new Request();

    }

    //All the tests here

    //[Fact]
    //public async Task TestLogin()
    //{
    //    // Arrange
    //    var userContextMock = new Mock<PrsDbContext>();
    //    userContextMock.Setup<DbSet<User>>(x => x.Users)
    //        .ReturnsDbSet(TestDataHelper.GetFakeUserList());

    //    //Act
    //    UsersController usersController = new(userContextMock.Object);
    //    var users = (await usersController.GetUser()).Value;

    //    //Assert
    //    Assert.NotNull(users);
    //    Assert.Equal(2, employees.Count());
    //}



    //[Theory]
    //[InlineData("karolgm@gmail.com", "123")]
    //[InlineData("greg@gmail.com", "greg")]
    //[InlineData("sa", "sa")]
    //public async void TestUserLogin(string name, string pass)
    //{
    //    user.Username = name;
    //    user.Password = pass;
    //    await userCtrl.Login(user.Username, user.Password);
    //}

    //[Theory]
    //[InlineData(50)]
    //public async void TestRequestReview(decimal total)
    //{
    //    request.Total = total;
    //    await requestCtrl.Review(request.Id, request);
    //    if (total <= 50 && total > 0)
    //    {
    //        Assert.Equal("APPROVED", request.Status);

    //    }
    //    else if (total > 50)
    //    {
    //        Assert.Equal("REVIEW", request.Status);

    //    }
    //}

    //[Fact]
    //public async void TestRequestApprove()
    //{
    //    await requestCtrl.Approve(request.Id, request);
    //    Assert.Equal("APPROVED", request.Status);
    //}

    //[Fact]
    //public async void TestRequestReject()
    //{
    //    await requestCtrl.Reject(request.Id, request);
    //    Assert.Equal("REJECTED", request.Status);
    //}

    //[Fact]
    //public async void TestGetReviews()
    //{
    //    var returnedList = await requestCtrl.GetReviews(user.Id);
    //    foreach (var item in returnedList)
    //    {
    //        Assert.Equal("REVIEW", item.Status);
    //        Assert.Equal(user.Id), item.UserId);

    //    }
    //}

    //[Theory]
    //[InlineData("APPROVED")]
    //[InlineData("REVIEW")]
    //[InlineData("REJECTED")]
    //[InlineData("NEW")]
    //public async void TestGetRequestByStatus(string status)
    //{
    //    var returnedStatus = await requestCtrl.GetRequestByStatus(status);
    //    foreach (var item in returnedStatus)
    //    {
    //        Assert.Equal(status, item.Status);

    //    }
    //}
}