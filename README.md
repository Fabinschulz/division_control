
# Controle de Dívidas

Bem-vindo ao repositório do Controle de Dívidas! Este projeto foi desenvolvido para ajudá-lo a gerenciar suas dívidas de forma eficiente e organizada. Com uma combinação de tecnologias e padrões de design, construímos uma aplicação robusta e intuitiva para atender às suas necessidades.

## Objetivo

O objetivo principal deste projeto é fornecer uma plataforma simples e poderosa para controlar suas dívidas pessoais ou empresariais. Com o Controle de Dívidas, você poderá adicionar, visualizar, editar e excluir dívidas, além de acompanhar o seu saldo e definir metas financeiras.

## Tecnologias e Padrões Utilizados

### Back-end

-   Designer Patterns: Utilizamos padrões de design, como Singleton, Observer e Factory, para criar uma arquitetura sólida e flexível, facilitando a manutenção e expansão do código.
-   MVC (Model-View-Controller): Implementamos a separação clara de responsabilidades entre o modelo (dados), a visualização (interface do usuário) e o controlador (lógica da aplicação), garantindo um código mais organizado e de fácil entendimento.
-   MVP (Produto Viável Mínimo): Seguimos o conceito de desenvolvimento de um MVP, focando nas funcionalidades essenciais para uma primeira versão do sistema, permitindo um lançamento rápido e posterior iteração com base no feedback dos usuários.

### Front-end

-   Angular: Utilizamos o framework Angular para a construção do front-end, aproveitando seu poderoso sistema de componentes e facilidade de integração com o back-end.
-   SCSS: Usamos o SCSS (Sassy CSS) como pré-processador de CSS para facilitar a estilização e manutenção da interface, permitindo a criação de estilos reutilizáveis e mais organizados.

### Banco de dados

- Baixe SSMS - Microsoft SQL server management e SQL Express.(https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- Assista o video (https://www.youtube.com/watch?v=LxtLqS-9KYo&t=820s) para ajuda-lo na instalação. 
- Ou instale https://dbeaver.io/download/ (Recomendado).


### Recomendações

- Use o Visual Studio da Microsoft para melhor performance.
- Execute o comando: update-database -context SystemContext no gerenciador de pacote. OBS: O Projeto padrão deve estar apontado para ControleDeDividas.infra.

## Instruções de Instalação e Execução

Para executar o Controle de Dívidas em seu ambiente local, siga as etapas abaixo:

1.  Clone este repositório para o seu sistema.
2.  Certifique-se de ter o Node.js e o npm instalados em sua máquina.
3.  Acesse o diretório do projeto e execute o comando `npm install` para instalar as dependências necessárias.
4.  Execute o comando `ng serve` para iniciar o servidor de desenvolvimento.
5.  Abra o seu navegador e acesse `http://localhost:4200` para utilizar o Controle de Dívidas.

## Contribuição

Se você deseja contribuir para o projeto, siga as diretrizes abaixo:

1.  Fork este repositório e faça o clone para o seu sistema.
2.  Crie uma nova branch com uma descrição clara do que você está implementando ou corrigindo: `git checkout -b nome-da-sua-branch`.
3.  Faça as alterações necessárias e adicione os commits: `git commit -m "Descrição das alterações"`.
4.  Envie as alterações para o repositório remoto: `git push origin nome-da-sua-branch`.
5.  Abra um pull request para revisão do seu código.

## Equipe

-   [Fabio Lima](https://github.com/Fabinschulz)
-   [Maria Eduarda](https://github.com/lymaduds)
-   [Ibson Ramos](https://github.com/ibsonramos)
-   [Nedge Alves](https://github.com/nome-do-desenvolvedor)
-   [João Alberto](https://github.com/nome-do-desenvolvedor)

## Interface

- [Controle de Dividas](/frontend/src/assets/divisioncontrol.gif)
