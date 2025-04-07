namespace TripadvisorApiFramework.Helpers.Http.Attributes
{
    public abstract class HttpRequestItemAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IgnoreNullValue { get; set; } = true;


        internal static string ConvertRealPropNameToAssigned(Type targetType, string realPropName)
        {
            return AttributeHelper.GetAttribute<HttpRequestItemAttribute>(targetType.GetProperty(realPropName))?.Name ?? realPropName;
        }
    }
}
