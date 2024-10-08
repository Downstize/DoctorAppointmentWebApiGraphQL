using DoctorAppointmentWebApi;
using DoctorAppointmentWebApi.Mutations;
using DoctorAppointmentWebApi.Queries;
using Microsoft.EntityFrameworkCore;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AppointmentQuery>();
builder.Services.AddScoped<DepartmentQuery>();
builder.Services.AddScoped<DoctorQuery>();
builder.Services.AddScoped<DoctorScheduleQuery>();
builder.Services.AddScoped<PatientQuery>();
builder.Services.AddScoped<SpecializationQuery>();
builder.Services.AddScoped<AppointmentMutation>();
builder.Services.AddScoped<DepartmentMutation>();
builder.Services.AddScoped<DoctorMutation>();
builder.Services.AddScoped<DoctorScheduleMutation>();
builder.Services.AddScoped<PatientMutation>();
builder.Services.AddScoped<SpecializationMutation>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<AppointmentQuery>()
    .AddTypeExtension<DepartmentQuery>()
    .AddTypeExtension<DoctorQuery>()
    .AddTypeExtension<DoctorScheduleQuery>()
    .AddTypeExtension<PatientQuery>()
    .AddTypeExtension<SpecializationQuery>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<AppointmentMutation>()
    .AddTypeExtension<DepartmentMutation>()
    .AddTypeExtension<DoctorMutation>()
    .AddTypeExtension<DoctorScheduleMutation>()
    .AddTypeExtension<PatientMutation>()
    .AddTypeExtension<SpecializationMutation>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL("/graphql");

app.Run();