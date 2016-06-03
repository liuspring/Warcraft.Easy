﻿using System;
using System.Web;

namespace Common
{
	/// <summary>
	/// 缓存相关的操作类
	/// </summary>
	public class DataCache
	{
		/// <summary>
		/// 获取当前应用程序指定CacheKey的Cache值
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <returns></returns>
		public static object GetCache(string cacheKey)
		{
			var objCache = HttpRuntime.Cache;
			return objCache[cacheKey];
		}

		/// <summary>
		/// 设置当前应用程序指定CacheKey的Cache值
		/// </summary>
		/// <param name="cacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string cacheKey, object objObject)
		{
			var objCache = HttpRuntime.Cache;
			objCache.Insert(cacheKey, objObject);
		}

	    /// <summary>
	    /// 设置当前应用程序指定CacheKey的Cache值
	    /// </summary>
	    /// <param name="cacheKey"></param>
	    /// <param name="objObject"></param>
	    /// <param name="absoluteExpiration"> </param>
	    /// <param name="slidingExpiration"> </param>
	    public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration,TimeSpan slidingExpiration )
		{
			var objCache = HttpRuntime.Cache;
			objCache.Insert(cacheKey, objObject,null,absoluteExpiration,slidingExpiration);
		}
	}
}
