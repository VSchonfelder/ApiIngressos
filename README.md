# 🎟 TicketPrime API

API backend desenvolvida para gerenciamento de eventos, usuários e cupons de desconto.
Projeto acadêmico da disciplina de Engenharia de Software, com foco em segurança, organização e boas práticas.

---

## 🚀 Tecnologias Utilizadas

* C# (.NET Minimal API)
* Dapper
* PostgreSQL
* xUnit (Testes automatizados)

---

## 📂 Estrutura do Projeto

```
/docs     -> Documentação (requisitos e BDD)
/db       -> Script SQL do banco de dados
/src      -> Código-fonte da API
/tests    -> Testes automatizados (xUnit)
```

---

## ⚙️ Como Executar o Projeto

### 1. Clonar o repositório

```
git clone https://github.com/VSchonfelder/ApiIngressos.git
cd ApiIngressos
```

---

### 2. Restaurar dependências

```
dotnet restore
```

---

### 3. Compilar o projeto

```
dotnet build
```

---

### 4. Configurar conexão com o banco

Edite o arquivo:

```
src/ApiIngressos/appsettings.json
```

E configure a connection string:

```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=IngressosDB;Username=postgres;Password=SUA_SENHA"
}
```

---

### 5. Criar o banco de dados

1. Crie um banco no PostgreSQL (ex: `IngressosDB`)
2. Execute o script:

```
/db/script.sql
```

---

### 6. Executar a API

```
cd src/ApiIngressos
dotnet run
```

---

## 📡 Endpoints da API

### 🔹 Eventos

* `POST /api/eventos` → Cadastra um novo evento
* `GET /api/eventos` → Lista todos os eventos

---

### 🔹 Usuários

* `POST /api/usuarios` → Cadastra um usuário

  * Retorna **400 BadRequest** se o CPF já estiver cadastrado

---

### 🔹 Cupons

* `POST /api/cupons` → Cadastra um cupom de desconto

---

## 🛡 Segurança

* Uso de Dapper com parâmetros (`@`)
* Proteção contra SQL Injection
* Validação de regras de negócio no backend

---

## 🧪 Testes Automatizados

Para executar os testes:

```
dotnet test
```

* Todos os testes utilizam `Assert`
* Projeto estruturado com xUnit

---

## 📌 Observações

* O projeto segue rigorosamente os nomes de rotas, tabelas e colunas definidos no enunciado
* A estrutura de pastas é obrigatória e sensível a maiúsculas/minúsculas
* Não utiliza Entity Framework, conforme exigido

---

## 👥 Equipe

* Vinícius Schonfelder (06010595) — Banco de Dados e Infraestrutura
* Arthur Fita Santana (06008892) — Endpoints de Eventos
* Rodrigo da Costa Cernigoi (06012368) — Usuários e Cupons + Regras de Negócio + xUnit
* David Almeida Ferreira (06012723) — Segurança e Revisão + FrontEnd

