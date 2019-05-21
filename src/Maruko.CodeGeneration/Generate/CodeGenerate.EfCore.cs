using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Maruko.CodeGeneration.Options;

namespace Maruko.CodeGeneration.Generate
{
    public partial class CodeGenerate
    {
        public static void GenerateEntityFramework(List<Dictionary<string, string>> list, bool ifExsitedCovered = false)
        {
            //todo 动态生成各种实体的仓储
            list.ForEach(item =>
            {
                var path = CodeGenerateOption.Disk + $"\\GenerateCode\\EntityFrameworkRepos\\Repos\\{item["FileName"]}";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullPath = $"{path}\\{item["ClassName"]}Repos.cs";
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

using Maruko.EntityFrameworkCore.UnitOfWork;
using {solution}.Domain.{fileName};
using {solution}.Domain.{fileName}.IRepos;

namespace {solution}.EntityFrameworkCore.Repos.{fileName}
{
    public class {domainName}Repos : BaseRepository<{domainName}>, I{domainName}Repos
    {
        public {domainName}Repos(IEfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}";
                content = content.Replace("{templateTime}", DateTime.Now.ToString("MM/dd/yyyy"))
                    .Replace("{domainName}", item["ClassName"])
                    .Replace("{solution}", CodeGenerateOption.SolutionName)
                    .Replace("{fileName}", item["FileName"]);
                WriteAndSave(fullPath, content);
            });

            //todo 生成Module基类
            var efCoreModelPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\{solutionSuffix}EntityFrameworkCoreModule.cs";
            efCoreModelPath = efCoreModelPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                .Replace("{solution}", CodeGenerateOption.SolutionName);

            if (!File.Exists(efCoreModelPath))
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

namespace {solution}.EntityFrameworkCore
{
    public class {solutionSuffix}EntityFrameworkCoreModule : MarukoModule
    {
        public override double Order { get; set; } = 8;
    }
}";
                moduleContent = moduleContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);

                WriteAndSave(efCoreModelPath, moduleContent);
            }

            //todo 生成基础仓储类
            var IdbContextContentPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\I{solutionSuffix}DbContext.cs";
            IdbContextContentPath = IdbContextContentPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                .Replace("{solution}", CodeGenerateOption.SolutionName);
            if (!File.Exists(IdbContextContentPath))
            {
                var IdbContextContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using System;
using Maruko.Dependency;

namespace {solution}.EntityFrameworkCore
{
    public interface I{solutionSuffix}DbContext : IDependencyTransient, IDisposable
    {
    }
}
";
                IdbContextContent = IdbContextContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);
                WriteAndSave(IdbContextContentPath, IdbContextContent);
            }

            //todo 生成工作单元基类
            var uowPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\{solutionSuffix}EfCoreBaseUow.cs";
            uowPath = uowPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                .Replace("{solution}", CodeGenerateOption.SolutionName);
            if (!File.Exists(uowPath))
            {
                var uowContent = UowContent();
                uowContent = uowContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);
                WriteAndSave(uowPath, uowContent);
            }

