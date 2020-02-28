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
        /// <returns>Number of available confirmed blocks in block chain.</returns>
        Task<ulong> GetBlockCount();
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
        /// Creates new keystore attached to master or light node.
        /// </summary>
        /// <param name="masterPassword">Locks entire keystore with this password if provided, if empty keystore will not be locked.</param>
        /// <returns>bool</returns>
        Task<bool> Create(string masterPassword);
        /// <summary>
        /// Opens existing keystore attached to master or light node.
        /// </summary>
        /// <param name="masterPassword">Unlocks keystore with this password if keystore was originally locked with provided password.</param>
        /// <returns>bool</returns>
        Task<bool> Open(string masterPassword);
        /// <summary>
        /// Changes master password used to lock entire keystore.
        /// </summary>
        /// <param name="oldMasterPassword">Current master password used to lock keystore.</param>
        /// <param name="newMasterPassword">New master password that will replace current one.</param>
        /// <returns>bool</returns>
        Task<bool> ChangePassword(string oldMasterPassword, string newMasterPassword);
        /// <summary>
        /// List all addresses in keystore attached to master or light node.
        /// </summary>
        /// <returns>ListAddressesResponse</returns>
        Task<ListAddressesResponse> ListAddresses();
        /// <summary>
        /// List all addresses (stored in keystore attached to master or light node) with their associated name and current balance status.
        /// </summary>
        /// <returns>ListBalancePerAddressResponse</returns>
        Task<ListBalancePerAddressResponse> ListBalancePerAddress();
        /// <summary>
        /// Verifies if provided address string is in valid Tolar address format.
        /// </summary>
        /// <param name="address">Address in hex string format.</param>
        /// <returns>VerifyAddressResponse</returns>
        Task<VerifyAddressResponse> VerifyAddress(ByteString address);
        /// <summary>
        /// Creates new address in keystore attached to master or light node.
        /// </summary>
        /// <param name="name">Optional address description name.</param>
        /// <param name="lockPassword">Optional password to protect generate keypair for newly created address.</param>
        /// <param name="lockHint">Optional password hint for selected password.</param>
        /// <returns>ByteString</returns>
        Task<ByteString> CreateNewAddress(string name, string lockPassword, string lockHint);
        /// <summary>
        /// Changes lock password for single address used to lock its private key in keystore.
        /// </summary>
        /// <param name="address">Address for which password changing is required.</param>
        /// <param name="oldPassword">Current address password.</param>
        /// <param name="newPassword">New address password that will replace current one.</param>
        /// <returns>bool</returns>
        Task<bool> ChangeAddressPassword(ByteString address, string oldPassword, string newPassword);
        /// <summary>
        /// Exports key file for selected address from keystore attached to master or light node.
        /// </summary>
        /// <param name="address">Selected address for which export keypair information is required.</param>
        /// <returns></returns>
        Task<string> ExportKeyFile(ByteString address);
        /// <summary>
        /// Imports key file to keystore attached to master or light node.
        /// </summary>
        /// <param name="jsonKeyFile">Key file in encrypted JSON format.</param>
        /// <param name="name">Optional name for imported address.</param>
        /// <param name="lockPassword">Provide lock password if original key file was password protected.</param>
        /// <param name="lockHint">Optional lock hint for lock password.</param>
        /// <returns>Returns true if import successful, false otherwise.</returns>
        Task<bool> ImportKeyFile(string jsonKeyFile, string name, string lockPassword, string lockHint);
        /// <summary>
        /// Sends data for creating transaction on light node only if sender address private key is stored in node keystore, transaction signing is left for node to handle.
        /// </summary>
        /// <param name="senderAddress">Address in Tolar format from which transaction will be send.</param>
        /// <param name="receiverAddress">Address in Tolar format to which transaction will be send.</param>
        /// <param name="amount">Amount of tolars to send.</param>
        /// <param name="senderAddressPassword">Password to unlock private key for sender address on node keystore (leave empty for no password).</param>
        /// <param name="gas">Maximum gas (gas limit) that will be spend to send this transaction (gas used for transaction sending or computational work in case of smart contracts).</param>
        /// <param name="gasPrice">Amount of gas to pay for each unit of gas, greater gas price is related to faster time to execute transaction (transaction fee = gas * gas price).</param>
        /// <param name="data">Smart contract bytecode in hex format.</param>
        /// <param name="nonce">Unique transaction index for this sender address (auto-incremented value, each transaction has unique nonce).</param>
        /// <returns>Transaction hash.</returns>
        Task<ByteString> SendRawTransaction(ByteString senderAddress, ByteString receiverAddress, ByteString amount, string senderAddressPassword, ByteString gas, ByteString gasPrice, string data, ByteString nonce);
        /// <summary>
        /// Sends data for creating transaction on light node only if sender address private key is stored in node keystore, transaction signing is left for node to handle.
        /// Transaction used for transferring funds from sender to receiver address.
        /// </summary>
        /// <param name="senderAddress">Address in Tolar format from which transaction will be send.</param>
        /// <param name="receiverAddress">Address in Tolar format to which transaction will be send.</param>
        /// <param name="amount">Amount of tolars to send.</param>
        /// <param name="senderAddressPassword">Password to unlock private key for sender address on node keystore (leave empty for no password).</param>
        /// <param name="gas">Maximum gas (gas limit) that will be spend to send this transaction (gas used for transaction sending or computational work in case of smart contracts).</param>
        /// <param name="gasPrice">Amount of gas to pay for each unit of gas, greater gas price is related to faster time to execute transaction (transaction fee = gas * gas price).</param>
        /// <param name="nonce">Unique transaction index for this sender address (auto-incremented value, each transaction has unique nonce).</param>
        /// <returns>Transaction hash</returns>
        Task<ByteString> SendFundTransferTransaction(ByteString senderAddress, ByteString receiverAddress, ByteString amount, string senderAddressPassword, ByteString gas, ByteString gasPrice, ByteString nonce);
        /// <summary>
        /// Sends data for creating transaction on light node only if sender address private key is stored in node keystore, transaction signing is left for node to handle.
        /// Transaction used for deploying the contract.
        /// </summary>
        /// <param name="senderAddress">Address in Tolar format from which transaction will be send.</param>
        /// <param name="amount">Amount of tolars (can be required by contract constructor).</param>
        /// <param name="senderAddressPassword">Password to unlock private key for sender address on node keystore (leave empty for no password).</param>
        /// <param name="gas">Maximum gas (gas limit) that will be spend to send this transaction (gas used for transaction sending or computational work in case of smart contracts).</param>
        /// <param name="gasPrice">Amount of gas to pay for each unit of gas, greater gas price is related to faster time to execute transaction (transaction fee = gas * gas price).</param>
        /// <param name="data">Smart contract bytecode in hex format.</param>
        /// <param name="nonce">Unique transaction index for this sender address (auto-incremented value, each transaction has unique nonce).</param>
        /// <returns>Transaction hash</returns>
        Task<ByteString> SendDeployContractTransaction(ByteString senderAddress, ByteString amount, string senderAddressPassword, ByteString gas, ByteString gasPrice, string data, ByteString nonce);
        /// <summary>
        /// Sends data for creating transaction on light node only if sender address private key is stored in node keystore, transaction signing is left for node to handle.
        /// Transaction used for executing contract functions
        /// </summary>
        /// <param name="senderAddress">Address in Tolar format from which transaction will be send.</param>
        /// <param name="receiverAddress">Contract address in Tolar format.</param>
        /// <param name="amount">Amount of tolars if needed in contract function.</param>
        /// <param name="senderAddressPassword">Password to unlock private key for sender address on node keystore (leave empty for no password).</param>
        /// <param name="gas">Maximum gas (gas limit) that will be spend to send this transaction (gas used for transaction sending or computational work in case of smart contracts).</param>
        /// <param name="gasPrice">Amount of gas to pay for each unit of gas, greater gas price is related to faster time to execute transaction (transaction fee = gas * gas price).</param>
        /// <param name="data">Function bytecode in hex format.</param>
        /// <param name="nonce">Unique transaction index for this sender address (auto-incremented value, each transaction has unique nonce).</param>
        /// <returns>Transaction hash</returns>
        Task<ByteString> SendExecuteFunctionTransaction(ByteString senderAddress, ByteString receiverAddress, ByteString amount, string senderAddressPassword, ByteString gas, ByteString gasPrice, string data, ByteString nonce);
        
        #endregion
    }
}