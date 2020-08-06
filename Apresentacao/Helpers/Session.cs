using Microsoft.AspNetCore.Http;

namespace Apresentacao.Helpers
{
    public class Session
    {
        private static IHttpContextAccessor _accessor;

        public Session()
        {
            _accessor = new HttpContextAccessor();
        }

        public void Create<T>(string key, T objeto)
        {
            string jsonValue = new Serializer().SetObject<T>(objeto);
            _accessor.HttpContext.Session.SetString(key, jsonValue);
        }

        public T GetObject<T>(string key)
        {
            string json = null;
            if (!string.IsNullOrWhiteSpace(key))
                json = _accessor.HttpContext.Session.GetString(key);
            return new Serializer().GetObject<T>(json);
        }
    }
}
