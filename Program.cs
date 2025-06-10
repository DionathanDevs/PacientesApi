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


app.UseStaticFiles(); 
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
    },new()
{
    Nome = "Luana Ribeiro",
    CPF = "12345678901",
    DataNascimento = new DateTime(1987, 6, 12),
    Telefone = "(62) 98888-1111",
    Email = "luana.ribeiro@example.com",
    Endereco = "Rua T-63, Goiânia - GO",
    TipoSanguineo = "B+",
    Alergias = "Glúten"
},
new()
{
    Nome = "Eduardo Nascimento",
    CPF = "23456789012",
    DataNascimento = new DateTime(1979, 2, 28),
    Telefone = "(95) 97777-2222",
    Email = "eduardo.nascimento@example.com",
    Endereco = "Rua das Palmeiras, Boa Vista - RR",
    TipoSanguineo = "O-",
    Alergias = "Nenhuma"
},
new()
{
    Nome = "Camila Farias",
    CPF = "34567890123",
    DataNascimento = new DateTime(1993, 3, 19),
    Telefone = "(47) 96666-3333",
    Email = "camila.farias@example.com",
    Endereco = "Av. Atlântica, 707, Balneário Camboriú - SC",
    TipoSanguineo = "AB+",
    Alergias = "Latex"
},
new()
{
    Nome = "Thiago Melo",
    CPF = "45678901234",
    DataNascimento = new DateTime(1986, 10, 5),
    Telefone = "(82) 95555-4444",
    Email = "thiago.melo@example.com",
    Endereco = "Rua do Farol, 200, Maceió - AL",
    TipoSanguineo = "A-",
    Alergias = "Frutos do mar"
},
new()
{
    Nome = "Larissa Cunha",
    CPF = "56789012345",
    DataNascimento = new DateTime(1991, 8, 17),
    Telefone = "(98) 94444-5555",
    Email = "larissa.cunha@example.com",
    Endereco = "Av. Litorânea, 321, São Luís - MA",
    TipoSanguineo = "B-",
    Alergias = "Dipirona"
},
new()
{
    Nome = "Fábio Teixeira",
    CPF = "67890123456",
    DataNascimento = new DateTime(1972, 1, 9),
    Telefone = "(96) 93333-6666",
    Email = "fabio.teixeira@example.com",
    Endereco = "Rua Cândido Mendes, 800, Macapá - AP",
    TipoSanguineo = "O+",
    Alergias = "Nenhuma"
},
new()
{
    Nome = "Beatriz Moraes",
    CPF = "78901234567",
    DataNascimento = new DateTime(1996, 4, 11),
    Telefone = "(27) 92222-7777",
    Email = "beatriz.moraes@example.com",
    Endereco = "Rua da Lama, 909, Vitória - ES",
    TipoSanguineo = "AB-",
    Alergias = "Poeira"
},
new()
{
    Nome = "Renato Dias",
    CPF = "89012345678",
    DataNascimento = new DateTime(1983, 9, 6),
    Telefone = "(73) 91111-8888",
    Email = "renato.dias@example.com",
    Endereco = "Rua dos Pescadores, 303, Ilhéus - BA",
    TipoSanguineo = "A+",
    Alergias = "Camarão"
},
new()
{
    Nome = "Isabela Martins",
    CPF = "90123456789",
    DataNascimento = new DateTime(1994, 5, 27),
    Telefone = "(53) 90000-9999",
    Email = "isabela.martins@example.com",
    Endereco = "Av. Dom Joaquim, 707, Pelotas - RS",
    TipoSanguineo = "B+",
    Alergias = "Amendoim"
},
new()
{
    Nome = "Rafael Gonçalves",
    CPF = "01234567890",
    DataNascimento = new DateTime(1977, 12, 3),
    Telefone = "(92) 98888-0000",
    Email = "rafael.goncalves@example.com",
    Endereco = "Rua do Comércio, 505, Manaus - AM",
    TipoSanguineo = "O-",
    Alergias = "Nenhuma"
}

           
        };

        await db.Pacientes.AddRangeAsync(pacientes);
        await db.SaveChangesAsync();
        
        Console.WriteLine($"Inseridos {pacientes.Count} pacientes iniciais");
    }
}
