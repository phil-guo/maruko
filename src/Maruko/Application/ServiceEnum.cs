namespace Maruko.Application
{
    public enum ServiceEnum
    {
        Failure = 0,

        Success,

        ValidateParam,        

        /// <summary>
        /// token验证失败
        /// </summary>
        TokenValidate = 401,
    }
}
