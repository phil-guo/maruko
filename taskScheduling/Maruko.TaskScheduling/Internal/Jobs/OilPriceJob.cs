using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Autofac;
using Maruko.Core.Extensions;
using Maruko.Core.FreeSql.Internal.Repos;
using Maruko.Core.ObjectMapping;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Maruko.TaskScheduling
{
    public class OilPriceJob : IJob
    {
        private readonly ILogger<OilPriceJob> _logger;
        private readonly IFreeSqlRepository<TaskScheduling> _taskSchedule;
        private readonly IObjectMapper _objectMapper;
        private readonly IFreeSqlRepository<AllCountryOilPrice> _oil;


        public OilPriceJob()
        {
            _logger = ServiceLocator.Current.Resolve<ILogger<OilPriceJob>>();
            _taskSchedule = ServiceLocator.Current.Resolve<IFreeSqlRepository<TaskScheduling>>();
            _objectMapper = ServiceLocator.Current.Resolve<IObjectMapper>();
            _oil = ServiceLocator.Current.Resolve<IFreeSqlRepository<AllCountryOilPrice>>();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var taskId = context.JobDetail.JobDataMap.GetLongValue("objectId");
            // var task = await _taskSchedule
            //     .GetAll()
            //     .Select<TaskScheduling>()
            //     .Where(item => item.Id == taskId)
            //     .ToOneAsync();
            var listOil = new List<OilDTO>();
            OilDTO dto = null;
            var htmlParser = new HtmlParser();
            var htmlDoc = GetHtmlByUrl("http://www.qiyoujiage.com/");
            //HTML 解析成 IDocument
            var dom = htmlParser.ParseDocument(htmlDoc);

            var data = dom.All.FirstOrDefault(_ => _.Id == "left");

            var oilText = data?.Children.FirstOrDefault()?.FirstChild?.Text();
            // dto.NextNotify = oilText;
            var oilData = dom.QuerySelectorAll("ul.ylist").FirstOrDefault();
            List<dynamic> oilPrice = null;
            var i = 0;
            foreach (var oil in oilData.Children)
            {
                if (oil.ClassName == "t")
                {
                    dto = new OilDTO();
                    dto.NextNotify = oilText;
                    dto.CityName = oil.TextContent;
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
            await _oil.GetAll().Insert(_objectMapper.Map<List<AllCountryOilPrice>>(listOil)).ExecuteAffrowsAsync();
            await _taskSchedule.GetAll()
                .Update<TaskScheduling>(taskId)
                .Set(item => item.OverTime, DateTime.Now)
                .ExecuteAffrowsAsync();
        }

        public string GetHtmlByUrl(string url)
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
                using (System.IO.StreamReader reader =
                    new System.IO.StreamReader(respStream, Encoding.GetEncoding("UTF-8")))
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