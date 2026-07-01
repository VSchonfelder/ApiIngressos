# ADR 001: Escolha do Micro-ORM para acesso a dados

**Status:** Aceito

## Contexto
O projeto TicketPrime precisa de uma forma de acessar o banco de dados PostgreSQL para realizar operações de leitura e escrita nas entidades de Eventos, Usuários e Cupons. A equipe precisa decidir qual abordagem utilizar, considerando que a aplicação exige alta performance e que o uso de frameworks "pesados" foi descartado devido ao objetivo de manter a API minimalista e rápida.

## Decisão
Decidimos utilizar o **Dapper** como nossa biblioteca de mapeamento objeto-relacional (Micro-ORM). Ele substituirá o uso de Entity Framework ou ado.net cru.

## Consequências

Esta decisão traz as seguintes consequências para o desenvolvimento e manutenção do sistema:

### Prós:
* Altíssima performance de execução, sendo praticamente tão rápido quanto o ADO.NET puro.
* Controle total sobre as queries SQL executadas, facilitando o uso do PostgreSQL e a otimização de `JOINs`.
* Facilita a parametrização de queries para evitar SQL Injection nativamente.

### Contras:
* Menor produtividade para operações básicas de CRUD, pois exige que os desenvolvedores escrevam comandos `INSERT`, `UPDATE` e `DELETE` na mão.
* Ausência de um mecanismo embutido de controle de esquema (Migrations), o que nos obrigará a gerenciar os scripts SQL (como o `script.sql`) manualmente.
