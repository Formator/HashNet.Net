using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tolar.Proto;

namespace HashNet.Net
{
    public class HashNetClient: IHashNetClient
    {
        private BlockchainService.BlockchainServiceClient _blockchainService;
        private AccountService.AccountServiceClient _accountService;

        public HashNetClient(string blockchainEndpoint = "https://localhost:9100", string accountEndpoint = "https://localhost:9100")
        {
            AccountEndpoint = accountEndpoint;
            BlockchainEndpoint = blockchainEndpoint;
        }

        public string AccountEndpoint{ get; set; }
        public string BlockchainEndpoint { get; set; }

        public async Task<object> GetAddresses()
        {
            var reply = await AccountService.ListAddressesAsync(new ListAddressesRequest());
            return reply;
        }

        public async Task<object> GetBlockCount()
        {
            var reply = await BlockchainService.GetBlockCountAsync(new GetBlockCountRequest());
            return reply;
        }

        public async Task<object> GetBlockchainInfo()
        {
            var reply = await BlockchainService.GetBlockchainInfoAsync(new GetBlockchainInfoRequest());
            return reply;
        }

        private AccountService.AccountServiceClient AccountService
        {
            get
            {
                if (_accountService == null)
                {
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
                    try
                    {
                        var channel = GrpcChannel.ForAddress(AccountEndpoint); //, ChannelCredentials.Insecure
                        _accountService = new AccountService.AccountServiceClient(channel);
                    }
                    finally
                    {
                        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
                    }
                }
                return _accountService;
            }
        }

        private BlockchainService.BlockchainServiceClient BlockchainService
        {
            get
            {
                if (_blockchainService == null)
                {
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
                    try
                    {
                        var channel = GrpcChannel.ForAddress(BlockchainEndpoint);
                        _blockchainService = new BlockchainService.BlockchainServiceClient(channel);
                    }
                    finally
                    {
                        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", false);
                        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", false);
                    }
                }
                return _blockchainService;
            }
        }
    }
}
