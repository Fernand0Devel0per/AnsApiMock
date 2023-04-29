# AnsApiMock
# ANS API Clone

Este projeto é um clone da API do governo da ANS (Agência Nacional de Saúde). A API possui diversos endpoints que permitem gerenciar petições e documentos relacionados. Além disso, o projeto conta com um sistema simples de autenticação via middleware.

## Endpoints

Aqui estão os principais endpoints disponíveis na API:

### 1. Criar petição

```
POST /e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/{tipoProtocolo}/{assunto}
```

Cria uma petição com base nos dados fornecidos. Retorna um objeto JSON contendo informações sobre a petição criada.

### 2. Processar documento principal

```
POST /e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/{tipoProtocolo}/{assunto}/{codProtocolo}/documentos/principais
```

Processa um documento principal e associa-o à petição especificada. Retorna um objeto JSON contendo informações sobre o documento processado.

### 3. Processar documento complementar

```
POST /e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/{tipoProtocolo}/{assunto}/{codProtocolo}/documentos/complementares
```

Processa um documento complementar e associa-o à petição especificada. Retorna um objeto JSON contendo informações sobre o documento processado.

### 4. Consultar impugnações concluídas

```
POST /e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/ressarcimento/impugnacoes/concluidas
```

Retorna um status de sucesso (200 OK) caso a requisição seja bem-sucedida.

## Autenticação

O projeto utiliza um sistema simples de autenticação via middleware. Para utilizar a API, é necessário fornecer um token válido no cabeçalho da requisição.

## Requisitos

Para executar este projeto, é necessário ter instalado:

- .NET 7.0 ou superior

## Executando o projeto

Para executar o projeto, siga os seguintes passos:

1. Clone o repositório
2. Navegue até a pasta do projeto
3. Execute o comando `dotnet run`

## Contribuindo

Sinta-se à vontade para contribuir com o projeto, reportando bugs ou sugerindo melhorias.

## Licença

Este projeto está licenciado sob a licença MIT.

# Funcionalidades adicionais

Além dos endpoints mencionados anteriormente, a API também oferece funcionalidades adicionais que auxiliam no gerenciamento de petições e documentos.

## Banco de dados

A API utiliza um banco de dados para armazenar informações sobre as petições e documentos processados. O banco de dados é gerenciado através do Entity Framework Core, facilitando a criação e aplicação de migrações quando necessário.

Para aplicar migrações pendentes, é utilizado o seguinte método:


```
void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetService<MockAnsDbContext>();
    dbContext.Database.Migrate();
}

```

## Serviços

A API conta com serviços que encapsulam a lógica de negócio para cada endpoint, facilitando a manutenção e a extensão das funcionalidades. Os principais serviços são:

- `IProtocoloService`: responsável pela criação e gerenciamento de petições.
- `IDocumentoService`: responsável pelo processamento e gerenciamento de documentos principais e complementares.

## Tratamento de erros

O projeto conta com tratamento de erros para garantir que as respostas da API sejam consistentes e informativas. Exemplos de erros tratados incluem requisições inválidas, ausência de arquivos e falhas durante o processamento de documentos.

As respostas de erro incluem informações como código, mensagem e data de ocorrência do erro, facilitando a identificação e resolução de problemas.
