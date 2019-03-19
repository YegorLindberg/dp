using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using StackExchange.Redis;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private const string MessagesChannelName = "events";
        private const string RedisConnectionURL = "localhost:6379";

        static readonly ConcurrentDictionary<string, string> _data = new ConcurrentDictionary<string, string>();
        private ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnectionURL);

        // GET api/values/<id>
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string value = null;
            _data.TryGetValue(id, out value);
            return value;
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]string value)
        {
            IDatabase db = redis.GetDatabase();

            string id = Guid.NewGuid().ToString();
            db.StringSet(id, value);

            ISubscriber sub = redis.GetSubscriber();
            sub.Publish(MessagesChannelName, id);

            return id;
        }
    }
}
