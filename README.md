# 🎟 TicketPrime API

API backend desenvolvida para gerenciamento de eventos, usuários e cupons de desconto.
Projeto acadêmico da disciplina de Engenharia de Software, com foco em segurança, organização e boas práticas.

---

## 🚀 Tecnologias Utilizadas

* C# (.NET Minimal API)
* Dapper
* Banco de Dados Relacional (SQL)
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
git clone <URL_DO_REPOSITORIO>
cd repo
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

### 4. Executar a API

```
dotnet run --project src/ApiIngressos
```

---

## 🗄️ Banco de Dados

1. Criar um banco de dados relacional (ex: PostgreSQL)
2. Executar o script localizado em:

```
/db/script.sql
```

3. Configurar a connection string no projeto

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

✔ Todos os testes utilizam `Assert`
✔ Projeto estruturado com xUnit

---

## 📌 Observações

* O projeto segue rigorosamente os nomes de rotas, tabelas e colunas definidos no enunciado
* A estrutura de pastas é obrigatória e sensível a maiúsculas/minúsculas
* Não utiliza Entity Framework, conforme exigido

---

## 👥 Equipe

* Vinícius — Banco de Dados e Infraestrutura
* Arthur — Endpoints de Eventos
* Rodrigo — Usuários e Cupons + Regras de Negócio + xUnit
* David — Segurança e Revisão + FrontEnd

---
