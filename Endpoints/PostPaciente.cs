using Microsoft.AspNetCore.Mvc;
using PacientesApi.Data;
using PacientesApi.Models;


namespace PacientesApi.Endpoints;

public static class PostPaciente
{
    public static void MapCreatePaciente(this WebApplication app)
    {
        app.MapPost("/pacientes", async ([FromBody] Paciente paciente, [FromServices] AppDbContext context) =>
        {
            context.Pacientes.Add(paciente);
            await context.SaveChangesAsync();
            return Results.Created($"/pacientes/{paciente.Id}", paciente);
        })
        .WithName("PostPaciente")
        .Produces<Paciente>(StatusCodes.Status201Created);
    }
}