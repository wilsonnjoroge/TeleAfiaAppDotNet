
using MediatR;
using System.Reflection;
using TeleAfiaAppDotNet.Application.Authentication.Commands.ForgotPassword;
using TeleAfiaAppDotNet.Application.Authentication.Commands.Register.RegisterUser;
using TeleAfiaAppDotNet.Application.Authentication.Commands.ResetPassword;
using TeleAfiaAppDotNet.Application.Interfaces.UserRoleAndTypeRepositories;
using TeleAfiaAppDotNet.Application.Interfaces;
using TeleAfiaAppDotNet.Application.UserCRUD.Commands.DeleteUser;
using TeleAfiaAppDotNet.Application.UserCRUD.Commands.UpdateUser;
using TeleAfiaAppDotNet.Application.UserCRUD.Queries.GetUser;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.AddRole;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.DeleteRole;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Commands.UpdateRole;
using TeleAfiaAppDotNet.Application.UserRoleManagement.Queries.GetRoles;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.DeleteUserType;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.UpdateUserType;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Queries.GetUserTypes;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ForgotPasswordDTOs;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.LoginDTOs;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.RegisterUserDTOs;
using TeleAfiaAppDotNet.Contracts.AuthenticationDTOs.ResetPasswordDTOs;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.DeleteUserDTOs;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.GetUserDTOs;
using TeleAfiaAppDotNet.Contracts.UserCrudDTOs.UpdateUserDTOs;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserRoleDTOs;
using TeleAfiaAppDotNet.Contracts.UserRoleAndTypeManagementDTOs.UserTypeDTOs;
using TeleAfiaAppDotNet.Domain.RolesAndTypesAggregate.RolesAndTypesEntity;
using TeleAfiaAppDotNet.Domain.UserAggregate.UsersEntities;
using TeleAfiaAppDotNet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TeleAfiaAppDotNet.Infrastructure.Repositories.UserRoleAndTypeManagement;
using TeleAfiaAppDotNet.Infrastructure.Repositories;
using TeleAfiaAppDotNet.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeleAfiaAppDotNet.Application.Authentication.Queries.LogIn;
using TeleAfiaAppDotNet.Application.UserTypesManagement.Commands.AddUserType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MediatR for handling commands and queries
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // Adjust with appropriate AutoMapper profile or class

// Register command and query handlers
builder.Services.AddTransient<IRequestHandler<RegisterUserCommand, RegisterResponse>, RegisterUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<LoginQuery, LoginResponse>, LoginQueryHandler>();
builder.Services.AddTransient<IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>, ForgotPasswordCommandHandler>();
builder.Services.AddTransient<IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>, ResetPasswordCommandHandler>();

// User operations
builder.Services.AddTransient<IRequestHandler<GetUserQuery, GetUserResponse>, GetUserQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllUsersQuery, List<User>>, GetAllUsersQueryHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateUserCommand, UpdateUserResponse>, UpdateUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteUserCommand, DeleteUserResponse>, DeleteUserCommandHandler>();

// User Roles operations
builder.Services.AddTransient<IRequestHandler<AddRoleCommand, UserRoleResponse>, AddRoleCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateRoleCommand, UserRoleResponse>, UpdateRoleCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteRoleCommand, UserRoleResponse>, DeleteRoleCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetRoleQuery, List<Role>>, GetRoleQueryHandler>();

// User Types operations
builder.Services.AddTransient<IRequestHandler<GetUserTypeQuery, List<UserType>>, GetUserTypeQueryHandler>();
builder.Services.AddTransient<IRequestHandler<AddUserTypeCommand, UserTypeResponse>, AddUserTypeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateUserTypeCommand, UserTypeResponse>, UpdateUserTypeCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteUserTypeCommand, UserTypeResponse>, DeleteUserTypeCommandHandler>();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<IPractitionerTypeRepository, PractitionerTypeRepository>();
builder.Services.AddScoped<IPractitionerRepository, PractitionerRepository>();

// Register JwtSettings from appsettings.json
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register JwtTokenGenerator
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Adjust if necessary
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

