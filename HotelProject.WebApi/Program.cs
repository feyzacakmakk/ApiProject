using HotelProject.BusinessLayer.Abstract;
using HotelProject.BusinessLayer.Concrete;
using HotelProject.DataAccessLayer.Abstract;
using HotelProject.DataAccessLayer.Conccrete;
using HotelProject.DataAccessLayer.EntityFramework;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(opt =>
{
	//burada tan�mlad���m�z key i a�a��da usercors i�inde kullan�caz.
	opt.AddPolicy("OtelApiCors", opts =>
	{
		opts.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});


builder.Services.AddDbContext<Context>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IRoomService,RoomManager>();
builder.Services.AddScoped<IRoomDal,EfRoomDal>();

builder.Services.AddScoped<ISubscribeService, SubscribeManager>();
builder.Services.AddScoped<ISubscribeDal, EfSubscribeDal>();

builder.Services.AddScoped<ITestimonialService, TestimonialManager>();
builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();

builder.Services.AddScoped<IStaffService, StaffManager>();
builder.Services.AddScoped<IStaffDal, EfStaffDal>();

builder.Services.AddScoped<IServiceService, ServiceManager>();
builder.Services.AddScoped<IServiceDal, EfServiceDal>();

//builder.Services.AddScoped<>


// Add services to the container.

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

app.UseCors("OtelApiCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
