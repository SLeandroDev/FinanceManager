using FinanceManager.Application.Services;
using FinanceManager.Infrastructure.Persistence;
using FinanceManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Annotations;
using FinanceManager.Filters;

var builder = WebApplication.CreateBuilder(args);


// Pegando as configurações do JWT do appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Convert.FromBase64String(jwtSettings["SecretKey"]);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
    });


builder.Services.AddAuthorization();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7241"); // Substitua pela URL da sua API
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

// Configurando a conexão com o banco de dados
builder.Services.AddDbContext<FinanceManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrando os Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Registrando os Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<AuthFilter>();

builder.Services.AddSession();

// Adicionando o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();  // Adiciona a possibilidade de explorar endpoints
builder.Services.AddSwaggerGen();  
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); // Gera a documentação da API
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Habilita o Swagger
    app.UseSwaggerUI();  // Habilita a interface do Swagger
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
