# Plano de Iteração (Sprint Atual)

**Objetivo da Iteração:**
Consolidar a estrutura inicial da Minimal API do TicketPrime, garantindo que os fluxos de criação de usuários (com bloqueio de CPF duplicado), criação de eventos e cupons estejam funcionais, persistidos no PostgreSQL e totalmente validados por testes unitários seguindo o padrão AAA.

**Escopo (Backlog Selecionado):**
- Implementar rota de criação de usuários com a regra de negócio de limite de 1 por CPF.
- Implementar rota de cadastro de eventos exigindo nome preenchido.
- Implementar rota de cupons de desconto.
- Setup do projeto com Dapper e testes com xUnit.

**Entregáveis (Evidências):**
- Código-fonte da API funcional (`/src`).
- Script de banco de dados atualizado (`/db/script.sql`).
- Suíte de testes rodando sem falhas (`/tests`).
- Documentação de arquitetura e operação finalizada e versionada (`/docs`).

**Risco Principal do Ciclo:**
Curva de aprendizado da equipe no uso do Dapper para escrever SQL cru (sem Entity Framework). Isso pode gerar atrasos caso alguém trave na escrita de consultas mais complexas que exijam `JOIN`.

**Definição de Pronto (DoD):**
- O código deve compilar perfeitamente e rodar localmente sem `warnings` impeditivos.
- 100% dos testes unitários criados devem passar (`dotnet test`).
- O código não pode ter vazamento de dados sensíveis ou senhas hardcoded.
- O revisor de código deve ter aprovado o Pull Request.

---

## Quadro Visual e Limite de Trabalho em Progresso

A equipe é composta por 4 desenvolvedores (Vinícius, Arthur, Rodrigo e David). Para evitar gargalos e garantir um fluxo ágil e rápido (foco em terminar, não apenas em começar), utilizaremos um quadro Kanban com as seguintes colunas:

1. **Backlog**: Fila de tickets pendentes priorizados pelo Product Owner.
2. **Em Desenvolvimento**: Tickets que um desenvolvedor puxou e está codando no momento.
3. **Code Review**: Tickets finalizados, aguardando aprovação/testes de outro membro do grupo.
4. **Concluído**: Funcionalidade aprovada e integrada na branch `main`.

### Limite de WIP
Como somos 4 integrantes, queremos estimular o pareamento (Pair Programming) e a colaboração. Logo, a nossa política de restrição será:

**WIP máximo: 3 tarefas**

*Justificativa:* Limitando a no máximo 3 tarefas ocorrendo simultaneamente entre as colunas ativas ("Em Desenvolvimento" e "Code Review"), pelo menos 1 membro da equipe ficará "livre". Essa pessoa terá que obrigatoriamente ajudar um colega a terminar uma tarefa em andamento antes de puxar um ticket novo do Backlog.
