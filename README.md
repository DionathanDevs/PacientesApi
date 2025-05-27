# 📚 PacientesAPI - Sistema de Gerenciamento de Pacientes

API RESTful para gestão de registros médicos desenvolvida em .NET 9.0.

## Sumário
1. [Sobre o Projeto](#sobre-o-projeto)
2. [Pré-requisitos](#pré-requisitos)
3. [Configuração Inicial](#configuração-inicial)

## Sobre o Projeto
O objetivo deste projeto é desenvolver uma API voltada à gestão de registros de pacientes, utilizando tecnologias atuais de desenvolvimento web. A aplicação foi concebida para possibilitar operações como consulta, cadastro, atualização e exclusão de dados de pacientes de forma estruturada e eficiente. Este modelo é aplicável em contextos de saúde, onde é fundamental garantir agilidade na localização, registro e manutenção de informações por parte dos usuários responsáveis pelo controle dos prontuários.

## Pré-requisitos
Para rodar este projeto, você precisará ter instalado:
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download) (ou versão compatível mais recente)
- Gerenciador de pacotes NuGet

## Configuração Inicial

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/DionathanDevs/PacientesApi
    cd PacientesAPI
    ```

2.  **Restaure os pacotes NuGet necessários:**
    ```bash
    dotnet restore
    ```


3.  **Crie e aplique as migrações do Entity Framework Core:**
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```
    Isso criará o banco de dados SQLite e o populará com 10 registros iniciais de pacientes[cite: 1], conforme configurado em `Program.cs`.

## Executando a Aplicação

Para iniciar a API, navegue até o diretório raiz do projeto e execute:
```bash
dotnet run
