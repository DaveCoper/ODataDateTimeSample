using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataDateTime.ODataClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var odataClient = new Simple.OData.Client.ODataClient("http://localhost:53339");

            //var entries = await odataClient.For<Common.TestEntity>().FindEntriesAsync();

            var newEntry = await odataClient.For<Common.TestEntity>().Set(new Common.TestEntity { Id = 150, DateTimeValue = DateTime.Now }).InsertEntryAsync();

            Debug.Assert(newEntry.LastDateTimeKind != "Unspecified", "Incorrect date time kind.");
        }
    }
}
