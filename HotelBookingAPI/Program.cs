using FluentValidation;
using FluentValidation.AspNetCore;
using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using HotelBookingAPI.ViewModel.NewFolder;
using HotelBookingAPI.ViewModel.NewFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IKhachSanService, KhachSanService>();
builder.Services.AddTransient<IDatPhongService, DatPhongService>();
builder.Services.AddTransient<IKhachHangService, KhachHangService>();


builder.Services.AddTransient<IValidator<CreateKhachSanVM>, CreateKhachSanValidation>();
builder.Services.AddTransient<IValidator<CreatePhongVM>, CreatePhongValidation>();
builder.Services.AddTransient<IValidator<CreateKhachHangVM>, CreateKhachHangValidation>();
builder.Services.AddTransient<IValidator<CreateDatPhongVM>, CreateDatPhongValidation>();
builder.Services.AddTransient<IValidator<KhachSanUpdateVM>, UpdateKhachSanValidator>();

// Add FluentValidation to the pipeline
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

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
