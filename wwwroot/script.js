const API = "http://localhost:5022/pacientes";

async function carregarPacientes() {
  const resposta = await fetch(API);
  const pacientes = await resposta.json();
  mostrarPacientes(pacientes);
}

function mostrarPacientes(pacientes) {
  const tabela = document.getElementById("tabelaPacientes");
  tabela.innerHTML = "";
  pacientes.forEach((p) => {
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
                <button onclick="editarPaciente(${
                  p.id
                })"><i class="fa-solid fa-pen-to-square"></i></button>
                <button onclick="excluirPaciente(${
                  p.id
                })"><i class="fa-solid fa-trash"></i></button>
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
    alergias: document.getElementById("alergias").value,
  };

  const url = id ? `${API}/${id}` : API;
  const metodo = id ? "PUT" : "POST";

  await fetch(url, {
    method: metodo,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(paciente),
  });

  limparFormulario();
  carregarPacientes();

  setTimeout(() => {
    const secaoTabela = document.getElementById("secaoTabela");
    secaoTabela.scrollIntoView({
      behavior: "smooth",
      block: "start",
    });
  }, 100);
}

function editarPaciente(id) {
  fetch(`${API}/${id}`)
    .then((res) => res.json())
    .then((p) => {
      document.getElementById("cad-id").value = p.id;
      document.getElementById("nome").value = p.nome;
      document.getElementById("cpf").value = p.cpf;
      document.getElementById("telefone").value = p.telefone;
      document.getElementById("dataNascimento").value =
        p.dataNascimento.substring(0, 10);
      document.getElementById("email").value = p.email;
      document.getElementById("endereco").value = p.endereco;
      document.getElementById("tipoSanguineo").value = p.tipoSanguineo;
      document.getElementById("alergias").value = p.alergias;
      const telaCadastro = document.getElementById("formCadastro");
      telaCadastro.scrollIntoView({ behavior: "smooth", block: "start" });
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

  const filtrados = pacientes.filter(
    (p) =>
      (id === "" || p.id == id) &&
      (nome === "" || p.nome.toLowerCase().includes(nome)) &&
      (cpf === "" || p.cpf.includes(cpf)) &&
      (telefone === "" || p.telefone.includes(telefone))
  );

  mostrarPacientes(filtrados);

  setTimeout(() => {
    const secaoTabela = document.getElementById("secaoTabela");
    secaoTabela.scrollIntoView({
      behavior: "smooth",
      block: "start",
    });
  }, 100);
}

carregarPacientes();

 async function carregarRelatorio() {
      try {
        const response = await fetch('http://localhost:5022/relatorio');
        const data = await response.json();

      
        document.getElementById('totalPacientes').textContent = data.totalPacientes;
        
        



        const ctxTipo = document.getElementById('tipoSanguineoChart').getContext('2d');
        new Chart(ctxTipo, {
          type: 'pie',
          data: {
            labels: tipos,
            datasets: [{
              label: 'Quantidade',
              data: quantidades,
              backgroundColor: [
                '#4e79a7', '#f28e2b', '#e15759',
                '#76b7b2', '#59a14f', '#edc948',
                '#b07aa1', '#ff9da7', '#9c755f', '#bab0ab'
              ],
            }]
          },
          options: {
            responsive: true,
            plugins: {
              legend: {
                position: 'bottom',
              }
            }
          }
        });

        // Gráfico Idades
        const nomes = data.idades.map(x => x.nome);
        const idades = data.idades.map(x => x.idade);

        const ctxIdades = document.getElementById('idadesChart').getContext('2d');
        new Chart(ctxIdades, {
          type: 'bar',
          data: {
            labels: nomes,
            datasets: [{
              label: 'Idade',
              data: idades,
              backgroundColor: '#4e79a7'
            }]
          },
          options: {
            responsive: true,
            scales: {
              y: {
                beginAtZero: true,
                ticks: { stepSize: 5 }
              }
            },
            plugins: {
              legend: {
                display: false
              }
            }
          }
        });

      } catch (error) {
        console.error('Erro ao carregar relatório:', error);
      }
    }

    carregarRelatorio();
