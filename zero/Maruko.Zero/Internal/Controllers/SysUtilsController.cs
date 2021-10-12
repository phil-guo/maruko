using System;
using System.Collections.Generic;
using System.IO;
using Maruko.Core.Application;
using Maruko.Core.Web;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Maruko.Zero
{
    [ApiController]
    [EnableCors("cors")]
    [Route("api/v1/utils/")]
    public class SysUtilsController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;

        public SysUtilsController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("uploadImages"), SwaggerFileUpload]
        public AjaxResponse<object> UploadImage()
        {
            var files = Request.Form.Files;
            var filePaths = new List<string>();
            foreach (var file in files)
            {
                var filePath = $"/uploads/{ DateTime.Now:yyyy}/{ DateTime.Now:yyyyMM}/{ DateTime.Now:yyyyMMdd}/";
                //获取当前web目录
                var webRootPath = _hostEnvironment.ContentRootPath;
                if (!Directory.Exists(webRootPath + filePath))
                {
                    Directory.CreateDirectory(webRootPath + filePath);
                }

                if (file == null)
                    continue;

                // 文件后缀
                var fileExtension = Path.GetExtension(file.FileName);

                //判断后缀是否是图片
                const string fileSuffix = ".gif|.jpg|.jpeg|.png";
                if (fileExtension == null)
                {
                    return new AjaxResponse<object>("上传的文件没有后缀"); ;
                }
                if (fileSuffix.IndexOf(fileExtension.ToLower(), StringComparison.Ordinal) <= -1)
                {
                    return new AjaxResponse<object>("请上传jpg、png、gif格式的图片");
                }


                var strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff"); //取得时间字符串
                var strRan = Convert.ToString(new Random().Next(100, 999)); //生成三位随机数
                var saveName = strDateTime + strRan + fileExtension;

                //插入图片数据                 
                using (FileStream fs = System.IO.File.Create(webRootPath + filePath + saveName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
                var fileAllPath = (filePath + saveName);
                filePaths.Add(fileAllPath);
            }

            return new AjaxResponse<object>(JsonConvert.SerializeObject(filePaths), "上传成功");
        }
    }
}