            //todo 生成自定义主键仓储基类
            var repositoryPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\BaseRepositoryOfTPrimaryKey.cs";
            if (!File.Exists(repositoryPath))
            {
                var reposContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.EntityFrameworkCore.Repository;
using Maruko.EntityFrameworkCore.UnitOfWork;

namespace {solution}.EntityFrameworkCore
{
    public class BaseRepositoryOfTPrimaryKey<TEntity, TPrimaryKey> : MarukoBaseRepository<TEntity, TPrimaryKey>,
        IRepository<TEntity, TPrimaryKey>
        where TEntity : FullAuditedEntity<TPrimaryKey>
    {
        public BaseRepositoryOfTPrimaryKey(IEfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}";
                reposContent = reposContent
                    .Replace("{solution}", CodeGenerateOption.SolutionName);
                WriteAndSave(repositoryPath, reposContent);
            }


            //todo 生成int主键类型仓储基类
            var reposOfIntPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\BaseRepository.cs";
            if (!File.Exists(reposOfIntPath))
            {
                var reposOfIntContent = RepositoryContent();
                reposOfIntContent = reposOfIntContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);
                WriteAndSave(reposOfIntPath, reposOfIntContent);
            }

            //todo 生成dbcontext上下文 
            var dbContextPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\{solutionSuffix}DbContext.cs";
            dbContextPath = dbContextPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault());
            if (!File.Exists(dbContextPath))
            {
                var dbContextContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using Maruko.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;

namespace {solution}.EntityFrameworkCore
{
    public class {solutionSuffix}DbContext : BaseDbContext, I{solutionSuffix}DbContext
    {
        public {solutionSuffix}DbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //初始化默认数据

            //创建索引
        }
    }
}";
                dbContextContent = dbContextContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);
                WriteAndSave(dbContextPath, dbContextContent);
            }

            //todo 生成extension文件夹以及文件
            var extensionPath = CodeGenerateOption.Disk + "\\GenerateCode\\EntityFrameworkRepos\\Extension\\";
            if (!Directory.Exists(extensionPath))
            {
                Directory.CreateDirectory(extensionPath);
            }
            extensionPath += @"{solutionSuffix}EntityFrameworkExtension.cs";
            extensionPath = extensionPath
                .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                .Replace("{solution}", CodeGenerateOption.SolutionName);
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

