using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GeneBlood.Web.Admin.Common
{
    public class CacheHelp
    {
        #region 缓存处理
        public static void AddSlidingPxpireCache(string key, object obj, int slidingExpireMinutes)
        {
            HttpRuntime.Cache.Add(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                new TimeSpan(0, slidingExpireMinutes, 0), System.Web.Caching.CacheItemPriority.Default,
                null);
        }
        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">保存到缓存中的对象</param>
        /// <param name="absoluteExpireMinutes">设置缓存时间(单位:分钟)</param>
        public static void AddAbsoluteExpireCache(string key, object obj, int absoluteExpireMinutes)
        {
            HttpRuntime.Cache.Add(key, obj, null,
                System.DateTime.Now.AddMinutes(absoluteExpireMinutes),
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default,
                null);
        }

        public static void SetSlidingPxpireCache(string key, object obj, int slidingExpireMinutes)
        {
            HttpRuntime.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                new TimeSpan(0, slidingExpireMinutes, 0), System.Web.Caching.CacheItemPriority.Default,
                null);
        }

        public static void SetAbsoluteExpireCache(string key, object obj, int absoluteExpireMinutes)
        {
            HttpRuntime.Cache.Insert(key, obj, null,
                System.DateTime.Now.AddMinutes(absoluteExpireMinutes),
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default,
                null);
        }
        /// <summary>
        /// 清除缓存
        /// </summary>
        /// <param name="key">要清除缓存的 key</param>
        public static void ClearCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T">缓存中的数据类型，设置缓存和取缓存时类型保持一致即可</typeparam>
        /// <param name="key">缓存 key</param>
        /// <returns></returns>
        public static T GetCache<T>(string key) where T : class
        {
            return HttpRuntime.Cache.Get(key) as T;
        }
        #endregion
    }
}
