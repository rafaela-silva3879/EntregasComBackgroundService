using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Entregas.Service.Configurations
{
    public class SwaggerConfiguration
    {
        public static void AddSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "API para controle pedidos",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafaela Silva",
                        Email = "rafaela.silva3879@outlook.com",
                        Url = new
                Uri("https://github.com/rafaela-silva3879")
                    }
                });
                //gerando um arquivo XML dentro da pasta
                //de compilação do projeto
                //contendo os comentários XML feitos no código
                var xmlFile = $"{Assembly.GetExecutingAssembly()
                .GetName().Name}.xml";
                //capturar o local onde é gerado o arquivo anterior
                var xmlPath = Path.Combine
                (AppContext.BaseDirectory, xmlFile);
                //configurando o Swagger para incluir
                //os comentários xml na página de documentação
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
