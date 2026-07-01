# Fluxo de Manutenção e Liberação Segura

## 1. Classificação de Manutenção (Taxonomia de Swanson)

Abaixo estão classificados 12 tickets fictícios de manutenção do sistema TicketPrime, utilizando a taxonomia de Swanson (Corretiva, Adaptativa, Perfectiva, Preventiva).

* **Ticket 1 → Corretiva**
  * *Descrição:* Corrigir o erro 500 que ocorre quando um usuário tenta cadastrar um evento sem data preenchida.
* **Ticket 2 → Adaptativa**
  * *Descrição:* Modificar a integração de pagamentos para suportar a nova versão obrigatória da API da Cielo que entrará em vigor mês que vem.
* **Ticket 3 → Perfectiva**
  * *Descrição:* Adicionar um novo filtro de busca na listagem de eventos para permitir que os usuários busquem por categoria (ex: Show, Teatro).
* **Ticket 4 → Preventiva**
  * *Descrição:* Refatorar o código do `Program.cs` para isolar as rotas em extensões separadas, facilitando a manutenção futura pela equipe.
* **Ticket 5 → Corretiva**
  * *Descrição:* Resolver bug onde o cálculo do desconto do cupom é aplicado em dobro caso o usuário clique duas vezes no botão rapidamente.
* **Ticket 6 → Adaptativa**
  * *Descrição:* Mascarar a exibição do CPF nas respostas da API para adequação ambiental à nova norma da LGPD.
* **Ticket 7 → Preventiva**
  * *Descrição:* Atualizar as bibliotecas (ex: Dapper, Npgsql) para suas versões mais recentes visando mitigar vulnerabilidades futuras recém-descobertas.
* **Ticket 8 → Perfectiva**
  * *Descrição:* Implementar funcionalidade de exportação de lista de presença do evento em formato PDF para os organizadores.
* **Ticket 9 → Corretiva**
  * *Descrição:* Consertar a validação de email no cadastro de usuário que está recusando emails com o domínio `.io`.
* **Ticket 10 → Adaptativa**
  * *Descrição:* Migrar a hospedagem do banco de dados para a nova versão do PostgreSQL (v16) devido ao fim do suporte da versão atual na nuvem AWS.
* **Ticket 11 → Perfectiva**
  * *Descrição:* Melhorar a interface visual do sistema de administrador, adicionando um dashboard gráfico de vendas na página inicial.
* **Ticket 12 → Preventiva**
  * *Descrição:* Criar e aumentar a cobertura de testes unitários nas regras de validação de ingressos para evitar que desenvolvedores juniores quebrem a lógica acidentalmente nas próximas sprints.

---

## 2. Pipeline de Liberação Segura

Para garantir que a resolução de um ticket crítico de correção (ex: Ticket 1 ou Ticket 5) chegue à produção sem causar indisponibilidade, utilizaremos o seguinte pipeline de liberação em 4 passos:

### 1. Análise de Impacto
Antes de qualquer linha de código ser escrita, a equipe avalia onde o bug ocorre e quais outros serviços dependem desse fluxo. Levantamos quais tabelas do banco serão lidas e mapeamos o "raio de explosão" caso a correção dê errado, garantindo que o problema está isolado.

### 2. Teste como Instrumento Cirúrgico
A correção não é testada manualmente clicando no sistema inteiro. O desenvolvedor escreve um teste de unidade automatizado que **reproduz exatamente o bug reportado**. Em seguida, aplica a correção para fazer esse teste específico passar, provando matematicamente que o erro original foi resolvido no microscópio sem afetar o resto.

### 3. Feature Toggle
A alteração no código é envolvida em uma variável de ambiente ou chave de configuração (`Feature Toggle / Feature Flag`). Isso permite que a correção suba para produção desligada por padrão. Caso haja algum comportamento anômalo, podemos ligar e desligar a correção instantaneamente sem precisar fazer um novo deploy (rollback instantâneo).

### 4. Estratégia de Release e Regressão
A liberação é feita através de *Canary Release* (liberação canário): o novo código é direcionado inicialmente para apenas 5% dos usuários. A equipe de operações monitora as métricas (DORA, logs de erro) durante 1 hora. Se não houver regressão (aumento de falhas ou lentidão em outras rotas), a alteração é gradualmente expandida para 100% da base de usuários. Se der problema, o Feature Toggle é desligado.
