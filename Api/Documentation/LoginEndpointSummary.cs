using Api.Endpoints;
using FastEndpoints;

namespace Api.Documentation;

public class LoginEndpointSummary : Summary<LoginEndpoint>
{
    public LoginEndpointSummary()
    {
        // Resumo e descrição do endpoint
        Summary = "Autenticação do usuário";
        Description = "Endpoint para autenticar o usuário e retornar um token JWT em caso de sucesso.";

        // Respostas possíveis
        Response<string>(200, "Autenticação bem-sucedida. Token JWT retornado.");
        Response<string>(401, "Credenciais inválidas.");
        Response<string>(404, "Usuário não encontrado.");
        Response<string>(500, "Erro interno no servidor ao gerar o token.");

        // Exemplo de payload de requisição
        ExampleRequest = new
        {
            Email = "usuario@example.com",
            Password = "SenhaSegura123"
        };
    }
}
