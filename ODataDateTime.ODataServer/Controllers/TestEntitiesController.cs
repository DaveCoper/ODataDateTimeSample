using Microsoft.AspNet.OData;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web.Http;

namespace ODataDateTime.ODataServer.Controllers
{
    public class TestEntitiesController : ODataController
    {
        private static ConcurrentDictionary<int, Common.TestEntity> data;

        static TestEntitiesController()
        {
            data = new ConcurrentDictionary<int, Common.TestEntity>();

            for (int i = 1; i < 5; ++i)
            {
                var dateTime = DateTime.UtcNow + TimeSpan.FromHours(1);
                data.TryAdd(i, new Common.TestEntity { Id = i, DateTimeValue = dateTime, LastDateTimeKind = dateTime.Kind.ToString() });
            }
        }

        [EnableQuery]
        public IQueryable<Common.TestEntity> Get()
        {
            return data.Values.AsQueryable();
        }

        [EnableQuery]
        public Common.TestEntity Get([FromODataUri] int key)
        {
            return data[key];
        }

        public IHttpActionResult Post(Common.TestEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kind is unspecified. I would expect UTC because of setting config.SetTimeZoneInfo(TimeZoneInfo.Utc) in WebApiConfig;
            entity.LastDateTimeKind = entity.DateTimeValue.Kind.ToString();

            data[entity.Id] = entity;
            return Created(entity);
        }
    }
}