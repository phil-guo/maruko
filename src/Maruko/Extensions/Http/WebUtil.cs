using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Maruko.Core.Extensions.Http
{
 public  class WebUtil
    {
		public static string BuildQuery(IDictionary<string, object> parameters, string charset)
		{
			var postData = new StringBuilder();
			var hasParam = false;

			using (var dem = parameters.GetEnumerator())
			{
				while (dem.MoveNext())
				{
					var name = dem.Current.Key;
					var value = dem.Current.Value;
					// 忽略参数名或参数值为空的参数
					if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value?.ToString()))
					{
						if (hasParam)
							postData.Append("&");

						postData.Append(name);
						postData.Append("=");

						var encodedValue = HttpUtility.UrlEncode(value?.ToString(), Encoding.GetEncoding(charset));

						postData.Append(encodedValue);
						hasParam = true;
					}
				}

				return postData.ToString();
			}
		}
	}
}
