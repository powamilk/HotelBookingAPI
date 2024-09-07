using FluentValidation;
using FluentValidation.AspNetCore;
using HotelBookingAPI.Entities;
using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using HotelBookingAPI.ViewModel.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IKhachSanService, KhachSanService>();
builder.Services.AddTransient<IDatPhongService, DatPhongService>();
builder.Services.AddTransient<IKhachHangService, KhachHangService>();

builder.Services.AddTransient<IValidator<CreateKhachSanVM>, CreateKhachSanValidator>();
builder.Services.AddTransient<IValidator<CreatePhongVM>, CreatePhongValidator>();
builder.Services.AddTransient<IValidator<CreateKhachHangVM>, CreateKhachHangValidator>();
builder.Services.AddTransient<IValidator<CreateDatPhongVM>, CreateDatPhongValidator>();
builder.Services.AddTransient<IValidator<KhachSanUpdateVM>, UpdateKhachSanValidator>();
builder.Services.AddTransient<IValidator<UpdateDatPhongStatusVM>, UpdateDatPhongValidator>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<DatPhong, DatPhongVM>();
    cfg.CreateMap<KhachHang, KhachHangVM>();
    cfg.CreateMap<KhachSan, KhachSanVM>();
    cfg.CreateMap<Phong, PhongVM>();
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
