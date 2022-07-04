using System.Threading.Tasks;
using Maruko.Core.Application;
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
        private readonly IAllCountryOilPriceService _allCountryOilPriceService;
        public AllCountryOilPriceController(IAllCountryOilPriceService curd) :
            base(curd)
        {
            _allCountryOilPriceService = curd;
        }

        [HttpPost("spiderOilAsync")]
        public async Task<AjaxResponse<object>> SpiderOilAsync()
        {
            await _allCountryOilPriceService.SpiderOil();
            return new AjaxResponse<object>( "ok");
        }
    }
}