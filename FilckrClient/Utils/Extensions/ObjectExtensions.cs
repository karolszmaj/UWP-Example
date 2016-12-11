using Newtonsoft.Json;

namespace FilckrClient.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T @object)
        {
            var serialized = JsonConvert.SerializeObject(@object);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
