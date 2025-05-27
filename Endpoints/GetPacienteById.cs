using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacientesApi.Data;
using PacientesApi.Models;

namespace PacientesApi.Endpoints;

public static class GetPacienteById
{
    public static void MapGetPacienteById(this WebApplication app)
    {
        app.MapGet("/pacientes/{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) =>
        {
            var paciente = await context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
            return paciente is not null ? Results.Ok(paciente) : Results.NotFound();
        })
        .WithName("GetPacienteById")
        .Produces<Paciente>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
