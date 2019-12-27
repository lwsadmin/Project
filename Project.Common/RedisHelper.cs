﻿using System;
using System.Collections.Generic;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Threading;
//using YJT.Web.lib;
//using YJT.Common.Log;

namespace Project.Common
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public class RedisHelper
    {
        //单例模式
        public static RedisCommon Zero { get { return new RedisCommon(); } }
        public static RedisCommon One { get { return new RedisCommon(1); } }
        public static RedisCommon Two { get { return new RedisCommon(2); } }
        public static RedisCommon Three { get { return new RedisCommon(3); } }
    }

    /// <summary>
    /// Redis操作类
    /// 老版用的是ServiceStack.Redis  
    /// .Net Core使用StackExchange.Redis的nuget包
    /// </summary>
    public class RedisCommon
    {
        //redis数据库连接字符串
        private string _conn = UtilConf.Configuration["Redis"] ?? "127.0.0.1:6379";
        private int _db = 0;

        //静态变量 保证各模块使用的是不同实例的相同链接
        private static ConnectionMultiplexer connection;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RedisCommon() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        /// <param name="connectStr"></param>
        public RedisCommon(int db)
        {
            _db = db;
        }
        /// <summary>
        /// 缓存数据库，数据库连接
        /// </summary>
        public ConnectionMultiplexer CacheConnection
        {
            get
            {
                try
                {
                    if (connection == null || !connection.IsConnected)
                    {
                        connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_conn)).Value;
                    }
                }
                catch (Exception ex)
                {
                    // Log.Debug("RedisHelper->CacheConnection 出错\r\n", ex.Message.ToString());
                    return null;
                }
                return connection;
            }
        }

        /// <summary>
        /// 缓存数据库
        /// </summary>
        public IDatabase CacheRedis => CacheConnection.GetDatabase(_db);


        #region --KEY/VALUE存取--
        /// <summary>
        /// 单条存值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StringSet(string key, string value)
        {
            return CacheRedis.StringSet(key, value);
        }

        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <param name="value">保存的值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = default(TimeSpan?))
        {
            return CacheRedis.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="arr">key</param>
        /// <returns></returns>
        public bool StringSet(KeyValuePair<RedisKey, RedisValue>[] arr)
        {
            return CacheRedis.StringSet(arr);
        }

        /// <summary>
        /// 批量存值
        /// </summary>
        /// <param name="keysStr">key</param>
        /// <param name="valuesStr">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StringSetMany(string[] keysStr, string[] valuesStr)
        {
            var count = keysStr.Length;
            var keyValuePair = new KeyValuePair<RedisKey, RedisValue>[count];
            for (int i = 0; i < count; i++)
            {
                keyValuePair[i] = new KeyValuePair<RedisKey, RedisValue>(keysStr[i], valuesStr[i]);
            }

            return CacheRedis.StringSet(keyValuePair);
        }

        /// <summary>
        /// 保存一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool SetStringKey<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return CacheRedis.StringSet(key, json, expiry);
        }

        /// <summary>
        /// 追加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void StringAppend(string key, string value)
        {
            ////追加值，返回追加后长度
            long appendlong = CacheRedis.StringAppend(key, value);
        }

        /// <summary>
        /// 获取单个key的值
        /// </summary>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        public RedisValue GetStringKey(string key)
        {
            return CacheRedis.StringGet(key);
        }

        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>System.String.</returns>
        public string StringGet(string key)
        {
            try
            {
                return CacheRedis.StringGet(key);
            }
            catch (Exception ex)
            {
                //Log.Debug("RedisHelper->StringGet 出错\r\n", ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取多个Key
        /// </summary>
        /// <param name="listKey">Redis Key集合</param>
        /// <returns></returns>
        public RedisValue[] GetStringKey(List<RedisKey> listKey)
        {
            return CacheRedis.StringGet(listKey.ToArray());
        }

        /// <summary>
        /// 批量获取值
        /// </summary>
        public string[] StringGetMany(string[] keyStrs)
        {
            var count = keyStrs.Length;
            var keys = new RedisKey[count];
            var addrs = new string[count];

            for (var i = 0; i < count; i++)
            {
                keys[i] = keyStrs[i];
            }
            try
            {

                var values = CacheRedis.StringGet(keys);
                for (var i = 0; i < values.Length; i++)
                {
                    addrs[i] = values[i];
                }
                return addrs;
            }
            catch (Exception ex)
            {
                // Log.Debug("RedisHelper->StringGetMany 出错\r\n", ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetStringKey<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(CacheRedis.StringGet(key));
        }
        #endregion


        #region --删除设置过期--
        /// <summary>
        /// 删除单个key
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns>是否删除成功</returns>
        public bool KeyDelete(string key)
        {
            return CacheRedis.KeyDelete(key);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys">rediskey</param>
        /// <returns>成功删除的个数</returns>
        public long KeyDelete(RedisKey[] keys)
        {
            return CacheRedis.KeyDelete(keys);
        }

        /// <summary>
        /// 判断key是否存储
        /// </summary>
        /// <param name="key">redis key</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return CacheRedis.KeyExists(key);
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="key">就的redis key</param>
        /// <param name="newKey">新的redis key</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            return CacheRedis.KeyRename(key, newKey);
        }

        /// <summary>
        /// 删除hasekey
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HaseDelete(RedisKey key, RedisValue hashField)
        {
            return CacheRedis.HashDelete(key, hashField);
        }

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool HashRemove(string key, string dataKey)
        {
            return CacheRedis.HashDelete(key, dataKey);
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void SetExpire(string key, DateTime datetime)
        {
            CacheRedis.KeyExpire(key, datetime);
        }
        #endregion


        #region 有序集合 并发控制
        #endregion

        #region 发布/订阅消息通知
        public long SendNoticBy(string ChannelName, string msg)
        {
            ISubscriber sub = connection.GetSubscriber();
            //订阅 ChannelName 频道
            //sub.Subscribe(ChannelName, new Action<RedisChannel, RedisValue>((channel, message) =>
            //{
            //    //  Console.WriteLine("Channel1" + " 订阅收到消息：" + message);
            //}));
            return sub.Publish(ChannelName, msg);//向频道 ChannelName 发送信息
                                                 // sub.Unsubscribe(ChannelName);//取消订阅
        }
        public void RedisSub(string subChannael)
        {
            //Actioncallback
            ISubscriber sub = connection.GetSubscriber();
            sub.Subscribe(subChannael, (channel, message) =>
            {
                Console.WriteLine(channel + ":" + message);
            });
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channelName">默认为空 为空则取消所有订阅，可指定订阅</param>
        public void Unsubscribe(string channelName = "")
        {

            ISubscriber sub = connection.GetSubscriber();
            if (string.IsNullOrEmpty(channelName))
                sub.UnsubscribeAll();
            else
                sub.Unsubscribe(channelName);

        }
        #endregion


        public void Lock()
        {
            RedisValue token = Environment.MachineName;
            //lock_key表示的是redis数据库中该锁的名称，不可重复。 分布式锁
            //token用来标识谁拥有该锁并用来释放锁。
            //TimeSpan表示该锁的有效时间。10秒后自动释放，避免死锁。
            if (CacheRedis.LockTake("lock_key", token, TimeSpan.FromSeconds(10)))
            {
                try
                {
                    //TODO:开始做你需要的事情
                    Thread.Sleep(5000);
                }
                finally
                {
                    CacheRedis.LockRelease("lock_key", token);//释放锁
                }
            }
        }
    }
}