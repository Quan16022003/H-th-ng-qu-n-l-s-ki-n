using System.Text.RegularExpressions;

namespace Web.Utils.UrlTransform
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) return null;
            return Regex.Replace(value?.ToString()!, "([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}
