using ServiceStack.Redis;
using System;
using System.Collections;
using System.Collections.Generic;


namespace MB.Code.Caching
{
    public class RedisProvider : ICacheProvider
    {
        private PooledRedisClientManager _pooledRedisClientManager;
        private string host;
        public bool Contains(string key)
        {
            bool result;
            using (IRedisClient _client = this.GetClient())
            {
                result = _client.ContainsKey(key);
            }
            return result;
        }
        public T Get<T>(string key)
        {
            T result;
            using (IRedisClient _client = this.GetClient())
            {
                result = _client.Get<T>(key);
            }
            return result;
        }
        public System.Collections.IDictionary Gets(string[] keys)
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                ht.Add(key, this.Get<object>(key));
            }
            return ht;

           //功能区
        }
        public void Set<T>(string key, T value)
        {
            using (IRedisClient _client = this.GetClient())
            {
                this.UseTransaction(_client, delegate (IRedisTransaction transaction)
                {
                    transaction.QueueCommand((IRedisClient c) => c.Set<T>(key, value));
                });
            }
        }
        public void Set<T>(string key, T value, System.DateTime expr)
        {
            System.DateTime dt = System.DateTime.Now;
            System.TimeSpan ts = expr - dt;
            this.Set<T>(key, value, ts);
        }
        public void Set<T>(string key, T value, System.TimeSpan ts)
        {
            using (IRedisClient _client = this.GetClient())
            {
                _client.Set<T>(key, value, ts);
            }
        }
        public void SetExprieTime(string key, System.DateTime expr)
        {
            System.DateTime dt = System.DateTime.Now;
            System.TimeSpan ts = expr - dt;
            this.SetExprieTime(key, ts);
        }
        public void SetExprieTime(string key, System.TimeSpan ts)
        {
            using (IRedisClient _client = this.GetClient())
            {
                _client.ExpireEntryIn(key, ts);
            }
        }
        public System.TimeSpan GetExprieTime(string key)
        {
            System.TimeSpan ts;
            using (IRedisClient _client = this.GetClient())
            {
                ts =(TimeSpan)_client.GetTimeToLive(key);
            }
            return ts;

            
        }
        public void Remove(string key)
        {
            using (IRedisClient _client = this.GetClient())
            {
                this.UseTransaction(_client, delegate (IRedisTransaction transaction)
                {
                    transaction.QueueCommand((IRedisClient c) => c.Remove(key));
                });
            }
        }
        public void Dispose()
        {
            this._pooledRedisClientManager.Dispose();
        }
        public void SetServerInfo(string host, int maxPoolSize)
        {
            this.host = host;
            string[] writeHost = new string[]
            {
                host
            };
            string[] readHosts = writeHost;
            System.Collections.Generic.IEnumerable<string> arg_35_0 = writeHost;
            System.Collections.Generic.IEnumerable<string> arg_35_1 = readHosts;
            RedisClientManagerConfig redisClientManagerConfig = new RedisClientManagerConfig();
            redisClientManagerConfig.MaxWritePoolSize=maxPoolSize;
            redisClientManagerConfig.MaxReadPoolSize=maxPoolSize;
            redisClientManagerConfig.AutoStart=true;
            this._pooledRedisClientManager = new PooledRedisClientManager(arg_35_0, arg_35_1, redisClientManagerConfig);
        }
        private IRedisClient GetClient()
        {
            return this._pooledRedisClientManager.GetClient();
        }
        private void UseTransaction(IRedisClient client, System.Action<IRedisTransaction> action)
        {
            using (IRedisTransaction transaction = client.CreateTransaction())
            {
                action(transaction);
                transaction.Commit();
            }
        }
        internal ICacheProvider CreateDefault()
        {
            return new RedisProvider();
        }
    }
}
