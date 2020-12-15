using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ODataDateTime.ODataServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SetTimeZoneInfo(TimeZoneInfo.Utc);

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            var entityBuilder = builder.EntitySet<Common.TestEntity>("TestEntities");
            entityBuilder.EntityType.HasKey(x => x.Id);

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
