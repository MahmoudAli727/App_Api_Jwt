using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(op =>
      op.UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthorization(
    options =>
    {
        options.AddPolicy("AgeGreaterThan25",builder=>builder.AddRequirements(new AgeGreaterThan25Requirment())
        //    builder=>
        //{
        //    builder.RequireAssertion(context =>
        //    {
        //        var dob = DateTime.Parse(context.User.FindFirstValue("DateBirth"));
        //        return DateTime.Today.Year - dob.Year >= 25;
        //        //return context.User.IsInRole("SuperUser");
        //    });
        //    //builder.RequireRole("Admin", "SuperUser");//--> or
        //}
        );

        // add multiple policy 
        options.AddPolicy("EmployeeOnly",builder=>
        {
            builder.RequireClaim("userType","Employee","user");
        });
    });

builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorizationHandler>();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IStudentSevice, StudentService>();


builder.Services.AddControllers(option =>
{
    //option.Filters.Add<LogActivityFilter>();
    ////option.Filters.Add<PermissionBaseAuthorizationFilter>();
    //option.Filters.Add<LogSensitiveActionAttribute>();
}).AddFluentValidation(validate => validate.RegisterValidatorsFromAssemblyContaining<Program>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomJwtAuth(builder.Configuration);
builder.Services.AddSwaggerGenJwtAuth();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<RateLimitingMiddleware>();
//app.UseMiddleware<ProfilingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
