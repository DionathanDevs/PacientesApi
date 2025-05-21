using System.ComponentModel.DataAnnotations;

namespace PacientesApi.Models;

public class Paciente
{

    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Endereco { get; set; }
    public string? TipoSanguineo { get; set; } 
    public string? Alergias { get; set; }     
}