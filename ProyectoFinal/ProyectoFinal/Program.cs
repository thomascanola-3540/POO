using API.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Servicios.Comandos;
using Servicios.Interfaces;
using Servicios.Servicios;
using System.Globalization;
using System.Text;

// Los decimales siempre se leen con punto. Sin esto, en un computador en español
// la longitud "-75.378" del formulario se interpreta como -75378.
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
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
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Inyección de dependencias: los controladores piden la interfaz y aquí se decide qué clase la implementa.
builder.Services.AddScoped<IJwtServicios, JwtServicios>();
builder.Services.AddScoped<IAutenticacion, ServicioAutenticacion>();
builder.Services.AddScoped<IUsuario, ServicioUsuario>();
builder.Services.AddScoped<IRol, ServicioRol>();
builder.Services.AddScoped<IDepartamento, ServicioDepartamento>();
builder.Services.AddScoped<ICiudad, ServicioCiudad>();
builder.Services.AddScoped<IAve, ServicioAve>();
builder.Services.AddScoped<IArchivo, ServicioArchivo>();
builder.Services.AddScoped<IEvento, ServicioEvento>();
builder.Services.AddScoped<IDataset, ServicioDataset>();
builder.Services.AddScoped<GestorComandos>();

// CORS: el frontend corre en otra dirección y el navegador solo lo deja llamar a la API si ella lo autoriza.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    // Agrega el botón "Authorize" de Swagger para pegar el token.
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Pegue el token que devuelve api/Auth/Login"
    });
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Canto de Aves v1");
    });
}
else
{
    // En desarrollo el frontend usa http; redirigirlo a https rompería sus llamadas.
    app.UseHttpsRedirection();
}

app.UseCors("Frontend");

// El orden importa: primero se identifica al usuario y después se revisan sus permisos.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
