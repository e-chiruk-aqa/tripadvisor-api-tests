namespace TripadvisorApiFramework.Helpers.Http.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BodyAttribute : HttpRequestItemAttribute
    {
        public BodyAttribute()
        {
            base.IgnoreNullValue = false;
        }
    }
}