using Maruko.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace {solution}.EntityFrameworkCore.Extension
{
    public static class {solutionSuffix}EntityFrameworkExtension
    {
        public static IServiceCollection Add{solutionSuffix}DbContext(this IServiceCollection serviceCollection)
        {
            var connStr = ConfigurationSetting.DefaultConfiguration.GetConnectionString(""WriteConnection"");
                serviceCollection
                    .AddDbContextPool<{solutionSuffix}DbContext>(options => { options.UseMySql(connStr); });
                return serviceCollection;
            }
        }
    }";
                extensionContent = extensionContent
                    .Replace("{solutionSuffix}", CodeGenerateOption.SolutionName.Split('.').LastOrDefault())
                    .Replace("{solution}", CodeGenerateOption.SolutionName);
                WriteAndSave(extensionPath, extensionContent);
            }
        }

        private static string UowContent()
        {
            var uowContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using System;
using System.Linq;
using log4net;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.UnitOfWork;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Maruko.Logger;
using Microsoft.EntityFrameworkCore;
namespace {solution}.EntityFrameworkCore
{
    public class BabyEfCoreBaseUow : IEfUnitOfWork
    {
        private readonly BabyDbContext _writeDbContext;
        public ILog Logger { get; }
        public {solutionSuffix}EfCoreBaseUow(BabyDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
            Logger = LogHelper.Log4NetInstance.LogFactory(typeof(EfCoreBaseUnitOfWork<>));
        }     

        public void SetModify<TEntity, TPrimaryKey>(TEntity entity)
            where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            _writeDbContext.Entry(entity).State = EntityState.Modified;
        }
        
        public void SetModify<TEntity, TPrimaryKey>(TEntity entity, string[] inCludeColums)
            where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            var dbEntityEntry = _writeDbContext.Entry(entity);
            if (!inCludeColums.Any())
                return;

            _writeDbContext.Entry(entity).State = EntityState.Modified;
            inCludeColums?.ToList().ForEach(colums =>
            {
                dbEntityEntry.OriginalValues.Properties.ToList().ForEach(property =>
                {
                    if (property.Name == colums)
                        _writeDbContext.Entry(entity).Property(property.Name).IsModified = true;
                });
            });
        }

        public void Dispose()
        {
            _writeDbContext.Dispose();
        }
        
        public virtual int Commit()
        {
            try
            {
                //这里如果没有调用过createset方法就会报错，如果没有调用认为没有任何改变直接跳出来
                if (_writeDbContext == null)
                    return 0;
                return _writeDbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Logger.Debug(nameof(DbUpdateException) + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public virtual void CommitAndRefreshChanges()
        {
            bool saveFailed;
            do
            {
                try
                {
                    if (_writeDbContext == null)
                        return;

                    _writeDbContext.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList()
                        .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
                catch (DbUpdateException ex)
                {
                    Logger.Debug(nameof(DbUpdateException) + ex.Message);
                    throw new Exception(ex.Message);
                }
            } while (saveFailed);
        }
        
        public virtual void RollbackChanges()
        {
            // set all entities in change tracker
            // as 'unchanged state'
            if (_writeDbContext != null)
                _writeDbContext.ChangeTracker.Entries()
                    .ToList()
                    .ForEach(entry =>
                    {
                        if (entry.State != EntityState.Unchanged)
                            entry.State = EntityState.Detached;
                    });
        }

        public int ExecuteCommand(string sqlCommand, ContextType contextType = ContextType.DefaultContextType,
            params object[] parameters)
        {
            return _writeDbContext.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        DbSet<TEntity> IEfUnitOfWork.CreateSet<TEntity, TPrimaryKey>(ContextType contextType)
        {
            return _writeDbContext.CreateSet<TEntity, TPrimaryKey>();
        }

        public DbSet<TEntity> WriteCreateSet<TEntity, TPrimaryKey>(ContextType contextType)
            where TEntity : FullAuditedEntity<TPrimaryKey>
        {
            return _writeDbContext.CreateSet<TEntity, TPrimaryKey>();
        }
    }
}";
            return uowContent;
        }

        private static string RepositoryContent()
        {
            var reposContent = @"
//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：{templateTime}  
//版本1.0
//===================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Maruko.Domain.Entities.Auditing;
using Maruko.Domain.Repositories;
using Maruko.EntityFrameworkCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace {solution}.EntityFrameworkCore
{
    public class BaseRepository<TEntity>: BaseRepositoryOfTPrimaryKey<TEntity, int>,
        IRepository<TEntity>
        where TEntity : FullAuditedEntity<int>
    {
        public BaseRepository(IEfUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<TEntity> PageSearch(out int total, Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, int>> orderSelector = null, int pageIndex = 1,
            int pageMax = 20)
        {
            var query = GetAll().AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);

            query = orderSelector != null
                ? query.OrderByDescending(orderSelector)
                : query.OrderByDescending(item => item.Id);

            total = query.Count();

            var result = query
                .Skip(pageMax * (pageIndex - 1))
                .Take(pageMax)
                .ToList();
            return result;
        }

        public List<dynamic> PageSearchSelector(out int total, Expression<Func<TEntity, dynamic>> selector, Expression<Func<TEntity, bool>> predicate = null,
            Expression<Func<TEntity, int>> orderSelector = null, int pageIndex = 1, int pageMax = 20)
        {
            var query = GetAll().AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);

            query = orderSelector != null
                ? query.OrderByDescending(orderSelector)
                : query.OrderByDescending(item => item.Id);

            total = query.Count();

            var result = query
                .Select(selector)
                .Skip(pageMax * (pageIndex - 1))
                .Take(pageMax)
                .ToList();
            return result;
        }

        public bool BatchInsert(List<TEntity> entities)
        {
            entities.ForEach(item => { WriteGetSet().Add(item); });
            var result = UnitOfWork.Commit();
            return result != 0;
        }

        public bool BatchUpdate(List<TEntity> entities)
        {
            entities.ForEach(item => { _unitOfWork.SetModify<TEntity, int>(item); });
            var result = UnitOfWork.Commit();
            return result != 0;
        }

        public TEntity UpdateNotCommit(TEntity entity)
        {
            WriteGetSet().Add(entity);
            return entity;
        }

        public TEntity InsertNotCommit(TEntity entity)
        {
            _unitOfWork.SetModify<TEntity, int>(entity);
            return entity;
        }
    }
}
";
            return reposContent;
        }
    }
}
