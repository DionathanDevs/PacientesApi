using Microsoft.EntityFrameworkCore;
using PacientesApi.Data;
using PacientesApi.Endpoints;
using PacientesApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(); 

// Configuração do Swagger (mantenha se estiver usando)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuração do DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    app.UseDeveloperExceptionPage();
    
}

app.UseHttpsRedirection();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    

    app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}





app.UseStaticFiles(); // Habilita o middleware para servir arquivos estáticos
app.UseDefaultFiles();

// Mapear endpoints
app.MapGetPacientes();
app.MapGetPacienteById(); 
app.MapCreatePaciente();
app.MapUpdatePaciente();
app.MapDeletePaciente();
app.MapFallbackToFile("index.html");

// Configuração inicial do banco de dados
await InitializeDatabase(app);

app.Run();
async Task InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // 1. Verifica e aplica migrações pendentes
    if ((await db.Database.GetPendingMigrationsAsync()).Any())
    {
        await db.Database.MigrateAsync();
    }

    // 2. Verifica se a tabela está vazia e insere os pacientes
    if (!await db.Pacientes.AnyAsync())
    {
        var pacientes = new List<Paciente>
        {
              new()
    {
        Nome = "João Silva",
        CPF = "11122233344",
        DataNascimento = new DateTime(1985, 3, 15),
        Telefone = "(11) 99999-1111",
        Email = "joao.silva@example.com",
        Endereco = "Rua das Flores, 123, São Paulo - SP",
        TipoSanguineo = "O+",
        Alergias = "Nenhuma"
    },
    new()
    {
        Nome = "Maria Oliveira",
        CPF = "22233344455",
        DataNascimento = new DateTime(1990, 7, 22),
        Telefone = "(21) 98888-2222",
        Email = "maria.oliveira@example.com",
        Endereco = "Av. Brasil, 456, Rio de Janeiro - RJ",
        TipoSanguineo = "A+",
        Alergias = "Penicilina"
    },

    new()
    {
        Nome = "Carlos Pereira",
        CPF = "33344455566",
        DataNascimento = new DateTime(1978, 5, 10),
        Telefone = "(31) 97777-3333",
        Email = "carlos.pereira@example.com",
        Endereco = "Av. Afonso Pena, 789, Belo Horizonte - MG",
        TipoSanguineo = "B-",
        Alergias = "Dipirona"
    },
    new()
    {
        Nome = "Ana Santos",
        CPF = "44455566677",
        DataNascimento = new DateTime(1995, 11, 25),
        Telefone = "(41) 96666-4444",
        Email = "ana.santos@example.com",
        Endereco = "Rua XV de Novembro, 101, Curitiba - PR",
        TipoSanguineo = "AB+",
        Alergias = "Frutos do mar"
    },
    new()
    {
        Nome = "Pedro Costa",
        CPF = "55566677788",
        DataNascimento = new DateTime(1982, 9, 3),
        Telefone = "(51) 95555-5555",
        Email = "pedro.costa@example.com",
        Endereco = "Av. Borges de Medeiros, 202, Porto Alegre - RS",
        TipoSanguineo = "O-",
        Alergias = "Pólen"
    },
    new()
    {
        Nome = "Juliana Almeida",
        CPF = "66677788899",
        DataNascimento = new DateTime(1988, 7, 30),
        Telefone = "(61) 94444-6666",
        Email = "juliana.almeida@example.com",
        Endereco = "SQN 302, Bloco A, Brasília - DF",
        TipoSanguineo = "A-",
        Alergias = "Lactose"
    },
    new()
    {
        Nome = "Marcos Souza",
        CPF = "77788899900",
        DataNascimento = new DateTime(1975, 4, 18),
        Telefone = "(71) 93333-7777",
        Email = "marcos.souza@example.com",
        Endereco = "Av. Oceânica, 303, Salvador - BA",
        TipoSanguineo = "B+",
        Alergias = "Iodo"
    },
    new()
    {
        Nome = "Fernanda Lima",
        CPF = "88899900011",
        DataNascimento = new DateTime(1992, 12, 8),
        Telefone = "(81) 92222-8888",
        Email = "fernanda.lima@example.com",
        Endereco = "Rua do Sol, 404, Recife - PE",
        TipoSanguineo = "AB-",
        Alergias = "Ovos"
    },
    new()
    {
        Nome = "Ricardo Martins",
        CPF = "99900011122",
        DataNascimento = new DateTime(1980, 8, 22),
        Telefone = "(85) 91111-9999",
        Email = "ricardo.martins@example.com",
        Endereco = "Av. Beira Mar, 505, Fortaleza - CE",
        TipoSanguineo = "A+",
        Alergias = "Nenhuma"
    },
    new()
    {
        Nome = "Patrícia Rocha",
        CPF = "00011122233",
        DataNascimento = new DateTime(1998, 1, 14),
        Telefone = "(91) 90000-0000",
        Email = "patricia.rocha@example.com",
        Endereco = "Travessa Quintino Bocaiúva, 606, Belém - PA",
        TipoSanguineo = "O+",
        Alergias = "Picada de inseto"
    }
           
        };

        await db.Pacientes.AddRangeAsync(pacientes);
        await db.SaveChangesAsync();
        
        Console.WriteLine($"Inseridos {pacientes.Count} pacientes iniciais");
    }
}
