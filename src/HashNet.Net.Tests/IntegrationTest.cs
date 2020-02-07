using Funq;
using ServiceStack;
using NUnit.Framework;
using HashNet.Net;
using HashNet.Net.Model;
using ProtoBuf.Grpc.Client;
using System.Threading.Tasks;

namespace HashNet.Net.Tests
{
    public class IntegrationTest
    {
        private const int Port = 2000;
        static readonly string BaseUri = $"http://localhost:{Port}/";
        public IServiceClientAsync CreateClient() => new GrpcServiceClient(BaseUri);

        public IntegrationTest()
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
        }

        [Test]
        public async Task Can_call_Hello_Service_WebHost()
        {
            var client = new GrpcServiceClient("https://localhost:5001");

            var response = await client.GetAsync(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }

        

        [Test]
        public async Task Can_call_Hello_Service()
        {
            var client = CreateClient();

            var response = await client.GetAsync(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}