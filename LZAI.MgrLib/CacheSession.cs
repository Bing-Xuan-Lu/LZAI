using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LZAI.MgrLib
{
    /// <summary>
    /// 使用MemoryCache配合Cookie模擬Session(timeout時間20分鐘)
    /// </summary>
    static class CacheSession
    {
        private const string COOKIE_KEY = "CacheSessionId";
        private const int SessionTimeoutMinutes = 20;

        private static HttpContext CurrentContext
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("HttpContext.Current is null");
                return HttpContext.Current;
            }
        }

        public static string SessionId
        {
            get
            {
                var cookie = CurrentContext.Request.Cookies[COOKIE_KEY];
                if (cookie != null) return cookie.Value;
                //set session id cookie
                var sessId = NewShortGuid();
                CurrentContext.Response.SetCookie(new HttpCookie(COOKIE_KEY, sessId));
                return sessId;
            }
        }

        private static string NewShortGuid()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace('/', '_').Replace('+', '-').Substring(0, 22);
        }

        public static SessionObject Session
        {
            get
            {
                var cache = MemoryCache.Default;
                var sessId = SessionId;
                if (!cache.Contains(sessId))
                {
                    cache.Add(sessId, new SessionObject(sessId), new CacheItemPolicy()
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(SessionTimeoutMinutes)
                    });
                }
                return (SessionObject)cache[sessId];
            }
        }

        public class SessionObject
        {
            public string SessionId;
            Dictionary<string, object> items = new Dictionary<string, object>();
            public SessionObject(string sessId)
            {
                SessionId = sessId;
            }
            public object this[string key]
            {
                get
                {
                    lock (items)
                    {
                        if (items.ContainsKey(key)) return items[key];
                        return null;
                    }
                }
                set
                {
                    lock (items)
                    {
                        items[key] = value;
                    }
                }
            }
        }
    }
}
