using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace HashNet.Net.Tests
{
    [TestFixture]
    public class BlockchainTests
    {
        private const int Port = 9200;
        static readonly string BaseUri = $"http://127.0.0.1:{Port}";
        public IHashNetClient CreateClient() => new HashNetClient(BaseUri);

        [Test]
        public void Can_GetBlockCount()
        {
            var testClient = CreateClient();
            var result = testClient.GetBlockCount();
            var finaleResult = result.Result;
        }
    }
}
