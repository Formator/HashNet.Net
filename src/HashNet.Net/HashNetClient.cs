using Google.Protobuf;
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
            BlockchainEndpoint = blockchainEndpoint;
            AccountEndpoint = accountEndpoint;
        }

        public string BlockchainEndpoint { get; set; }
        public string AccountEndpoint{ get; set; }

        #region Blockchain
        /// <summary>
        /// Gets number of confirmed blocks in current node block chain.
        /// </summary>
        /// <returns>GetBlockCountResponse</returns>
        public async Task<GetBlockCountResponse> GetBlockCount()
        {
            var reply = await BlockchainService.GetBlockCountAsync(new GetBlockCountRequest());
            return reply;
        }
        /// <summary>
        /// Retrieves confirmed block information from current node block chain.
        /// </summary>
        /// <param name="blockHash">Hash for requested block.</param>
        /// <returns>GetBlockResponse</returns>
        public async Task<GetBlockResponse> GetBlockByHash(ByteString blockHash)
        {
            // var iblockHash = ByteString.CopyFromUtf8(blockHash);
            var reply = await BlockchainService.GetBlockByHashAsync(new GetBlockByHashRequest() {BlockHash = blockHash });
            return reply;
        }
        /// <summary>
        /// Retrieves confirmed block information from current node block chain.
        /// </summary>
        /// <param name="blockIndex">Block index for requested block.</param>
        /// <returns>GetBlockResponse</returns>
        public async Task<GetBlockResponse> GetBlockByIndex(ulong blockIndex)
        {
            var reply = await BlockchainService.GetBlockByIndexAsync(new GetBlockByIndexRequest() { BlockIndex = blockIndex });
            return reply;
        }
        /// <summary>
        /// Retrieves confirmed transaction information from current node block chain.
        /// </summary>
        /// <param name="transactionHash">Hash for requested transaction.</param>
        /// <returns>GetTransactionResponse</returns>
        public async Task<GetTransactionResponse> GetTransaction(ByteString transactionHash)
        {
            var reply = await BlockchainService.GetTransactionAsync(new GetTransactionRequest() { TransactionHash = transactionHash });
            return reply;
        }
        /// <summary>
        /// Retrieves block chain statistics information for current node block chain.
        /// </summary>
        /// <returns>GetBlockchainInfoResponse</returns>
        public async Task<GetBlockchainInfoResponse> GetBlockchainInfo()
        {
            var reply = await BlockchainService.GetBlockchainInfoAsync(new GetBlockchainInfoRequest());
            return reply;
        }
        /// <summary>
        /// Retrieves most recent transaction list based on transaction limit and how many transactions to skip (provides ability to get transactions in batches).
        /// </summary>
        /// <param name="addresses">List of all addresses by which transaction should be filtered (leave empty to apply no filter and return all transactions).</param>
        /// <param name="limit">Maximum number of transactions to return in one batch (no more than 1000).</param>
        /// <param name="skip">Number of most recent transactions to skip starting from blockchain’s last confirmed block.</param>
        /// <returns>GetTransactionListResponse</returns>
        public async Task<GetTransactionListResponse> GetTransactionList(IList<ByteString> addresses, ulong limit, ulong skip)
        {
            var req = new GetTransactionListRequest() { Limit = limit, Skip = skip };
            req.Addresses.AddRange(addresses);
            var reply = await BlockchainService.GetTransactionListAsync(req);
            return reply;
        }
        /// <summary>
        /// Get current balance (of tolars) for selected address.
        /// </summary>
        /// <param name="address">Selected address in Tolar address format.</param>
        /// <param name="blockIndex">Balance at specific block index.</param>
        /// <returns>GetBalanceResponse</returns>
        public async Task<GetBalanceResponse> GetBalance(ByteString address, ulong blockIndex)
        {
            var reply = await BlockchainService.GetBalanceAsync(new GetBalanceRequest() { Address = address, BlockIndex = blockIndex });
            return reply;
        }
        /// <summary>
        /// Get current balance (of tolars) for selected address.
        /// </summary>
        /// <param name="address">Selected address in Tolar address format.</param>
        /// <param name="blockIndex"></param>
        /// <returns>GetBalanceResponse</returns>
        public async Task<GetBalanceResponse> GetLatestBalance(ByteString address, ulong blockIndex)
        {
            var reply = await BlockchainService.GetLatestBalanceAsync(new GetBalanceRequest() { Address = address, BlockIndex = blockIndex });
            return reply;
        }
        /// <summary>
        /// Get next available nonce value for specific address.
        /// </summary>
        /// <param name="address">Selected address in Tolar address format.</param>
        /// <returns>GetNonceResponse</returns>
        public async Task<GetNonceResponse> GetNonce(ByteString address)
        {
            var reply = await BlockchainService.GetNonceAsync(new GetNonceRequest() { Address = address });
            return reply;
        }
        /// <summary>
        /// Executes read only contract functions on evm without spending gas or having any effect to address balances.
        /// </summary>
        /// <param name="senderAddress">Address in Tolar format from which transaction will be send.</param>
        /// <param name="receiverAddress">Contract address in Tolar format.</param>
        /// <param name="value">Amount of tolars if needed in contract function.</param>
        /// <param name="gas">Maximum gas (gas limit) that is available for call function.</param>
        /// <param name="gasPrice">Amount of gas to pay for each unit of gas, greater gas price is related to faster time to execute transaction (transaction fee = gas * gas price).</param>
        /// <param name="data">Contract function bytecode in hex format.</param>
        /// <param name="nonce">Unique transaction index for this sender address (auto-incremented value, each transaction has unique nonce).</param>
        /// <returns></returns>
        public async Task<TryCallTransactionResponse> TryCallTransaction(ByteString senderAddress, ByteString receiverAddress, ByteString value, ByteString gas, ByteString gasPrice, string data, ByteString nonce)
        {
            var reply = await BlockchainService.TryCallTransactionAsync(new Transaction() { SenderAddress = senderAddress, ReceiverAddress = receiverAddress, Value = value, Gas = gas, GasPrice = gasPrice, Data = data, Nonce = nonce });
            return reply;
        }
        #endregion

        #region Account
        public async Task<ListAddressesResponse> GetAddresses()
        {
            var reply = await AccountService.ListAddressesAsync(new ListAddressesRequest());
            return reply;
        }
        #endregion

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
