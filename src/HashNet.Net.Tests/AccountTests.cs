using Funq;
using ServiceStack;
using NUnit.Framework;
using HashNet.Net;
using HashNet.Net.Model;
using ProtoBuf.Grpc.Client;
using System.Threading.Tasks;

namespace HashNet.Net.Tests
{
    [TestFixture]
    public class AccountTests
    {
        private const int Port = 9100;
        static readonly string BaseUri = $"http://127.0.0.1:{Port}/";
        public IHashNetClient CreateClient() => new HashNetClient(BaseUri);

        [Test]
        public void Can_Charge_Customer()
        {
            var testClient = CreateClient();
            var result = testClient.GetAddresses();
            var finaleResult = result.Result;
        }
    }
}