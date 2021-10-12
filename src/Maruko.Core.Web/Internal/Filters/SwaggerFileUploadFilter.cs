using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Maruko.Core.Web
{
    public class SwaggerFileUploadFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            #region 文件上传处理

            if (!context.ApiDescription.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
                !context.ApiDescription.HttpMethod.Equals("PUT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var fileParameters = context.ApiDescription.ActionDescriptor.Parameters
                .Where(n => n.ParameterType == typeof(IFormFile)).ToList();
            var attrParameters = context.ApiDescription.ActionDescriptor.Parameters
                .Where(n => n.ParameterType.GetCustomAttribute<SwaggerFileUploadAttribute>() != null).ToList();

            var isPass = true;
            MethodInfo methodInfo;
            if (context.ApiDescription.TryGetMethodInfo(out methodInfo)
                && methodInfo.GetCustomAttribute<SwaggerFileUploadAttribute>() != null)
            {
                isPass = false;
            }

            if (fileParameters.Count < 0 && attrParameters.Count < 0 && isPass)
            {
                return;
            }

            if (fileParameters.Count > 0)
            {
                foreach (var fileParameter in fileParameters)
                {
                    var parameter = operation.Parameters.Single(n => n.Name == fileParameter.Name);
                    operation.Parameters.Remove(parameter);
                    operation.RequestBody = GetRequestBody(parameter.Name);
                }
            }

            if (attrParameters.Count > 0)
            {
                foreach (var attrParameter in attrParameters)
                {
                    var parameter = operation.Parameters.Single(n => n.Name == attrParameter.Name);
                    operation.Parameters.Remove(parameter);
                    operation.RequestBody = GetRequestBody(parameter.Name);
                }
            }

            if (isPass == false)
            {
                operation.RequestBody = GetRequestBody("file_data");
            }

            #endregion
        }


        private OpenApiRequestBody GetRequestBody(string fileName)
        {
            Dictionary<string, OpenApiMediaType> content = new Dictionary<string, OpenApiMediaType>();
            Dictionary<string, OpenApiSchema> fileProperties = new Dictionary<string, OpenApiSchema>();
            fileProperties.Add($"{fileName}", new OpenApiSchema
            {
                Type = "string",
                Format = "binary",
                AdditionalPropertiesAllowed = true,
                Nullable = true
            });
            var schem = new OpenApiSchema
            {
                Type = "object",
                AdditionalPropertiesAllowed = true,
                //Format = "file",
                Properties = fileProperties
            };
            content.Add("multipart/form-data", new OpenApiMediaType
            {
                Schema = schem,
                Encoding = schem.Properties.ToDictionary(
                    entry => entry.Key,
                    entry => new OpenApiEncoding { Style = ParameterStyle.Form }
                )
            });
            var body = new OpenApiRequestBody();
            body.Content = content;
            return body;
        }
    }
}
