Histórias de Usuário:

1. 

Como administrador,
Quero cadastrar usuários no sistema,
Para permitir que clientes possam comprar ingressos para eventos.

2. 

Como administrador,
Quero cadastrar cupons de desconto,
Para oferecer promoções e incentivar a compra de ingressos.

3. 

Como administrador,
Quero cadastrar eventos no sistema,
Para disponibilizar ingressos para venda aos usuários.

4. 

Como administrador,
Quero validar se um CPF já está cadastrado antes de criar um novo usuário,
Para evitar registros duplicados e garantir a integridade dos dados.


Critérios de Aceitação:

1.
Dado que já existe um usuário com o CPF "x",
quando tentar eu cadastrar outro usuário com o mesmo CPF.
Então o sistema deve retornar o erro 400.

2.
Dado que eu já peguei um cupom,
quando eu tentar pegar o mesmo cupom mais de uma vez.
Então o app precisa permitir que eu tenha apenas um do mesmo cupom.
