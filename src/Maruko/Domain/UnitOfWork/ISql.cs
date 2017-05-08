namespace Maruko.Domain.UnitOfWork
{
    public interface ISql
    {
        ///  <summary>
        ///  执行增删改sql命令到底层持久存储
        ///  Execute arbitrary command into underliying persistence store
        ///  </summary>
        /// <param name="contextType"></param>
        /// <param name="sqlCommand">
        ///  Command to execute
        ///  <example>
        ///  SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        ///  </example>
        /// </param>
        ///  <param name="parameters">A vector of parameters values</param>
        ///  <returns>The number of affected records</returns>
        int ExecuteCommand( string sqlCommand, ContextType contextType = ContextType.DefaultContextType, params object[] parameters);
    }
}
