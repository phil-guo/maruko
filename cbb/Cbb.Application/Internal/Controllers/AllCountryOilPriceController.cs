using System.Threading.Tasks;
using Maruko.Core.Application;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("getCityOilPrice")]
        [AllowAnonymous]
        public async Task<object> GetCityOilPrice(string cityName)
        {
            return Ok(await _allCountryOilPriceService.GetCityOilPrice(cityName));
        }

        [HttpPost("spiderOilAsync")]
        [AllowAnonymous]
        public async Task<AjaxResponse<object>> SpiderOilAsync()
        {
            await _allCountryOilPriceService.SpiderOil();
            return new AjaxResponse<object>( "ok");
        }
    }
}