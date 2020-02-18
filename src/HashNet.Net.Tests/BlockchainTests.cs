using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace HashNet.Net.Tests
{
    [TestFixture]
    public class BlockchainTests
    {
        private const int Port = 9100;
        static readonly string BaseUri = $"https://116.203.182.5:{Port}";
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
