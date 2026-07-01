# Segurança no Ciclo de Desenvolvimento (SSDF)

## A. Threat Model (Modelagem de Ameaças)

A rota de maior risco identificada no sistema TicketPrime é a rota de Venda de Ingressos e Pagamentos (`POST /api/eventos/comprar`), pois é onde ocorre o principal fluxo de dados financeiros e a alocação de bens digitais (os ingressos reais).

**Ativos Protegidos:**
Dados pessoais dos clientes (como nome e CPF), histórico de transações de compra e os limites de saldo dos cupons de desconto promocionais.

**Vetor de Ataque Provável:**
Ataque de Injeção de SQL (SQL Injection) inserindo strings maliciosas no campo de `Cpf` ou manipulação massiva (força-bruta) para tentar resgatar o mesmo cupom de desconto dezenas de vezes antes que o banco de dados registre a primeira transação.

**Falha Arquitetural Potencial:**
Ausência de validação (sanitização) nas entradas do usuário e a falta de controle de concorrência (ex: transações no banco de dados e bloqueios em tabela) no momento da confirmação de uso do cupom de desconto.

**Controle de Engenharia (Mitigação):**
Para anular injeção de SQL, será imposto o uso exclusivo de *queries parametrizadas* (`@Parametro`) fornecidas nativamente pelo Dapper. Para o problema de fraude de cupom, será implementado *Lock Otimista/Pessimista* em nível de transação do PostgreSQL.

---

## B. Gates de Segurança no Pipeline

Para garantir que a infraestrutura se mantenha imaculada e dentro do compliance de segurança, a equipe aprovará os códigos baseando-se em 3 portões rígidos e automatizados.

**Gate 1**
**Análise Estática de Código (SAST):** Todo Pull Request criado pelos desenvolvedores acionará um robô (como o SonarQube ou GitHub Advanced Security) que vai ler o código e **bloquear** o "Merge" automaticamente caso detecte strings literais de senhas ou consultas SQL vulneráveis inseridas no código C#.

**Gate 2**
**Análise de Composição de Software (SCA):** Um serviço (como o Dependabot) fará checagens semanais no arquivo `.csproj`. Se o pacote do Dapper ou Npgsql estiver desatualizado e possuir uma vulnerabilidade conhecida (CVE) divulgada mundialmente, o deploy produtivo será bloqueado até a equipe atualizar a biblioteca.

**Gate 3**
**Análise Dinâmica de API (DAST):** Antes da liberação da release, um script automatizado rodará contra o ambiente de homologação atirando centenas de `payloads` inválidos conhecidos (dicionário OWASP Top 10) na Minimal API. Se o endpoint retornar exceções técnicas brutas (estouro de pilha, vazamento de path interno), o processo de deploy é interrompido.
