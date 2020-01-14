using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Util
{
	public static class Json
	{
		public static Dictionary<string, string> GetDictionaryFromText(JObject items)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach(KeyValuePair<string, JToken> o in items){
				result.Add(o.Key, o.Value.ToString());
			}

			return result;
		}
	}
}
