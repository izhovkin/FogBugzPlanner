using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FogBugzPlanner.Client;

namespace Tests
{
    [TestFixture]
    public class FbClientTests
    {
        [Test]
        [Explicit("Need to supply password")]
        public async Task TestLogin()
        {
            var client = new FbClient(new Uri(Settings.FbUri));
            var token = await client.Login("Ilya Izhovkin", "");

            Assert.That(token, Is.Not.Null);
        }

        [Test]
        public void TestLoginInvalidCreds()
        {
            var client = new FbClient(new Uri(Settings.FbUri));
            Assert.ThrowsAsync<Exception>(async () =>
            {
                await client.Login("Ilya Izhovkin", "blabla");
            });            
        }
        
        [Test]
        public async Task TestListPeople()
        {
            var client = new FbClient(new Uri(Settings.FbUri)) { Token = Settings.Token };

            var people = await client.ListPeople();

            Assert.That(people.Length, Is.Not.Zero);
        }

        [Test]
        public async Task TestSearchCases()
        {
            var client = new FbClient(new Uri(Settings.FbUri)) { Token = Settings.Token };

            var cases = await client.SearchCases(@"tag:""ars.sprint.162"" status:""active""");

            Assert.That(cases.Length, Is.Not.Zero);
        }
    }
}
