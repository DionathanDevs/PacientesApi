        const API = "http://localhost:5022/pacientes";

        async function carregarPacientes() {
            const resposta = await fetch(API);
            const pacientes = await resposta.json();
            mostrarPacientes(pacientes);
        }

        function mostrarPacientes(pacientes) {
    const tabela = document.getElementById("tabelaPacientes");
    tabela.innerHTML = "";
    pacientes.forEach(p => {
        const linha = document.createElement("tr");
        linha.innerHTML = `
            <td>${p.id}</td>
            <td>${p.nome}</td>
            <td>${p.cpf}</td>
            <td>${p.telefone}</td>
            <td>${new Date(p.dataNascimento).toLocaleDateString()}</td>
            <td>${p.endereco}</td>
            <td>${p.tipoSanguineo}</td>
            <td>${p.alergias ?? ""}</td>
            <td>
                <button onclick="editarPaciente(${p.id})">✏️</button>
                <button onclick="excluirPaciente(${p.id})">🗑️</button>
            </td>
        `;
        tabela.appendChild(linha);
    });
}

        async function adicionarOuAtualizarPaciente() {
            const id = document.getElementById("cad-id").value;
            const paciente = {
                nome: document.getElementById("nome").value,
                cpf: document.getElementById("cpf").value,
                telefone: document.getElementById("telefone").value,
                dataNascimento: document.getElementById("dataNascimento").value,
                email: document.getElementById("email").value,
                endereco: document.getElementById("endereco").value,
                tipoSanguineo: document.getElementById("tipoSanguineo").value,
                alergias: document.getElementById("alergias").value
            };

            const url = id ? `${API}/${id}` : API;
            const metodo = id ? "PUT" : "POST";

            await fetch(url, {
                method: metodo,
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(paciente)
            });

            limparFormulario();
            carregarPacientes();
        }

        function editarPaciente(id) {
            fetch(`${API}/${id}`)
                .then(res => res.json())
                .then(p => {
                    document.getElementById("cad-id").value = p.id;
                    document.getElementById("nome").value = p.nome;
                    document.getElementById("cpf").value = p.cpf;
                    document.getElementById("telefone").value = p.telefone;
                    document.getElementById("dataNascimento").value = p.dataNascimento.substring(0, 10);
                    document.getElementById("email").value = p.email;
                    document.getElementById("endereco").value = p.endereco;
                    document.getElementById("tipoSanguineo").value = p.tipoSanguineo;
                    document.getElementById("alergias").value = p.alergias;
                    window.scrollTo({ top: 0, behavior: "smooth" });
                });
        }

        async function excluirPaciente(id) {
            if (confirm("Tem certeza que deseja excluir este paciente?")) {
                await fetch(`${API}/${id}`, { method: "DELETE" });
                carregarPacientes();
            }
        }

        function limparFormulario() {
            document.getElementById("cad-id").value = "";
            document.getElementById("nome").value = "";
            document.getElementById("cpf").value = "";
            document.getElementById("telefone").value = "";
            document.getElementById("dataNascimento").value = "";
            document.getElementById("email").value = "";
            document.getElementById("endereco").value = "";
            document.getElementById("tipoSanguineo").value = "";
            document.getElementById("alergias").value = "";
        }

        async function filtrarPacientes() {
            const id = document.getElementById("filtroId").value;
            const nome = document.getElementById("filtroNome").value.toLowerCase();
            const cpf = document.getElementById("filtroCpf").value;
            const telefone = document.getElementById("filtroTelefone").value;

            const resposta = await fetch(API);
            const pacientes = await resposta.json();

            const filtrados = pacientes.filter(p =>
                (id === "" || p.id == id) &&
                (nome === "" || p.nome.toLowerCase().includes(nome)) &&
                (cpf === "" || p.cpf.includes(cpf)) &&
                (telefone === "" || p.telefone.includes(telefone))
            );

            mostrarPacientes(filtrados);
        }

        carregarPacientes();
