//===================================================================================
//此代码由代码生成器自动生成      
//对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//===================================================================================
//作者:simple              
//创建时间：09/02/2021  
//版本1.0
//===================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Cbb.Application.Internal.Domain;
using Maruko.Core.FreeSql.Internal.AppService;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cbb.Application
{
    public class AllCountryOilPriceService : CurdAppService<AppAllCountryOilPrice, AppAllCountryOilPriceDTO>,
        IAllCountryOilPriceService
    {
        private readonly ILogger<AllCountryOilPriceService> _logger;
        private readonly IFreeSqlRepository<AppOilTime> _oilTime;

        public AllCountryOilPriceService(IObjectMapper objectMapper,
            IFreeSqlRepository<AppAllCountryOilPrice> repository,
            ILogger<AllCountryOilPriceService> logger,
            IFreeSqlRepository<AppOilTime> oilTime
            ) : base(objectMapper, repository)
        {
            _logger = logger;
            _oilTime = oilTime;
        }


        public async Task SpiderOil()
        {
            var time = DateTime.Now;
            var oilTimes = await _oilTime
                .GetAll()
                .Select<AppOilTime>()
                .Where(item => item.Year == time.Year)
                .OrderBy(item => item.Sort)
                .ToListAsync();
            var oilTime = oilTimes.FirstOrDefault(item => item.IsNextNotify);


            if (time.ToString("yyyy-MM-dd") != oilTime?.Time.ToString("yyyy-MM-dd"))
                return;

            var nextSort = oilTime.Sort + 1;
            var nextOilTime = oilTimes.FirstOrDefault(_ => _.Sort == nextSort);
            if (nextOilTime == null)
                return;

            var listOil = new List<OilDTO>();
            OilDTO dto = new OilDTO();
            var htmlParser = new HtmlParser();
            var htmlDoc = GetHtmlByUrl("http://www.qiyoujiage.com/");
            //HTML 解析成 IDocument
            var dom = htmlParser.ParseDocument(htmlDoc);

            //var data = dom.All.FirstOrDefault(_ => _.Id == "left");
            //var oilText = data?.Children.FirstOrDefault()?.FirstChild?.Text();
            // dto.NextNotify = oilText;
            var oilData = dom.QuerySelectorAll("ul.ylist").FirstOrDefault();
            List<dynamic> oilPrice = new List<dynamic>();
            var i = 0;
            foreach (var oil in oilData.Children)
            {
                if (oil.ClassName == "t")
                {
                    dto = new OilDTO
                    {
                        NextNotify = $"{nextOilTime.Time:yyyy-MM-dd},0时调整",
                        CityName = oil.TextContent
                    };
                    i = 0;
                    oilPrice = new List<dynamic>();
                }
                else if (string.IsNullOrEmpty(oil.ClassName))
                {
                    i += 1;
                    if (i == 1)
                    {
                        oilPrice.Add(new
                        {
                            name = "92#",
                            price = oil.TextContent
                        });
                    }

                    if (i == 2)
                    {
                        oilPrice.Add(new
                        {
                            name = "95#",
                            price = oil.TextContent
                        });
                    }

                    if (i == 3)
                    {
                        oilPrice.Add(new
                        {
                            name = "98#",
                            price = oil.TextContent
                        });
                        dto.PriceJson = JsonConvert.SerializeObject(oilPrice);
                        listOil.Add(dto);
                    }

                    if (i == 4)
                        continue;
                }
            }
            await Table
                .GetAll()
                .Insert(ObjectMapper.Map<List<AppAllCountryOilPrice>>(listOil))
                .ExecuteAffrowsAsync();

            
            nextOilTime.IsNextNotify = true;
            oilTime.IsNextNotify = false;

            var newOilTimes = new List<AppOilTime>()
            {
                oilTime,
                nextOilTime
            };
            _oilTime.BatchUpdate(newOilTimes);
        }

        private string GetHtmlByUrl(string url)
        {
            try
            {
                System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
                wRequest.ContentType = "text/html; charset=gb2312";

                wRequest.Method = "get";
                wRequest.UseDefaultCredentials = true;
                // Get the response instance.
                var task = wRequest.GetResponseAsync();
                System.Net.WebResponse wResp = task.Result;
                System.IO.Stream respStream = wResp.GetResponseStream();
                //dy2018这个网站编码方式是GB2312,
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return string.Empty;
            }
        }
    }
}