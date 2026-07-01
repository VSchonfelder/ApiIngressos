# Topologia de Times (Team Topologies)

No cenário em que o sistema TicketPrime sofra grande escalabilidade e o número de desenvolvedores aumente drasticamente, a organização estrutural do projeto deve seguir os 4 modelos de equipe fundamentais:

1. **Stream-aligned (Alinhado ao Fluxo de Valor)**
   *Contexto no Projeto:* Esta seria a equipe "Core" responsável pela jornada de ponta-a-ponta do cliente (Ex: "Squad de Checkout" ou "Squad de Eventos"). O time seria autossuficiente para criar a rota de compra, testar e fazer o deploy sem depender de ninguém, garantindo fluxo contínuo de valor.

2. **Platform (Plataforma)**
   *Contexto no Projeto:* Equipe responsável por criar um "PaaS (Plataforma como Serviço) interno" para reduzir a carga cognitiva da equipe *Stream-aligned*. Eles cuidariam de criar os scripts de CI/CD, Terraform para o banco de dados PostgreSQL e ferramentas de monitoramento para que a equipe de checkout não precise entender como configurar a infraestrutura AWS.

3. **Enabling (Facilitadores)**
   *Contexto no Projeto:* Um grupo de especialistas itinerantes. Se a equipe *Stream-aligned* estiver tendo dificuldades para implementar Dapper ou testes avançados no .NET 10, o time Enabling entra, ensina as melhores práticas, cria ferramentas de apoio temporárias e sai quando a equipe principal não precisar mais deles.

4. **Complicated-Subsystem (Subsistema Complicado)**
   *Contexto no Projeto:* Equipe ultraespecializada que cuida de um pedaço do sistema que exige muito conhecimento específico de domínio ou matemática densa. No caso da TicketPrime, seria o time que desenvolve e mantém o **Motor de Antifraude e Fila de Espera**. Esse motor exige algoritmos complexos, então fica isolado nessa equipe que apenas expõe uma API limpa para o time *Stream-aligned* usar.
