using System.Web.Http;
using WebActivatorEx;
using ProtocoloConsultasAPI;
using Swashbuckle.Application;
using System;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ProtocoloConsultasAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ProtocoloConsultasAPI")
                        .Description("Esta API permite a consulta e o gerenciamento de protocolos.")
                        .TermsOfService("Termos de uso da API")
                        .Contact(contact =>
                        {
                            contact.Name("Vinicius Gomes");
                            contact.Email("viniciusgarciagomes@hotmail.com");
                            contact.Url("http://exemplo.com");
                        })
                        .License(license =>
                        {
                            license.Name("Licença");
                            license.Url("http://exemplo.com/licenca");
                        });

                    c.IgnoreObsoleteActions();
                    c.DescribeAllEnumsAsStrings();
                    c.UseFullTypeNameInSchemaIds();

                    c.OperationFilter<AddDefaultResponse>();
                })
                .EnableSwaggerUi(c =>
                {
                    // Título da documentação
                    c.DocumentTitle("Documentação da API Protocolo Consultas");

                    // Ativar o suporte ao OAuth2, se necessário
                    // c.EnableOAuth2Support(
                    //     clientId: "seu-client-id",
                    //     clientSecret: null,
                    //     realm: "seu-realm",
                    //     appName: "Swagger UI"
                    // );

                    // Suporte para chaves de API
                    // c.EnableApiKeySupport("apiKey", "header");
                });
        }
    }

    // Exemplo de filtro de operação para adicionar resposta padrão
    public class AddDefaultResponse : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (!operation.responses.ContainsKey("200"))
                operation.responses.Add("200", new Response { description = "Operação bem-sucedida." });

            if (!operation.responses.ContainsKey("400"))
                operation.responses.Add("400", new Response { description = "Requisição inválida." });

            if (!operation.responses.ContainsKey("500"))
                operation.responses.Add("500", new Response { description = "Erro interno do servidor." });
        }
    }
}
