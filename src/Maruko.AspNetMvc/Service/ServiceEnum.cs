using System;
using System.Collections.Generic;
using System.Text;

namespace Maruko.AspNetMvc.Service
{
    public enum ServiceEnum
    {
        Failure = 0,

        Success,

        ValidateParam,

        /// <summary>
        /// 补偿失败
        /// </summary>
        CompensationFailure,

        /// <summary>
        /// 添加冻结订单失败
        /// </summary>
        AddFrozenFailure,

        /// <summary>
        /// 账期转账失败
        /// </summary>
        TransferFailure,

        /// <summary>
        /// token验证失败
        /// </summary>
        TokenValidate = 401,

        /// <summary>
        /// 商户是否审核
        /// </summary>
        IsMerchantAudited=100
    }
}
