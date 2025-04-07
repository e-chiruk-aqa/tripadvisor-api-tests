using System.Reflection;

namespace TripadvisorApiFramework.Helpers
{
    public static class AttributeHelper
    {
        public static TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo) where TAttribute : Attribute
        {
            return (TAttribute)memberInfo.GetCustomAttributes(typeof(TAttribute), inherit: false).SingleOrDefault();
        }

        public static bool AttributeIsApplied<TAttribute>(MemberInfo memberInfo) where TAttribute : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit: false).Any();
        }
    }
}
