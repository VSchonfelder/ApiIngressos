# Manual de Operação e Confiabilidade

## 1. Matriz de Riscos

Abaixo estão listados os 5 principais riscos do projeto TicketPrime, bem como as estratégias estabelecidas pela equipe para tratamento de cada um.

| Risco | Probabilidade | Impacto | Estratégia | Gatilho | Ação Planejada |
|---|---|---|---|---|---|
| Queda do banco de dados na abertura de vendas | Médio | Alto | Mitigar | O monitoramento de CPU do banco de dados ultrapassar 90% de uso. | Escalar a instância de leitura do banco e limitar temporariamente os acessos simultâneos na API. |
| Serviço de e-mail impedindo envio de ingressos | Baixo | Alto | Transferir | Falha reportada pelo provedor de e-mails em sua página de status oficial. | Acionar failover para API de e-mails terceira (como SendGrid) e armazenar as requisições em fila. |
| Dificuldade técnica da equipe atrasando entregas | Médio | Médio | Evitar | Uma tarefa ficar parada em code review ou bloqueada por mais de dois dias seguidos. | Paralisar tarefas novas e promover uma sessão de Pair Programming (Dojo) entre os integrantes do grupo. |
| Vazamento de CPFs (LGPD) por injeção de SQL | Baixo | Alto | Evitar | Alerta da ferramenta de análise estática (SonarQube) sobre concatenação de SQL. | Reprovar imediatamente o Pull Request e exigir parameterização do Dapper em 100% das queries. |
| Fraude na aplicação de múltiplos cupons na mesma compra | Alto | Médio | Aceitar | Identificação de logs de compra duplicados para o mesmo CPF com diferença de milissegundos. | Deixar a transação inicial ocorrer, mas executar rotina diária que identifica compras anômalas e estorna o saldo automaticamente. |

---

## 2. Fichas de Definição Operacional (Métricas)

### Métrica de Fluxo
**Nome da Métrica:** Lead Time for Changes
**O que Mede:** O tempo que leva desde o momento em que um código é commitado pelo desenvolvedor até que ele esteja rodando com sucesso no ambiente de produção.
**Fórmula:** (Data e Hora do Deploy Produtivo) - (Data e Hora do Primeiro Commit da Tarefa)
**Fonte de Dados:** CI/CD Pipeline History (ex: GitHub Actions)
**Frequência de Coleta:** Mensal
**Limites de Saúde:** Saudável se <= 48 horas. Crítico se > 48 horas.
**Ação se Violado:** Identificar o gargalo no Kanban (ex: revisão de código travada) e estabelecer automações para diminuir atritos no deploy.

### Métrica de Qualidade
**Nome da Métrica:** Change Failure Rate (Taxa de Falha de Mudança)
**O que Mede:** A proporção de releases que vão para produção e geram incidentes, bugs severos ou necessidade de rollback imediato.
**Fórmula:** (Quantidade de Deploys que geraram falha / Quantidade Total de Deploys) * 100
**Fonte de Dados:** Tickets Corretivos no Jira e Histórico de Rollbacks do CI/CD
**Frequência de Coleta:** Mensal
**Limites de Saúde:** Excelente se < 15%. Preocupante se > 15%.
**Ação se Violado:** Aumentar imediatamente a meta de Cobertura de Testes para o próximo ciclo e instituir a obrigatoriedade de Testes de Integração para todas as rotas da Minimal API.

---

## 3. Service Level Objective (SLO)

A rota mais crítica e essencial para a rentabilidade da aplicação é a rota de Venda de Ingressos (Checkout).

**SLI (Indicador):** Taxa de disponibilidade e resposta rápida da API de vendas (Requisições retornando HTTP 200 em menos de 500 milissegundos).
**Fórmula de Coleta:** (Requisições de venda bem-sucedidas e em < 500ms) / (Total de requisições de venda recebidas) * 100
**Fonte do Dado:** Ferramenta de APM (Datadog ou Application Insights) consumindo logs do backend.
**Janela de Medição:** 30 dias
**Alvo (SLO):** 99.5%

### Error Budget Policy:

A política a seguir define as ações que a equipe deve tomar caso a indisponibilidade consuma o "Orçamento de Erro" permitido pelo SLO.

* **Nível 1 (Consumo < 50% do Error Budget):**
  A equipe pode manter o andamento normal da Sprint, priorizando o desenvolvimento de novas funcionalidades normalmente.

* **Nível 2 (Consumo entre 50% e 90% do Error Budget):**
  O alerta amarelo é acionado. O Product Owner passa a priorizar obrigatoriamente tarefas de estabilidade, dívida técnica e melhoria de testes. Funcionalidades não-críticas são rebaixadas no backlog.

* **Nível 3 (Consumo > 100% do Error Budget):**
  Alerta vermelho. Estabelece-se imediatamente o **Feature Freeze** (congelamento) do projeto. **Zero novas funcionalidades** podem ser desenvolvidas ou iniciadas sob nenhuma hipótese. O esforço integral de todos os desenvolvedores é revertido exclusivamente para investigar, corrigir gargalos de banco e código, até que o sistema retorne aos limites de confiabilidade.
