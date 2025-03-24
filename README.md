Thunders.TechTest
Visão Geral
Este repositório contém vários projetos que compõem a solução Thunders.TechTest, desenvolvida em .NET 8. A solução é composta pelos seguintes projetos:
•	Thunders.TechTest.ApiService: Serviço principal da API que lida com as operações de criação e consulta de tickets.
•	Thunders.TechTest.Application: Contém a lógica de aplicação, incluindo comandos e consultas.
•	Thunders.TechTest.Domain: Define as entidades de domínio e interfaces de repositório.
•	Thunders.TechTest.Infrastructure: Implementa a persistência de dados, incluindo a configuração do MongoDB.
Thunders.TechTest.ApiService
Descrição
O projeto Thunders.TechTest.ApiService é o serviço principal da API que expõe endpoints para a criação e consulta de tickets. Utilizamos o padrão CQRS (Command Query Responsibility Segregation) para separar as operações de leitura e escrita, o que nos permite otimizar as consultas diretamente no MongoDB.
Tecnologias Utilizadas
•	.NET 8: Framework principal para o desenvolvimento da aplicação.
•	MediatR: Biblioteca para implementar o padrão Mediator, facilitando a comunicação entre componentes.
•	SqlServer + ef core: Bibliotecas para implementar a inserção e consistencia dos dados. utilizado apenas nos commands.
•	MongoDB: Banco de dados NoSQL utilizado para armazenar os dados dos tickets.
Padrão CQRS
Optamos por utilizar o padrão CQRS para separar as operações de leitura e escrita. Isso nos permite otimizar as consultas diretamente no MongoDB, melhorando a performance e a escalabilidade da aplicação.
Escolha do MongoDB
Escolhemos o MongoDB devido à familiaridade com a tecnologia. No entanto, com mais tempo, poderíamos pesquisar outras opções de bancos de dados que poderiam se adequar melhor às necessidades do projeto.
Mediator vs RabbitMQ
Decidimos utilizar o MediatR para gerenciar os eventos dentro da aplicação, pois achamos mais simples e eficaz para o escopo atual do projeto. No futuro, uma possível melhoria seria implementar uma solução de mensageria com RabbitMQ, incluindo uma fila de retry para garantir a persistência eventual dos registros.
Endpoints
Abaixo estão alguns dos principais endpoints expostos pelo TicketController:
•	POST /api/Ticket: Cria um novo ticket.
•	GET /api/Ticket/pracas-que-mais-faturaram-por-mes: Retorna as praças que mais faturaram em um determinado mês.
•	GET /api/Ticket/valor-total-por-hora-por-cidade: Retorna o valor total por hora por cidade.
•	GET /api/Ticket/tipo-de-veiculos-por-praca: Retorna os tipos de veículos por praça.

Executando Testes de Performance com K6
Para garantir a performance e a escalabilidade da aplicação, utilizamos o K6 para realizar testes de carga. Siga os passos abaixo para rodar os testes K6 no projeto Thunders.TechTest.Tests.Tickets.
Passos para Executar os Testes K6
1.	Instalar o K6: Se você ainda não tem o K6 instalado, você pode instalá-lo seguindo as instruções no site oficial do K6.
2.	Navegar até o diretório do projeto: Abra o terminal e navegue até o diretório onde os testes K6 estão localizados. Supondo que os testes estejam no diretório Thunders.TechTest.Tests.Tickets, você pode usar o seguinte comando:  
    cd Thunders.TechTest.Tests.Tickets
3.	Executar os testes K6: Execute os testes K6 usando o comando k6 run seguido do nome do arquivo de script de teste. Supondo que o arquivo de script de teste seja test-script.js, o comando seria:
    k6 run InsercaoTicketTest.js
    k6 run PracasQueMaisFaturaramPorMesQueryTest.js
    k6 run TipoDeVeiculosPorPracaQueryTest.js
    k6 run ValorTotalPorHoraPorCidadeQueryTest.js



