using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacientesApi.Data;
using PacientesApi.Models;

namespace PacientesApi.Endpoints;

public static class RelatorioEndpoint
{
    public static void MapRelatorioEndpoint(this WebApplication app)
    {
        app.MapGet("/relatorio", async ([FromServices] AppDbContext context) =>
        {
            var pacientes = await context.Pacientes.ToListAsync();

            var total = pacientes.Count;

            var porTipoSanguineo = pacientes
                .GroupBy(p => p.TipoSanguineo)
                .Select(g => new { Tipo = g.Key, Quantidade = g.Count() })
                .ToList();

            var idades = pacientes
                .Select(p => new
                {
                    Nome = p.Nome,
                    Idade = DateTime.Today.Year - p.DataNascimento.Year -
                            (DateTime.Today.DayOfYear < p.DataNascimento.DayOfYear ? 1 : 0)
                })
                .ToList();

            return Results.Ok(new
            {
                TotalPacientes = total,
                PorTipoSanguineo = porTipoSanguineo,
                Idades = idades
            });
        })
        .WithName("RelatorioPacientes")
        .Produces(StatusCodes.Status200OK);
    }
}
