using Api.Endpoints;
using FastEndpoints;

namespace Api.Documentation;

public class CreateUsuarioEndpointSummary : Summary<CreateUsuarioEndpoint>
{
    public CreateUsuarioEndpointSummary()
    {
        // Resumo e descrição do endpoint
        Summary = "Criação de um novo usuário";
        Description = "Endpoint para criar um novo usuário com os dados fornecidos.";

        // Respostas possíveis
        Response<int>(200, "Usuário criado com sucesso.");
        Response<int>(400, "Erro ao criar o usuário.");

        // Exemplo de payload de requisição
        ExampleRequest = new
        {
            Nome = "João",
            Sobrenome = "Silva",
            Cpf = "12345678901",
            Email = "joao.silva@example.com",
            Password = "SenhaForte123",
            DataNascimento = new DateOnly(1990, 5, 15),
            Sexo = "Masculino"
        };
    }
}
