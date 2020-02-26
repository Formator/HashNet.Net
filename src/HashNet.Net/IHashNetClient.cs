using Google.Protobuf;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tolar.Proto;

namespace HashNet.Net
{
    public interface IHashNetClient
    {
        string AccountEndpoint { get; set; }

        #region Blockchain
        /// <summary>
        /// Gets number of confirmed blocks in current node block chain.
        /// </summary>
        /// <returns>GetBlockCountResponse</returns>
        Task<GetBlockCountResponse> GetBlockCount();
        /// <summary>
        /// Retrieves confirmed block information from current node block chain.
        /// </summary>
        /// <param name="blockHash">Hash for requested block.</param>
        /// <returns>GetBlockResponse</returns>
        Task<GetBlockResponse> GetBlockByHash(ByteString blockHash);
        /// <summary>
        /// Retrieves confirmed block information from current node block chain.
        /// </summary>
        /// <param name="blockIndex">Block index for requested block.</param>
        /// <returns>GetBlockResponse</returns>
        Task<GetBlockResponse> GetBlockByIndex(ulong blockIndex);
        /// <summary>
        /// Retrieves confirmed transaction information from current node block chain.
        /// </summary>
        /// <param name="transactionHash">Hash for requested transaction.</param>
        /// <returns>GetTransactionResponse</returns>
        Task<GetTransactionResponse> GetTransaction(ByteString transactionHash);
        /// <summary>
        /// Retrieves block chain statistics information for current node block chain.
        /// </summary>
        /// <returns>GetBlockchainInfoResponse</returns>
        Task<GetBlockchainInfoResponse> GetBlockchainInfo();

        /// <summary>
        /// Retrieves most recent transaction list based on transaction limit and how many transactions to skip (provides ability to get transactions in batches).
        /// </summary>
        /// <param name="addresses">List of all addresses by which transaction should be filtered (leave empty to apply no filter and return all transactions).</param>
        /// <param name="limit">Maximum number of transactions to return in one batch (no more than 1000).</param>
        /// <param name="skip">Number of most recent transactions to skip starting from blockchain’s last confirmed block.</param>
        /// <returns>GetTransactionListResponse</returns>
        Task<GetTransactionListResponse> GetTransactionList(IList<ByteString> addresses, ulong limit, ulong skip);
        /// <summary>
        /// Get current balance (of tolars) for selected address.
        /// </summary>
        /// <param name="address">Selected address in Tolar address format.</param>
        /// <param name="blockIndex">Balance at specific block index.</param>
        /// <returns>GetBalanceResponse</returns>
        Task<GetBalanceResponse> GetBalance(ByteString address, ulong blockIndex);
        /// <summary>
        /// Get current balance (of tolars) for selected address.
        /// </summary>
        /// <param name="address">Selected address in Tolar address format.</param>
        /// <param name="blockIndex"></param>
        /// <returns>GetBalanceResponse</returns>
        Task<GetBalanceResponse> GetLatestBalance(ByteString address, ulong blockIndex);
        /// <summary>
        /// Get next available nonce value for specific address.
        /// </summary>
        /// <param name="address">Selected address in Tolar address format.</param>
        /// <returns>GetNonceResponse</returns>
        Task<GetNonceResponse> GetNonce(ByteString address);
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
        Task<TryCallTransactionResponse> TryCallTransaction(ByteString senderAddress, ByteString receiverAddress, ByteString value, ByteString gas, ByteString gasPrice, string data, ByteString nonce);
        #endregion

        #region Account
        /// <summary>
        /// Extracting address information from blockchain.
        /// </summary>
        /// <returns>ListAddressesResponse</returns>
        Task<ListAddressesResponse> GetAddresses();
        #endregion
    }
}