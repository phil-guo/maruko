using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maruko.CodeGeneration.Options;

namespace Maruko.CodeGeneration.Generate
{
    public partial class CodeGenerate
    {
        public static void GenerateWebApi(List<Dictionary<string, string>> list, bool ifExsitedCovered = false)
        {
            //todo 动态生成各种Controller
            list.ForEach(item =>
            {
                var path = $"{CodeGenerateOption.Disk}\\GenerateCode\\WebApi\\Controllers\\{item["FileName"]}";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                var domainFullPath = $"{path}\\{item["ClassName"]}Controller.cs";
                if (File.Exists(domainFullPath) && !ifExsitedCovered)
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

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using {solution}.Application.{fileName};
using {solution}.Application.{fileName}.DTO.{domainName};
using {solution}.Domain.{fileName};

namespace {solution}.WebApi.Controllers.{fileName}
{   
        [EnableCors(""cors"")]
        [ApiExplorerSettings(GroupName = ""V1"")]
        [Route(""api/v1/{fileNameLower}/"")]
        public class {domainName}Controller : ControllerBaseCrud<{domainName}, {domainName}Dto, Search{domainName}Dto>
        {
            public {domainName}Controller(I{domainName}Service crud) : base(crud)
            {
            }
        }
}";
                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"])
                    .Replace("{fileNameLower}", item["ClassName"].ToLower() + "s");
                WriteAndSave(domainFullPath, content);
            });

            //todo 生成WebApiModule
            var modulePath = $"{CodeGenerateOption.Disk}" + "\\GenerateCode\\WebApi\\{solutionSuffix}WebApiModule.cs";
            modulePath = modulePath
                .Replace("{solutionSuffix}", "Wl.Station.Baby".Split('.').LastOrDefault());
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

namespace {solution}.WebApi
{
    public class {solutionSuffix}WebApiModule : MarukoModule
    {
        public override double Order { get; set; } = 10;
    }
}";
            moduleContent = moduleContent.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                .Replace("{solution}", CodeGenerateOption.SolutionName)
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());

            WriteAndSave(modulePath, moduleContent);

            //todo 生成extension
            var extensionPath = $"{CodeGenerateOption.Disk}\\GenerateCode\\WebApi\\Extension\\";
            if (!Directory.Exists(extensionPath)) Directory.CreateDirectory(extensionPath);
            extensionPath += @"{solutionSuffix}WebApiModuleExtension.cs";
            extensionPath = extensionPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
            if (!File.Exists(extensionPath))
            {
                var extensionContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Autofac;
using Maruko;
using Maruko.AspNetMvc;
using Maruko.AutoMapper;
using Maruko.EntityFrameworkCore;
using Maruko.Extensions;
using Maruko.Hangfire;
using Maruko.Modules;
using Maruko.MongoDB;
using {solution}.Application;
using {solution}.Domain;
using {solution}.EntityFrameworkCore;

namespace {solution}.WebApi.Extension
{
    public static class {solutionSuffix}WebApiModuleExtension
    {
        public static void RegisterModules(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModulesExtension(() => new MarukoModule[]
            {
                new MarukoKernelModule(),
                new AspNetMvcModule(),
                new AutoMapperModule(),
                new MongoDbModule(),
                new {solutionSuffix}DomainModule(),
                new EntityFrameworkCoreModule(),
                new HangfireModule(),
                new {solutionSuffix}EntityFrameworkCoreModule(),
                new {solutionSuffix}ApplicationModule(),
                new HangfireModule(),
                new {solutionSuffix}WebApiModule {IsLastModule = true}
            });
        }
    }
}";
                extensionContent = extensionContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"));
                WriteAndSave(extensionPath, extensionContent);
            }
        }

        private static void WriteAndSave(string fileName, string content)
        {
            //实例化一个文件流--->与写入文件相关联
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                //实例化一个StreamWriter-->与fs相关联
                using (var sw = new StreamWriter(fs))
                {
                    //开始写入
                    sw.Write(content);
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                }
            }
        }
    }
}