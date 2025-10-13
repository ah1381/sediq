using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Raya.Hrm.Shared.Library.Consts;
using Raya.Hrm.Shared.Library.Enums;
using Raya.Hrm.Shared.Library.Models.Auth;
using Raya.Hrm.Shared.Library.Models.Base;
using Raya.Hrm.Shared.Library.Models.Configs;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Raya.Hrm.Shared.Library.GenaralAuthService
{
    public interface IAuthService
    {
        Task<int> StoreUserAsync(CreateUserInfo model);
        Task<LoginResponseFromDbModel?> GetUser(string username);

        Task<LoginResponseFromDbModel?> GetUserForLogin(string username);

        Task CreateRole(Role request);
        Task CreatePermission(Permission request);

    }
    public class AuthService : BaseRepository, IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string username = string.Empty;


        public AuthService(IOptions<ServicesDbConfig> dbSettings, IHttpContextAccessor httpContextAccessor) : base(dbSettings.Value.DefaultConnection,dbSettings.Value.ConnectionType)
        {
            _httpContextAccessor = httpContextAccessor;
            username = _httpContextAccessor.HttpContext?.User?.FindFirst("Username")?.Value ?? "System";
        }

        public async Task AssignRole(List<Role> request,long memberId)
        {
            using var connection = CreateConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                if (request?.Any() == true)
                {
                    const string insertUserRoleSql = @"
                    INSERT INTO auth.user_role (""AuthenticationID"", ""RoleID"", ""CreatedAt"", ""CreatedBy"")
                    VALUES (@AuthenticationID, @RoleID, @CreatedAt, @CreatedBy)";

                    foreach (var role in request)
                    {
                        await connection.ExecuteAsync(insertUserRoleSql,
                            new
                            {
                                AuthenticationID = memberId,
                                RoleID = role.RowId,
                                CreatedAt = DateTime.UtcNow,
                                CreatedBy = username ?? "System"
                            }, transaction);
                    }
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<int> StoreUserAsync(CreateUserInfo model)
        {
            using var connection = CreateConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                const string insertUserSql = @"
                INSERT INTO auth.authentication (""Username"", ""Password"", ""OwnerProject"", ""UserType"") 
                VALUES (@Username, @Password, @OwnerProject, @UserType)
                RETURNING ""RowId""";

                var userId = await connection.QuerySingleAsync<long>(insertUserSql,
                    new
                    {
                        Username = model.Username,
                        Password = model.Password,
                        OwnerProject = Projects.RayaPersonProject,
                        UserType = (int)(UserType.Normal)
                    }, transaction);

                if (model.Roles?.Any() == true)
                {
                    const string insertUserRoleSql = @"
                    INSERT INTO auth.user_role (""AuthenticationID"", ""RoleID"", ""CreatedAt"", ""CreatedBy"")
                    VALUES (@AuthenticationID, @RoleID, @CreatedAt, @CreatedBy)";

                    foreach (var role in model.Roles)
                    {
                        await connection.ExecuteAsync(insertUserRoleSql,
                            new
                            {
                                AuthenticationID = userId,
                                RoleID = role.RowId,
                                CreatedAt = DateTime.UtcNow,
                                CreatedBy = model.CurrentUser ?? "System"
                            }, transaction);
                    }
                }

                transaction.Commit();
                return (int)userId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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

        public async Task CreateRole(Role request)
        {
            using var connection = CreateConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                const string insertRoleSql = @"
                INSERT INTO auth.role (""Name"", ""Description"")
                VALUES (@Name, @Description)
                RETURNING ""RowId""";

                var roleId = await connection.QuerySingleAsync<long>(insertRoleSql,
                    new { Name = request.Name, Description = request.Description }, transaction);

                if (request.Permissions?.Any() == true)
                {
                    const string insertPermissionRoleSql = @"
                    INSERT INTO auth.permission_role (""RoleID"", ""PermissionID"", ""CreatedAt"", ""CreatedBy"")
                    VALUES (@RoleID, @PermissionID, @CreatedAt, @CreatedBy)";

                    foreach (var permission in request.Permissions)
                    {
                        await connection.ExecuteAsync(insertPermissionRoleSql,
                            new
                            {
                                RoleID = roleId,
                                PermissionID = permission.RowId,
                                CreatedAt = DateTime.UtcNow,
                                CreatedBy = "System"
                            }, transaction);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task CreatePermission(Permission request)
        {
            using var connection = CreateConnection();
            const string sql = @"
            INSERT INTO auth.permission (""ControllerName"", ""ActionName"")
            VALUES (@ControllerName, @ActionName)";

            await connection.ExecuteAsync(sql,
                new
                {
                    ControllerName = request.ControllerName,
                    ActionName = request.ActionName
                });
        }
    }
}
