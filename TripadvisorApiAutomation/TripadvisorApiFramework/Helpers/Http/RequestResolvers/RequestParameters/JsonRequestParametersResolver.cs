using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TripadvisorApiFramework.Helpers.Http.Attributes;

namespace TripadvisorApiFramework.Helpers.Http.RequestResolvers.RequestParameters
{
    internal class JsonRequestParametersResolver : RequestParametersResolver
    {
        private class DynamicContractResolver : DefaultContractResolver
        {
            private readonly string[] _targetProps;

            private readonly Type _typeToBeConfigured;

            public DynamicContractResolver(Type typeToBeConfigured, params string[] targetProps)
            {
                _targetProps = targetProps;
                _typeToBeConfigured = typeToBeConfigured;
            }

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
                List<JsonProperty> list2 = new List<JsonProperty>();
                if (type == _typeToBeConfigured)
                {
                    foreach (JsonProperty item in list)
                    {
                        if (_targetProps.Contains<string>(item.PropertyName))
                        {
                            list2.Add(item);
                        }
                        else
                        {
                            string text = HttpRequestItemAttribute.ConvertRealPropNameToAssigned(type, item.PropertyName);
                            if (_targetProps.Contains(text))
                            {
                                item.PropertyName = text;
                                list2.Add(item);
                            }
                        }
                    }

                    return list2;
                }

                return list;
            }
        }

        public JsonRequestParametersResolver(HttpRequest request)
            : base(request)
        {
        }

        public override string RequestBodyToString()
        {
            IEnumerable<string> source = from pair in HttpRequestItemsToDictionary<BodyAttribute>()
                                         select pair.Key;
            string text = JsonConvert.SerializeObject(_request, Formatting.None, new JsonSerializerSettings
            {
                ContractResolver = new DynamicContractResolver(_request.GetType(), source.ToArray())
            });
            return (text == "{}") ? null : text;
        }
    }
}
