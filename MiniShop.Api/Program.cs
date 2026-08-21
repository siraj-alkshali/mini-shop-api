using MiniShop.Infrastructure.DependencyInjection;
using MiniShop.Application.DependencyInjection;
using FluentValidation;
using MiniShop.Application.DTOs.Validations;
using FluentValidation.AspNetCore;
using MiniShop.Application.Discounts;
using MiniShop.Application.Shipping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<DiscountOptions>(builder.Configuration.GetSection("Discounts"));

builder.Services.Configure<ShippingOptions>(builder.Configuration.GetSection("Shipping"));

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

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
