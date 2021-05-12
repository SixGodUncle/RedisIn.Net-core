using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisDemo;
using StackExchange.Redis;
using System.Threading;

namespace RedisApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    { 
        public IDatabase RedisHelper { get; }

        public RedisController(RedisHelper redisHelper)
        {
            RedisHelper = redisHelper.GetDatabase();
        }


        //[HttpGet]
        //public async Task<IActionResult> GetValue()
        //{
        //    await RedisHelper.StringSetAsync("name", "meiyan");
        //    var name= await RedisHelper.StringGetAsync("name");

        //    var result = RedisHelper.PingAsync();
        //    return Ok(name.ToString());
        //}

        [HttpGet]
        public async Task<IActionResult> GetKeyTtl()
        {
            await RedisHelper.StringSetAsync(new KeyValuePair<RedisKey, RedisValue>[]
            {
                new KeyValuePair<RedisKey, RedisValue>("name","wzb"),
                new KeyValuePair<RedisKey, RedisValue>("age","23"),
                new KeyValuePair<RedisKey, RedisValue>("gender","男"), 
                new KeyValuePair<RedisKey, RedisValue>("city","wuhan"), 
            });

            var values = await RedisHelper.StringGetAsync(new RedisKey[] {"name", "age", "gender", "city"});

            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
            return Ok(values);
        }
    }
}
