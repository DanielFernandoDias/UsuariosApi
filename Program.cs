using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Model;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionStringUsuario = builder.Configuration.GetConnectionString("UsuarioConnection");
builder.Services.AddDbContext<UsuarioDbContext>(
    opts =>
    {
        opts.UseMySql(connectionStringUsuario, ServerVersion.AutoDetect(connectionStringUsuario));
    });

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

// Configurando o AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Inje��o de depedencia ao nosso servi�o
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
    {
        policy.AddRequirements(new IdadeMinima(18));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
