namespace Maruko.CodeGeneration.Options
{
    public class CodeGenerateOption
    {
        /// <summary>
        /// 生成的所在 磁盘
        /// 格式：D:
        /// </summary>
        public static string Disk { get; set; } = "D:";

        /// <summary>
        /// 解决方案名称
        /// 格式:.
        /// </summary>
        public static string SolutionName { get; set; } = "Cbb.Application";//Maruko.Zero  Maruko.Dynamic.Config
    }
}
