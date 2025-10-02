using Application.Abstract;
using Infrastructure.Repository.Helpers;
using Microsoft.AspNetCore.Http.Features;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var versionApp = builder.Configuration.GetSection("Version").Value;
var nameApp = builder.Configuration.GetSection("NameApp").Value + versionApp;
var conexionGeneral = builder.Configuration.GetConnectionString("MongoDBAtlasUri");

// Carga los valores de MongoDB al Singleton
MongoDBCommonHelpers.Instance.MongoDBConnectionUri = conexionGeneral;
MongoDBCommonHelpers.Instance.DatabaseName = builder.Configuration.GetConnectionString("DatabaseName");

// Inicializar AutoMapper (¡Una sola vez!)
AutoMapperConfig.Initialize();
//<summary>
//configuracion Inyección de dependencias en controladores, desde la Capa de Aplicación.
//</summary>
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddHttpContextAccessor();
//<summary>
//Configuración Inyección de dependencias en services, desde la Capa de Dominio (Modelo).
//</summary>
// Repositorio (Scoped, para mantener la conexión por solicitud)
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    var origins = new List<string>();
    origins.Add(TransversalHelpers.Instance.originDefault);
    options.AddPolicy(name: "AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(origins.ToArray())
                   .AllowAnyHeader();
        });
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 314572800;
});


var app = builder.Build();
app.UseSwagger();
app.Use(async (context, next) =>
{
    context.Features.Get<IHttpMaxRequestBodySizeFeature>()!.MaxRequestBodySize = 314572800;
    await next();
});


app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", nameApp);
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
