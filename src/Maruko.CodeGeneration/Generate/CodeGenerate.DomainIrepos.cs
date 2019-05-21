using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maruko.CodeGeneration.Options;

namespace Maruko.CodeGeneration.Generate
{
    public partial class CodeGenerate
    {
        public static void GenerateDomain(List<Dictionary<string, string>> list, bool ifExsitedCovered = false)
        {
            //todo 动态生成各种实体的仓储
            list.ForEach(item =>
            {
                var path = CodeGenerateOption.Disk + $"\\GenerateCode\\Domain\\{item["FileName"]}\\IRepos";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = $"{path}\\I{item["ClassName"]}Repos.cs";
                if (File.Exists(fullPath) && !ifExsitedCovered)
                    return;

                var content = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.Dependency;
using Maruko.Domain.Repositories;

namespace {solution}.Domain.{fileName}.IRepos
{
    public interface I{domainName}Repos : IRepository<{domainName}>, IDependencyScoped
    {
    }
}
";

                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"]);
                WriteAndSave(fullPath, content);
            });

            //todo 生成domainModule
            //todo 生成dbcontext上下文 
            var domainModulePath = CodeGenerateOption.Disk + "\\GenerateCode\\Domain\\{solutionSuffix}DomainModule.cs";
            domainModulePath = domainModulePath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
            if (!File.Exists(domainModulePath))
            {
                var moduleContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================
using Maruko.Modules;

namespace {solution}.Domain
{
    public class {solutionSuffix}DomainModule : MarukoModule
    {
        public override double Order { get; set; } = 7;
    }
}
";
                moduleContent = moduleContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);

                WriteAndSave(domainModulePath, moduleContent);
            }
        }
    }
}
