using Maruko.CodeGeneration.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Maruko.CodeGeneration.Generate
{
    public partial class CodeGenerate
    {
        public static void GenerateService(List<string> list)
        {
            list.ForEach(item =>
            {
                //todo 生成Service
                var path = CodeGenerateOption.Disk + $"\\GenerateCode\\Service\\Imp";
                if (!Directory.Exists(path)) 
                    Directory.CreateDirectory(path);
                var fullPath = $"{path}\\{item}Service.cs";

                var content = @"//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================


using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;

namespace {solutionName}
{
    public class {domainName}Service : CurdAppService<{domainName}, {domainName}DTO>, I{domainName}Service
    {
        public {domainName}Service(IObjectMapper objectMapper, IFreeSqlRepository<{domainName}> repository) : base(objectMapper, repository)
        {
        }
    }
}
";

                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item)
                    .Replace("{solutionName}", CodeGenerateOption.SolutionName);
                WriteAndSave(fullPath, content);
                
                //todo 生成 IService
                path = CodeGenerateOption.Disk + $"\\GenerateCode\\Service";
                if (!Directory.Exists(path)) 
                    Directory.CreateDirectory(path);
                fullPath = $"{path}\\I{item}Service.cs";

                content = @"using Maruko.Core.FreeSql.Internal.AppService;

namespace {solutionName}
{
    public interface I{domainName}Service : ICurdAppService<{domainName}, {domainName}DTO>
    {
    }
}";
                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item)
                    .Replace("{solutionName}", CodeGenerateOption.SolutionName);
                WriteAndSave(fullPath, content);
            });          
        }
    }
}
