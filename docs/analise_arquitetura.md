# Análise de Arquitetura

## 1. Padrões Arquiteturais Prováveis

Abaixo estão 3 cenários fictícios aplicados ao domínio de venda de ingressos (TicketPrime), com a identificação do padrão arquitetural correspondente e a análise de seus trade-offs.

### Cenário 1
**Descrição:** O sistema precisa processar uma altíssima carga de acessos simultâneos durante a abertura de vendas de ingressos para grandes shows. Para suportar a demanda, a equipe técnica decidiu separar o sistema em múltiplos serviços pequenos e independentes (como "Serviço de Usuários", "Serviço de Cupons" e "Serviço de Pagamentos"), que podem ser escalados separadamente conforme a necessidade.
**Padrão Arquitetural Provável:** Microserviços (Microservices)

**Positivo:** Permite escalabilidade granular e independente. Se houver pico na compra de ingressos, apenas o serviço de vendas é escalado, preservando os recursos do resto do sistema.
**Negativo:** Aumenta drasticamente a complexidade operacional da infraestrutura (deployments, redes e monitoramento) e torna o rastreamento de requisições e falhas bem mais difícil.

---

### Cenário 2
**Descrição:** A aplicação inicial (MVP) foi desenhada dividindo o código em seções horizontais muito bem definidas: uma parte apenas recebe requisições web (API), outra executa estritamente as regras de negócio, e uma última é responsável apenas por salvar e buscar dados no PostgreSQL. Cada nível só pode acessar o nível diretamente abaixo dele.
**Padrão Arquitetural Provável:** Arquitetura em Camadas (Layered Architecture)

**Positivo:** Reduz a curva de aprendizado para novos desenvolvedores por ser um padrão muito comum e intuitivo, além de organizar muito bem as responsabilidades de código.
**Negativo:** Pode gerar lentidão no desenvolvimento e "boilerplate code", já que adicionar uma simples coluna no banco de dados exige alterar o fluxo passando obrigatoriamente por todas as camadas.

---

### Cenário 3
**Descrição:** Como a validação de pagamentos com as operadoras de cartão é demorada, o sistema de vendas não espera a resposta. Em vez disso, assim que o usuário clica em "Comprar", uma mensagem é colocada em um barramento (RabbitMQ). O usuário recebe uma resposta imediata de "Processando", e os serviços internos reagem à mensagem no barramento de forma assíncrona.
**Padrão Arquitetural Provável:** Arquitetura Orientada a Eventos (Event-Driven Architecture)

**Positivo:** Alta disponibilidade e excelente experiência do usuário, pois o frontend não fica travado esperando o processamento longo do backend.
**Negativo:** Traz o desafio da consistência eventual (os dados demoram um pouco para se refletirem no banco de leitura) e torna muito complexo o tratamento de falhas em cascata caso o evento de pagamento falhe silenciosamente.

---

## 2. Análise de Violações Arquiteturais

Abaixo apresento a análise de um trecho de código problemático hipotético de uma Controller de Vendas, extraindo 5 violações arquiteturais graves.

### Violação 1
**Problema:** Conexão com banco de dados fortemente acoplada e credenciais hardcoded.
**Evidência:** O código cria a conexão diretamente usando `new NpgsqlConnection("Host=localhost;Password=senha123")` dentro da rota.
**Impacto:** Impede a troca de ambiente (Dev/Prod), viola as regras de segurança (SSDF) e dificulta muito a testabilidade (impossível mockar o banco).
**Ação Recomendada:** Utilizar Injeção de Dependência (DI) para receber a configuração de conexão, buscando a string do `appsettings.json` ou de variáveis de ambiente.

### Violação 2
**Problema:** Regras de negócio vazadas na camada de apresentação (API).
**Evidência:** A verificação de estoque e a lógica de limite de ingressos por CPF estão codificadas diretamente dentro do endpoint da API.
**Impacto:** Dificulta o reaproveitamento das regras de venda, fere o princípio de Responsabilidade Única (SRP) e cria endpoints "gordos" (Fat Controllers).
**Ação Recomendada:** Extrair a lógica de verificação para uma camada de Domínio ou um Application Service (ex: `VendaService`).

### Violação 3
**Problema:** Risco crítico de Segurança (SQL Injection).
**Evidência:** O código monta a query SQL concatenando strings: `var sql = "SELECT * FROM Usuarios WHERE Cpf = '" + cpf + "'";`
**Impacto:** Permite que um atacante injete comandos maliciosos, podendo deletar ou vazar toda a base de dados de ingressos.
**Ação Recomendada:** Utilizar Queries Parametrizadas exclusivamente (ex: `@Cpf` com Dapper).

### Violação 4
**Problema:** Tratamento de Exceções Inadequado (Vazamento de detalhes internos).
**Evidência:** Existe um bloco `catch (Exception ex)` que retorna `return Results.BadRequest(ex.Message);` direto pro usuário.
**Impacto:** Vaza detalhes da infraestrutura ou do banco de dados para o cliente da API, o que expõe a arquitetura interna e facilita ataques.
**Ação Recomendada:** Capturar exceções e retornar mensagens genéricas para o cliente (ex: "Erro interno no servidor"), logando o erro detalhado internamente (ex: Serilog).

### Violação 5
**Problema:** Violação da Inversão de Dependência (DIP).
**Evidência:** O código instancia um serviço de envio de email de forma direta com `var emailService = new SmtpEmailService();`.
**Impacto:** Acoplamento forte com a implementação concreta de envio de email. Se o provedor de email mudar, a classe da API precisa ser alterada.
**Ação Recomendada:** Criar uma interface `IEmailService`, injetá-la no construtor do endpoint e registrar a implementação no container de injeção de dependência.
