using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Cbb.Application
{
    [EnableCors("cors")]
    [Route("api/v1/allcountryoilprices/")]
    public class AllCountryOilPriceController : BaseCurdController<AppAllCountryOilPrice, AppAllCountryOilPriceDTO>
    {
        public AllCountryOilPriceController(ICurdAppService<AppAllCountryOilPrice, AppAllCountryOilPriceDTO> curd) :
            base(curd)
        {
        }
    }
}