using Microsoft.EntityFrameworkCore;
using MockAbiANS.Data;
using MockAbiANS.Middlewares;
using MockAbiANS.Repository.Interface;
using MockAbiANS.Repository;
using MockAbiANS.DTOs.Peticao;
using MockAbiANS.Service.Interface;
using MockAbiANS.Service;
using MockAbiANS.Util.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MockAbiANS.Entities;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<MockAnsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MockAns")));

// Injeção de Dependência
builder.Services.AddScoped<IProtocoloRepository, ProtocoloRepository>();
builder.Services.AddScoped<IInformacaoAdicionalRepository, InformacaoAdicionalRepository>();
builder.Services.AddScoped<IDocumentoRepository, DocumentoRepository>();
builder.Services.AddScoped<IProtocoloService, ProtocoloService>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();

// Configurando AutoMapper
builder.Services.AddAutoMapper(typeof(DocumentoMappingProfile));
var app = builder.Build();

// Aplicar migrações pendentes antes de iniciar o servidor
ApplyMigrations(app);

// Registrando Autenticação simples.
app.UseMiddleware<SimpleTokenValidationMiddleware>();

app.MapPost("/e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/{tipoProtocolo}/{assunto}",
    async (int codOperadora, string tipoProtocolo, string assunto,
    PeticaoRequest peticaoRequest,
    IProtocoloService protocoloService) =>
    {
        try
        {
            var response = await protocoloService.CriarPeticao(codOperadora, tipoProtocolo, assunto, peticaoRequest);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new
            {
                codigo = ex.Message,
                mensagem = "Ocorreu um erro ao criar a petição.",
                dataOcorrencia = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };
            return Results.Json(errorResponse, statusCode: 500);
        }
    });

app.MapPost("/e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/{tipoProtocolo}/{assunto}/{codProtocolo}/documentos/principais", async (string versao, int codOperadora, string tipoProtocolo, string assunto, string codProtocolo, HttpRequest req, IDocumentoService documentoService) =>
{
    try
    {
        var request = await ExtrairDocumentoRequest(req);

        var documentoResponse = await documentoService.ProcessarDocumentoAsync(codOperadora, request, codProtocolo);
        return Results.Ok(documentoResponse);
    }
    catch (ArgumentException ex)
    {
        var errorResponse = new
        {
            codigo = ex.Message,
            mensagem = "Requisição inválida.",
            dataOcorrencia = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };
        return Results.BadRequest(errorResponse);
    }
    catch (Exception ex)
    {
        var errorResponse = new
        {
            codigo = ex.Message,
            mensagem = "Ocorreu um erro ao processar o documento principal.",
            dataOcorrencia = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };
        return Results.Json(errorResponse, statusCode: 500);
    }
});

app.MapPost("/e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/{tipoProtocolo}/{assunto}/{codProtocolo}/documentos/complementares", async (string versao, int codOperadora, string tipoProtocolo, string assunto, string codProtocolo, HttpRequest req, IDocumentoService documentoService) =>
{
    try
    {
        var request = await ExtrairDocumentoRequest(req);

        var documentoResponse = await documentoService.ProcessarDocumentoAsync(codOperadora, request, codProtocolo);
        return Results.Ok(documentoResponse);
    }
    catch (ArgumentException ex)
    {
        var errorResponse = new
        {
            codigo = ex.Message,
            mensagem = "Requisição inválida.",
            dataOcorrencia = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };
        return Results.BadRequest(errorResponse);
    }
    catch (Exception ex)
    {
        var errorResponse = new
        {
            codigo = ex.Message,
            mensagem = "Ocorreu um erro ao processar o documento complementar.",
            dataOcorrencia = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };
        return Results.Json(errorResponse, statusCode: 500);
    }
});

app.MapPost("/e-protocolo/{versao}/operadoras/{codOperadora}/peticoes/ressarcimento/impugnacoes/concluidas", async (string versao, int codOperadora, HttpRequest req) =>
{
    return Results.Ok("Deu Tudo certo");
});

app.Run();

// Método para aplicar migrações pendentes
void ApplyMigrations(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetService<MockAnsDbContext>();
    dbContext.Database.Migrate();
}

async Task<DocumentoRequest> ExtrairDocumentoRequest(HttpRequest req)
{
    if (!req.HasFormContentType)
    {
        throw new ArgumentException("Requisição inválida. Envie os dados no formato 'multipart/form-data'.");
    }

    var form = await req.ReadFormAsync();
    var arquivo = form.Files.GetFile("arquivo");

    if (arquivo == null)
    {
        throw new ArgumentException("Arquivo não encontrado na requisição.");
    }

    byte[] arquivoBytes;
    using (var memoryStream = new MemoryStream())
    {
        await arquivo.CopyToAsync(memoryStream);
        arquivoBytes = memoryStream.ToArray();
    }

    int? tipoDocumento = null;
    if (int.TryParse(form["tipoDocumento"], out int result))
    {
        tipoDocumento = result;
    }

    return new DocumentoRequest
    {
        Hash = form["hash"],
        NomeArquivo = form["nomeArquivo"],
        Assunto = form["assunto"],
        TipoDocumento = tipoDocumento,
        DataDocumento = form["dataDocumento"],
        Arquivo = arquivoBytes
    };
}
