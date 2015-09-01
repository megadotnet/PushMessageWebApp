using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Message.WebAPI.Services
{
    /// <summary>
    /// CacheManager
    /// </summary>
    /// <see cref="https://github.com/ServiceStack/ServiceStack.Redis/wiki/IRedisTypedClient"/>
    /// <seealso cref="http://www.codeproject.com/Tips/825904/ASP-NET-WebApi-Use-Redis-as-CacheManager"/>
    /// <example>
    /// <code>
    /// <![CDATA[
    ///    public class RedisController : ApiController 
    ///{ 
    ///    // GET: api/Redis/name 
    ///    public int Get(string name) 
    ///    { 
    ///        RedisClient client = new RedisClient("localhost", 6379);       
    ///        CacheManager cacheManager = new CacheManager(client); 
    ///        Person person = cacheManager.Get<Person>(name); 
    ///        return person.Age; 
    ///    } 
    ///    // POST: api/Redis 
    ///    public void Post(int age, string name) 
    ///    { 
    ///        RedisClient client = new RedisClient("localhost", 6379); 
           
    ///        CacheManager cacheManager = new CacheManager(client); 
    ///        Person person = new Person(); 
    ///        person.Age = age; 
    ///        person.Name = name; 
    ///        cacheManager.Set(person); 
    ///    }         
    ///}   
    ///public class Person 
    ///{ 
    ///    public int Age { get; set; } 
    ///    public string Name { get; set; } 
    ///}
    /// ]]>
    /// </code>
    /// </example>
    public class CacheManager
    {
        /// <summary>
        /// The _redis client
        /// </summary>
        private readonly IRedisClient _redisClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheManager"/> class.
        /// </summary>
        /// <param name="redisClient">The redis client.</param>
        public CacheManager(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Get<T>(string id)
        {
            using (var typedclient = new RedisClient())
            {
                var redis = _redisClient.As<T>();
                return redis.GetById(id.ToLower());
             
            }
        }

        public IQueryable<T> GetAll<T>()
        {
            using (var typedclient = new RedisClient())
            {
                var redis = _redisClient.As<T>();
                return redis.GetAll().AsQueryable();
            }
        }

        public IQueryable<T> GetAll<T>(string hash, string value, Expression<Func<T, bool>> filter)
        {
            var filtered = _redisClient.GetAllEntriesFromHash(hash).Where(c => c.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            var ids = filtered.Select(c => c.Key);

            var ret = _redisClient.As<T>().GetByIds(ids).AsQueryable()
                                .Where(filter);

            return ret;
        }

        public IQueryable<T> GetAll<T>(string hash, string value)
        {
            var filtered = _redisClient.GetAllEntriesFromHash(hash).Where(c => c.Value.Equals(value, StringComparison.InvariantCultureIgnoreCase));
            var ids = filtered.Select(c => c.Key);

            var ret = _redisClient.As<T>().GetByIds(ids).AsQueryable();
            return ret;
        }

        public void Set<T>(T item)
        {
            using (var typedclient = new RedisClient())
            {
                var redis = _redisClient.As<T>();
                redis.Store(item);
            }
        }

        public void Set<T>(T item, string hash, string value, string keyName)
        {
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty(keyName);

            _redisClient.SetEntryInHash(hash, prop.GetValue(item).ToString(), value.ToLower());

            _redisClient.As<T>().Store(item);
        }

        public void Set<T>(T item, List<string> hash, List<string> value, string keyName)
        {
            Type t = item.GetType();
            PropertyInfo prop = t.GetProperty(keyName);

            for (int i = 0; i < hash.Count; i++)
            {
                _redisClient.SetEntryInHash(hash[i], prop.GetValue(item).ToString(), value[i].ToLower());
            }

            _redisClient.As<T>().Store(item);
        }

        public void SetAll<T>(List<T> listItems)
        {
            using (var typedclient = new RedisClient())
            {
                var redis = _redisClient.As<T>();
                redis.StoreAll(listItems);
            }
        }

        public void SetAll<T>(List<T> list, string hash, string value, string keyName)
        {
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo prop = t.GetProperty(keyName);

                _redisClient.SetEntryInHash(hash, prop.GetValue(item).ToString(), value.ToLower());

                _redisClient.As<T>().StoreAll(list);
            }
        }

        public void SetAll<T>(List<T> list, List<string> hash, List<string> value, string keyName)
        {
            foreach (var item in list)
            {
                Type t = item.GetType();
                PropertyInfo prop = t.GetProperty(keyName);

                for (int i = 0; i < hash.Count; i++)
                {
                    _redisClient.SetEntryInHash(hash[i], prop.GetValue(item).ToString(), value[i].ToLower());
                }

                _redisClient.As<T>().StoreAll(list);
            }
        }

        public void Delete<T>(T item)
        {
            using (var typedclient = new RedisClient())
            {
                var redis = _redisClient.As<T>();
                redis.Delete(item);
            }
        }

        public void DeleteAll<T>(T item)
        {
            using (var typedclient = new RedisClient())
            {
                var redis = _redisClient.As<T>();
                redis.DeleteAll();
            }
        }

        public long PublishMessage(string channel, object item)
        {
            var ret = _redisClient.PublishMessage(channel, JsonConvert.SerializeObject(item));
            return ret;
        }
    } 
}