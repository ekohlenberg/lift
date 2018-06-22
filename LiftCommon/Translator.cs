using System;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace LiftCommon
{
    public class GoogleAjaxResponse<T>
    {
        public T responseData = default(T);
    }

    public class TranslationResponse
    {
        public string translatedText = string.Empty;
        public object responseDetails = null;
        public HttpStatusCode responseStatus = HttpStatusCode.OK;
    }

    public class Translator
    {
        private static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public static string TranslateText(string inputText, string fromLanguage, string toLanguage, string referrer, string key)
        {
            string requestUrl = string.Format("http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&q={0}&langpair={1}|{2}",
                System.Web.HttpUtility.UrlEncode(inputText),
                fromLanguage.ToLowerInvariant(),
                toLanguage.ToLowerInvariant()            
                );

            if (!String.IsNullOrEmpty(key))
            {
                requestUrl = string.Format(requestUrl + "&key={3}", key);
            }

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);

            if (!String.IsNullOrEmpty(referrer))
            {
                req.Referer = referrer;
            }

            try
            {
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                string responseJson = new StreamReader(res.GetResponseStream()).ReadToEnd();

                GoogleAjaxResponse<TranslationResponse> translation = serializer.Deserialize<GoogleAjaxResponse<TranslationResponse>>(responseJson);

                if (translation != null && translation.responseData != null && translation.responseData.responseStatus == HttpStatusCode.OK)
                {
                    return translation.responseData.translatedText;
                }
                else
                {
                    return string.Empty;
                }
            }

            catch
            {
                return string.Empty;
            }
        }

        public static string TranslateText(string inputText, CultureInfo fromLanguage, CultureInfo toLanguage, string referrer, string key)
        {
            return TranslateText(inputText, fromLanguage.TwoLetterISOLanguageName, toLanguage.TwoLetterISOLanguageName, referrer, key);
        }

        public static string TranslateText(string inputText, string fromLanguage, string toLanguage)
        {
            return TranslateText(inputText, fromLanguage, toLanguage, string.Empty, string.Empty);
        }

    }
}
