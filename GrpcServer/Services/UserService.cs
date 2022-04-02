using Grpc.Core;
using GrpcServer.Database;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcServer
{
    public class UserService : UserActivity.UserActivityBase
    {
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public override Task<SignInResponse> SignIn(SignInRequest request, ServerCallContext context)
        {
            DatabaseController databaseController = new DatabaseController();
            UserInfoModel userInfoModel = new UserInfoModel()
            {
                username = request.UserName,
                password = request.Password
            };
            var userId = databaseController.CheckUserInfo(userInfoModel);

            return Task.FromResult(new SignInResponse
            {
                StatusCode = userId >= 0 ? 200 : 401,
                Message = userId >= 0 ? "User, " + request.UserName + " Successfully logged in." : "Incorrect username or password."
            });
        }
        public override Task<SignUpResponse> SignUp(SignUpRequest request, ServerCallContext context)
        {
            DatabaseController databaseController = new DatabaseController();
            UserInfoModel userInfoModel = new UserInfoModel()
            {
                email = request.Email,
                username = request.UserName,
                password = request.Password
            };
            var userInsertResult = databaseController.InsertUserInfo(userInfoModel);


            return Task.FromResult(new SignUpResponse
            {
                StatusCode = userInsertResult > 0 ? 201 : 409,
                Message = userInsertResult > 0 ? "User, " + request.UserName + " Successfully signed up." : "Error. Username " + request.UserName + " is already taken."
            });
        }
    }
}
