# Book Store API - Desafio Solutis

### Funcionamento do Sistema

Focado em um sistema interno de venda de livros, a **API REST** bookStore possibilita aos seus usuários detalhamentos do seu negócio: livros.
Utilizando a **API externa da Google** é possível oferecer maiores informações sobre um determinado livro como descrição e autores, além disso,
conta com um sistema de **autentificação utilizando o JWT** e segurança de senha **MD5**.
Presando pela integridade dos dados foi escolhido o banco de dados **MySQL** para persistência de dados utilizando migrations visando a atualização automática do banco de dados quando ocorrer uma alteração no modelo de entidade. A database possui três tabelas: a de usuários, a de livros e a de compras.
Toda desenvolvida em **ASP.NET Core 3.1** e com arquitetura disponível para expansão de mais módulos e classes de negócios, a API, disponibiliza de um respositório genérico evitando repetição de código e possibilitando o reuso.

### Documentação da API 

A API bookStore foi completamente documentada utilizando **Swagger**.
É possível entender como cada método funciona, se comporta, quais os parâmetros são esperados, o que é retornado e se há necessidade ou não de autentificação. 

### Níveis de Acesso 

Esta API **possui níveis de acessos** para diferentes funcionalidades. 
Alguns métodos, como visualizaçāo de livros, são **públicos**, ou seja, liberados para visualizaçāo de qualquer que seja a pessoa.
Métodos de exclusão e criação são permitidos apenas para usuários com nível de gerência, vendedor e estoquista: são os métodos **privados.**

### Tecnologia Utilizada 

- [x] ASP.NET Core 3.1 
- [x] JWT (JSON Web Tokens) para autentificação
- [x] MySQL para persistência de dados
- [x] Swagger para documentação da API

### Tecnologia Suporte 

- [x] Heroku para Deploy
- [x] Visual Code
- [x] Azure para hospedagem do banco de dados
- [x] Docker para gerar imagem do projeto 
- [x] Grafana para criação de dash de monitoramento
- [x] Prometheus para coletar métricas da aplicação
- [x] Artillery para gerar teste de carga
- [x] Loki para coletar os logs da aplicação
- [x] Npm para suporte do Artillery

### Deploy

- [x] Link da aplicaçāo: https://book-solutis-api.herokuapp.com/swagger/index.html
- [x] Passo a passo para o build esta no repositório

### Acesso Local 

- [x] dotnet watch run 

### Pontos Fracos

- [x] Uso do md5 para criptografia (facilmente quebravel)

### Links

- [x] Grafana: localhost:3000
- [x] Prometheus: localhost:9090
- [x] Aplicação: localhost:8090/swagger/index.html

### Instrução Teste Carga
- Insira o comando no terminal.

         artillery run carga.yml

### Documentação de referencia 

- [x] Doc API: https://docs.microsoft.com/pt-br/aspnet/web-api/overview/older-versions/build-restful-apis-with-aspnet-web-api 
- [x] GitHub: https://github.com/dotnet
- [x] Autentificação: https://imasters.com.br/dotnet/asp-net-core-2-0-autenticacao-em-apis-utilizando-jwt-json-web-tokens
- [x] Docker: https://renatogroffe.medium.com/asp-net-core-docker-compose-implementando-solu%C3%A7%C3%B5es-web-multi-containers-5f46d22a2ca0
- [x] Artillery: https://www.artillery.io/docs/guides/guides/test-script-reference
- [x] Loki: https://grafana.com/oss/loki/
- [x] Grafana: https://grafana.com/docs/
- [x] Prometheus: https://prometheus.io/
