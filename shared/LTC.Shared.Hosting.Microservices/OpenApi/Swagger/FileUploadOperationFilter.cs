using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LTC.Shared.Hosting.Microservices.OpenApi.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasFormFile = context.MethodInfo
                .GetParameters()
                .Any(p => p.ParameterType == typeof(IFormFile));

            if (!hasFormFile) return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = JsonSchemaType.Object,
                            Properties =
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type   = JsonSchemaType.String,
                                    Format = "binary",
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
