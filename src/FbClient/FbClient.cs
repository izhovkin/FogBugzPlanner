using FogBugzPlanner.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FogBugzPlanner.Client
{
    public class FbClient
    {
        public Uri Endpoint { get; private set; }
        public string Token { get; set; }

        public FbClient(Uri endpoint)
        {
            if (null == endpoint)
                throw new ArgumentNullException(nameof(endpoint));

            Endpoint = endpoint;
        }

        public async Task<string> Login(string username, string password)
        {
            var response = await InvokeCmd("logon", "email", username, "password", password);
            var token = response.Root.Element("token")?.Value;
            if (null == token)
                throw new Exception("Failed to login: no token found in response");
            return token;
        }

        public async Task<Person[]> ListPeople()
        {
            EnsureToken();

            var res = await InvokeCmd("listPeople", "token", Token);
            var people = res.Root.Element("people");
            if (null == people)
                throw new Exception("No people element found in response");

            return people.Elements("person").Select(p =>
            {
                var person = new Person();
                person.Id = (int)Convert.ChangeType(p.Element("ixPerson")?.Value ?? "", typeof(int));
                person.FullName = p.Element("sFullName")?.Value;
                person.Email = p.Element("sEmail")?.Value;
                return person;
            }).ToArray();
        }

        private async Task<XDocument> InvokeCmd(string cmd, params string[] arguments)
        {
            var client = new HttpClient();
            client.BaseAddress = Endpoint;

            var argsString = string.Join("&", SplitPairs(arguments).Select(pair => $"{Escape(pair.Item1)}={Escape(pair.Item2)}"));

            var r = await client.PostAsync($"api.asp?cmd={cmd}&{argsString}", EmptyContent());

            var stream = await r.Content.ReadAsStreamAsync();

            var response = XDocument.Load(stream);
            var error = response.Root.Element("error");
            if (null != error)
            {
                throw new Exception(error.Value);
            }
            return response;
        }

        private IEnumerable<Tuple<string, string>> SplitPairs(string[] arguments)
        {
            if (arguments.Length % 2 != 0)
                throw new ArgumentException("The arguments list has not even length");

            for (var i = 0; i < arguments.Length; i += 2)
            {
                yield return Tuple.Create(arguments[i], arguments[i + 1]);
            }
        }

        private void EnsureToken()
        {
            if (string.IsNullOrEmpty(Token))
                throw new InvalidOperationException("Token is required");
        }

        private static StringContent EmptyContent() => new StringContent("");       

        private static string Escape(string dataString) => Uri.EscapeDataString(dataString);
    }
}
