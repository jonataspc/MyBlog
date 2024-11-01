# Feedback do Instrutor

#### 23/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Separação de responsabilidades
- Demonstrou conhecimento em Identity e JWT.
- Boa validação de permissões e roles
- Bom uso e domínio de diversos recursos do ASP.NET
- Mostrou entendimento do ecossistema de desenvolvimento em .NET
- Projeto extramamente completo e funcional

## Pontos Negativos:

- Complexidade exagerada
    - O projeto é extremamente simples e requer uma arquitetura simples
    - Não existe necessidade de DDD, events, handlers
    - Esse tipo de implementação deveria ser reservada para os projetos mais complexos que virão.
- O uso de try-catch deve ser evitado, é possível validar um problema de permissão e simplesmente retornar um 403 ou uma notificação de falta de acesso.
- A API não permite criar usuário
- O processo de criação de usuário não está incluindo o autor.

## Sugestões:

- Unificar a criação do user + autor no mesmo processo. Utilize o ID do registro do Identity como o ID da PK do Autor, assim você mantém um link lógico entre os elementos.
- Simplificar a arquitetura 3 camadas (web, api, core) resolveriam tudo devido a baixa complexidade.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
