


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();

//builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();

var connectionString = builder.Configuration.GetConnectionString("ShopProjectAPIContext");
builder.Services.AddDbContext<ProjectShopDbContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
