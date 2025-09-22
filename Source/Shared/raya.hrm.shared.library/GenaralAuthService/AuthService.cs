using Dapper;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Consts;
using Raya.Hrm.Shared.Library.Enums;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Text;

namespace Raya.Hrm.Shared.Library.GenaralAuthService
{
    public interface IAuthService
    {
        Task<int> StoreUserAsync(CreateUserInfo model);
        Task<LoginResponseFromDbModel?> GetUser(string username);

        Task<LoginResponseFromDbModel?> GetUserForLogin(string username);
    }
    public class AuthService : BaseRepository, IAuthService
    {
        public AuthService(IOptions<ServicesDbConfig> dbSettings) : base(dbSettings.Value.DefaultConnection)
        {
        }
        public async Task<int> StoreUserAsync(CreateUserInfo model)
        {

            using var connection = CreateConnection();
            const string sql = @"
            INSERT INTO auth.authentication ( ""Username"", ""Password"", ""OwnerProject"",""UserType"") 
            VALUES (@Username, @Password, @OwnerProject, @UserType)";

            var res = await connection.ExecuteAsync(sql,
                new
                {
                    Username = model.Username,
                    Password = model.Password,
                    OwnerProject = Projects.RayaPersonProject,
                    UserType = (int)(UserType.Normal)
                });

            return res;
        }

        public async Task<LoginResponseFromDbModel?> GetUser(string username)
        {
            using var connection = CreateConnection();
            var parameters = new DynamicParameters();
            username = username?.ToUpper();
            var queryBuilder = new StringBuilder(
                $@"SELECT * FROM auth.authentication where UPPER( ""Username"") = @username and ""OwnerProject"" ='{Projects.RayaPersonProject}'");
            parameters.Add("username", username);
            var res =
                await connection.
                    QueryFirstOrDefaultAsync<LoginResponseFromDbModel>(queryBuilder.ToString(), parameters);
            return res;
        }

        public async Task<LoginResponseFromDbModel?> GetUserForLogin(string username)
        {
            using var connection = CreateConnection();
            var parameters = new DynamicParameters();
            username = username.ToUpper();
            var queryBuilder = new StringBuilder(
                $@"SELECT * FROM auth.authentication where UPPER( ""Username"") = @username and
                (""OwnerProject"" ='{Projects.RayaAllProject}' or ""OwnerProject"" ='{Projects.RayaAllProject}')  ");
            parameters.Add("username", username);
            var res =
                await connection.
                    QueryFirstOrDefaultAsync<LoginResponseFromDbModel>(queryBuilder.ToString(), parameters);

            return res;
        }
    }
}
