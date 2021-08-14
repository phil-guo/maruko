using System;
using System.Collections.Generic;
using System.IO;
using Maruko.CodeGeneration.Options;

namespace Maruko.CodeGeneration.Generate
{
    public partial class CodeGenerate
    {
        public static void GenerateController(List<string> list)
        {
            list.ForEach(item =>
            {
                //todo 生成Service
                var path = CodeGenerateOption.Disk + $"\\GenerateCode\\Controllers";
                if (!Directory.Exists(path)) 
                    Directory.CreateDirectory(path);
                var fullPath = $"{path}\\{item}Controller.cs";

                var content = @"using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace {solutionName}
{
        [EnableCors(""cors"")]
        [Route(""api/v1/{domainNameLower}s/"")]
        public class {domainName}Controller : BaseCurdController<{domainName}, {domainName}DTO>
        {
            public PageController(ICurdAppService<{domainName}, {domainName}DTO> curd) : base(curd)
            {
            }
        }
    }";
                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item)
                    .Replace("{domainNameLower}", item.ToLower())
                    .Replace("{solutionName}", CodeGenerateOption.SolutionName);
                WriteAndSave(fullPath, content);
            });
        }
    }
}