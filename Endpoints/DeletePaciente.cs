using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacientesApi.Data;
using PacientesApi.Models;

namespace PacientesApi.Endpoints;

public static class DeletePaciente
{
    public static void MapDeletePaciente(this WebApplication app)
    {
        app.MapDelete("/pacientes/{id}", async ([FromRoute] int id, [FromServices] AppDbContext context) =>
        {
            var paciente = await context.Pacientes.FindAsync(id);
            if (paciente is null) return Results.NotFound();

            context.Pacientes.Remove(paciente);
            await context.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("DeletePaciente")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}