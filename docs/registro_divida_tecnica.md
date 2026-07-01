# Registro de Dívida Técnica

Esta tabela mapeia as principais dívidas técnicas acumuladas até o momento no desenvolvimento da Minimal API, categorizando o nível de risco e priorizando sua resolução.

| ID da Dívida | Descrição Técnica | Freq. Alteração | Risco | Esforço | Decisão |
|---|---|---|---|---|---|
| DT-001 | Ausência de Autenticação e Autorização nos endpoints (JWT) | Baixo | Alto | Alto | Prioridade 1 (Imediato) |
| DT-002 | Falta de tratamento centralizado de Exceções (Global Exception Handler) | Baixo | Médio | Baixo | Prioridade 1 (Imediato) |
| DT-003 | Banco de dados sendo versionado via arquivo manual `script.sql` (Sem Migrations) | Alto | Alto | Médio | Prioridade 2 (Próxima Sprint) |
| DT-004 | Inexistência de Testes de Integração para testar as rotas da API ponta-a-ponta | Médio | Médio | Alto | Prioridade 2 (Próxima Sprint) |
| DT-005 | As rotas da Minimal API estão todas acopladas no `Program.cs` ou classes não segregadas por feature | Alto | Baixo | Médio | Prioridade 3 (Aceitar/Ignorar) |
| DT-006 | Ausência de logs estruturados (ex: Serilog) para rastreabilidade de requisições | Baixo | Baixo | Médio | Prioridade 3 (Aceitar/Ignorar) |
