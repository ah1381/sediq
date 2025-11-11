using Example.Domain.DependencyInjection;
using Example.Service.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new Sediq.Web.Api.ModelBinders.PersianDateModelBinderProvider());
})
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Example.Service.Handler.Commands.Program.CreateProgramCommand).Assembly);
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Example.Service.Models.Mappings.Profiles));

// Add custom services

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Example.Domain.Data.AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();