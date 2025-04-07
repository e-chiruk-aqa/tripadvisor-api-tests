using Newtonsoft.Json;
using System.Xml.Serialization;
using TripadvisorApiFramework.Enums;
using TripadvisorApiFramework.Helpers.Http.RequestResolvers;

namespace TripadvisorApiFramework.Helpers.Http
{
    public abstract class HttpRequest
    {
        [XmlIgnore]
        [JsonIgnore]
        private string _content;

        [XmlIgnore]
        [JsonIgnore]
        public ContentType BodyContentType { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public virtual string Content
        {
            get
            {
                return GetBody();
            }
            set
            {
                _content = value;
            }
        }

        public override string ToString()
        {
            string text = "";
            Dictionary<string, string> urlParameters = GetUrlParameters();
            if (urlParameters.Count > 0)
            {
                text += "Parameters \n";
                text += DictionaryToJson(urlParameters);
                text += "\n";
            }

            Dictionary<string, string> headers = GetHeaders();
            if (headers.Count > 0)
            {
                text += "Headers \n";
                text += DictionaryToJson(headers);
                text += "\n";
            }

            Dictionary<string, string> cookies = GetCookies();
            if (cookies.Count > 0)
            {
                text += "Cookies \n";
                text += DictionaryToJson(cookies);
                text += "\n";
            }

            string body = GetBody();
            if (body != null)
            {
                text += "Body \n";
                text += body;
                text += "\n";
            }

            return text;
        }

        protected internal virtual Dictionary<string, string> GetUrlParameters()
        {
            return RequestResolver.GetRequestParametersResolver(this).GetUrlParameters();
        }

        protected internal virtual Dictionary<string, string> GetCookies()
        {
            return RequestResolver.GetRequestParametersResolver(this).GetCookies();
        }

        protected internal virtual Dictionary<string, string> GetHeaders()
        {
            return RequestResolver.GetRequestParametersResolver(this).GetHeaders();
        }

        protected internal virtual string GetBody()
        {
            if (_content != null)
            {
                return _content;
            }

            return RequestResolver.GetRequestParametersResolver(this).RequestBodyToString();
        }

        private string DictionaryToJson(Dictionary<string, string> dict)
        {
            return JsonConvert.SerializeObject(dict);
        }
    }
}
