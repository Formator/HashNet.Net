using HashNet.Net.Model.Account;
using ServiceStack;
using System.Threading.Tasks;

namespace HashNet.Net
{
    public interface IHashNetClient
    {
        string Url { get; set; }
        IServiceClientAsync HashNetRpcClient { get; }
        Task<ListAddressesResponse> GetAddresses();
    }
}