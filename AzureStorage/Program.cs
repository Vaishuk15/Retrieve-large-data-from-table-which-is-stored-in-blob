using AzureStorage.Repository;
using AzureStorage.Repository.Implementation;
using AzureStorage.Repository.Interface;
using AzureStorage.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddAutoMapper(typeof(AutoMapping)); ;

builder.Services.AddTransient<IAzureBlobStorageRepository, AzureBlobStorageRepository>();
builder.Services.AddTransient(typeof(IAzureTableStorageRepository<>), typeof(AzureTableStorageRepository<>));


new AzureRepositoryModule(builder.Services);
new ServiceModule(builder.Services);

builder.Services.AddAutoMapper(typeof(AutoMapping));

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
