namespace PacientesApi.Endpoints;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacientesApi.Data;
using PacientesApi.Models; 

public static class PutPaciente
{
    public static void MapUpdatePaciente(this WebApplication app)
    {
        app.MapPut("/pacientes/{id}", async ([FromRoute] int id, [FromBody] Paciente pacienteAtualizado, [FromServices] AppDbContext context) =>
        {
            var paciente = await context.Pacientes.FindAsync(id);
            if (paciente is null) return Results.NotFound();

            paciente.Nome = pacienteAtualizado.Nome;
            paciente.CPF = pacienteAtualizado.CPF;
            paciente.DataNascimento = pacienteAtualizado.DataNascimento;
            paciente.Telefone = pacienteAtualizado.Telefone;
            paciente.Email = pacienteAtualizado.Email;
            paciente.Endereco = pacienteAtualizado.Endereco;
             paciente.TipoSanguineo = pacienteAtualizado.TipoSanguineo;
            paciente.Alergias = pacienteAtualizado.Alergias;

            await context.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("PutPaciente")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
