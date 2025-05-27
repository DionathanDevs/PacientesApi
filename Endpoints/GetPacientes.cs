using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacientesApi.Data;
using PacientesApi.Models; 


namespace PacientesApi.Endpoints;

public static class GetPacientes
{
    public static void MapGetPacientes(this WebApplication app)
    {
        app.MapGet("/pacientes", async ([FromServices] AppDbContext context) =>
        {
            var pacientes = await context.Pacientes.ToListAsync();
            return pacientes;
        })
        .WithName("GetPacientes")
        .Produces<List<Paciente>>(StatusCodes.Status200OK);
    }
}
