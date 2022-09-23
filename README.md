# Cinemark
Projeto de Teste de Desenvolvedor Backend .NET da Cinemark

## Arquitetura

Toda a aplicação foi desenvolvida utilizando ASP.NET Core 6 com padrão de arquitetura DDD com CQRS, portanto a API possui dois banco de dados segregados, o MongoDB para leitura dos dados e o SQL Server para escrita, um componente é responsável para a sincronização dos dados através de mensagerias do RabiitmQ. A API possui um endpoint Token Get para obter um JWT, este que por sua vez, precisa ser inserido na Header das requições de CRUD de Filmes.

## Ambiente

### Docker
Acesse o link abaixo para baixar as seguintes imagens do docker:
* https://hub.docker.com/_/microsoft-mssql-server
* https://hub.docker.com/_/mongo
* https://hub.docker.com/_/rabbitmq

Em seguida execute o seguintes comandos:
```
CMD> docker pull mcr.microsoft.com/mssql/server
CMD> docker pull mongo
CMD> docker pull rabbitmq
```
Após a execução dos comandos acima, basta inicializar os container das imagens com os comandos:
```
CMD> docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Str0ngPa$$w0rd" -p 1433:1433 -d mcr.microsoft.com/mssql/server
CMD> docker run -d --name mongodb -p 27017:27017 mongo
CMD> docker run -d --hostname rabbitserver --name rabbitmq-server -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
* Para MacOs com arquitetura AMR64, utiliza o Azure-SQL-Edge ao invés do MSSQL
```
CLI> docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Str0ngPa$$w0rd' -p 1433:1433 -d mcr.microsoft.com/azure-sql-edge
```
### Banco de Dados
Para a criação do banco de dados e tabelas, entre no Gerenciador de Pacotes Nuget e selecione o projeto "Cinemark.Infrastructure.Data" e execute o seguinte comando:
```
PM> Update-Database
```
* Para MasOs/Linux,ir até o diretório "Cinemark.Service.Api" e executar o seguinte comando:
```
CLI> dotnet ef database update
```
Para acessar o SQL Server no client, utilize as seguintes credenciais:
```
Server: localhost
User: sa
Password: Str0ngPa$$w0rd
```
Para a criação da tabela de Usuário, acesse o SQL Server com o Login "sa" e Senha "Str0ngPa$$w0rd" e execute o seguinte script:
```
USE [cinemark]
GO

INSERT INTO [dbo].[Usuario]
           ([Nome]
           ,[Email]
           ,[Senha]
           ,[DataCriacao])
     VALUES
           ('Teste'
           ,'teste@cinemark.com'
           ,'12345'
           ,GETDATE())
GO
```
Para acessar terminal do container e o MongoDB, execute os comandos abaixo:
```
CMD> docker exec -it [ID do Container Docker do Mongo] bash  
```
```
CLI> mongosh
```
Para a criação da collection de Usuário acesse o MondoDB e execute os seguintes scripts:
```
use cinemark
```
```
db.Usuario.insert(
  {
  	"_id": 1,
  	"Nome": "Teste",
  	"Email": "teste@cinemark.com",
  	"Senha": "12345",
  	"DataCriacao": new Date()
  }
)
```
### RabiitMQ
Para acessar o RabbitMQ Management, utilize as seguintes credenciais:
```
Host: http://localhost:15672/
User: guest
Password: guest
```
## Requisições

O projeto possui Documentação pelo Swagger, portanto possui os seguintes endpoints:

### GET Token

Para obter um Token JWT e utilizar das requições dos endpoints de Filmes, utilize o seguinte Email e Senha:
```
Email: teste@cinemark.com 
Senha: 12345
```
```
curl -X 'GET' \
  'https://localhost:7189/api/Token?Email=teste%40cinemark.com&Senha=12345' \
  -H 'accept: text/plain'
```

### GET Filmes

Para obter todos os Filmes utilze o endpoint abaixo:
```
curl -X 'GET' \
  'https://localhost:7189/api/Filme' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

### GET Filmes por Id

Para obter um Filme pelo seu Id utilze o endpoint abaixo:
```
curl -X 'GET' \
  'https://localhost:7189/api/Filme/1' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

### POST Filme

Para criar um Filme utilze o endpoint abaixo:
```
curl -X 'POST' \
  'https://localhost:7189/api/Filme' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA' \
  -H 'Content-Type: application/json' \
  -d '{
  "nome": "Star Wars Episorio I - A Ameaça Fantasma",
  "categoria": "Ficção",
  "faixaEtaria": 12,
  "dataLancamento": "1999-05-04T14:00:00.000Z"
}'
```

### PUT Filme

Para editar um Filme utilze o endpoint abaixo:
```
curl -X 'PUT' \
  'https://localhost:7189/api/Filme' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 1,
  "nome": "Star Wars Episorio I - A Ameaça Fantasma [Edição]",
  "categoria": "Ficção",
  "faixaEtaria": 10,
  "dataLancamento": "1999-05-04T14:00:00.000Z"
}'
```

### DELETE Filmes

Para deletar um Filme utilze o endpoint abaixo:
```
curl -X 'DELETE' \
  'https://localhost:7189/api/Filme/1' \
  -H 'accept: text/plain' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0ZUBjaW5lbWFyay5jb20iLCJleHAiOjE2NTYwMjc0MjQsImlzcyI6ImNoYXJsZXMubWVuZGVzIiwiYXVkIjoiY2hhcmxlcy5tZW5kZXMifQ.UubI-d6hL1KsqZiZxSoDbLHL2PG7k83qiS2TAgpkIWA'
```

*A porta de utilização da API pode variar dependêndo da execução da mesma pelo Visual Studio.
