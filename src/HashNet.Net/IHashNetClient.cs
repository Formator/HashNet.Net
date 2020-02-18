using System.Threading.Tasks;

namespace HashNet.Net
{
    public interface IHashNetClient
    {
        string AccountEndpoint { get; set; }
// IServiceClientAsync HashNetRpcClient { get; }
        //Task<ListAddressesResponse> GetAddresses();
        Task<object> GetAddresses();
        Task<object> GetBlockCount();
    }
}