using NUnit.Framework;
using HashNet.Net;
using System.Threading.Tasks;

namespace HashNet.Net.Tests
{
    [TestFixture]
    public class AccountTests
    {
        private const int Port = 9600;
        static readonly string BaseUri = $"https://127.0.0.1:{Port}";
        public IHashNetClient CreateClient() => new HashNetClient(BaseUri);

        [Test]
        public void Can_GetAddresses()
        {
            var testClient = CreateClient();
            var result = testClient.GetAddresses();
            var finaleResult = result.Result;
        }
    }
}