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
        static readonly string BaseUri = $"http://localhost:{Port}";
        public IHashNetClient CreateClient() => new HashNetClient(blockchainEndpoint:BaseUri, accountEndpoint: BaseUri);

        [Test]
        public void Can_GetBlockCount()
        {
            var testClient = CreateClient();
            var result = testClient.GetBlockCount();
            var finaleResult = result.Result;
        }

        [Test]
        public void Can_GetBlockchainInfo()
        {
            var testClient = CreateClient();
            var result = testClient.GetBlockchainInfo();
            var finaleResult = result.Result;
        }

    }
}
