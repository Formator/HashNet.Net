using System;
using ServiceStack;
using HashNet.Net.Model;
using HashNet.Net.Model.Account;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;

namespace HashNet.Net
{
    public class HashNetClient : IHashNetClient
    {
        IServiceClientAsync hashNetRpcClient;

        public HashNetClient(): this("")
        {}

        public HashNetClient(string url)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            Url = url;
        }

        public string Url { get; set; }
        public IServiceClientAsync HashNetRpcClient 
        { 
            get 
            {
                if (hashNetRpcClient == null)
                    hashNetRpcClient = new GrpcServiceClient(Url);
                return hashNetRpcClient;
            }
        }

        public async Task<ListAddressesResponse> GetAddresses()
        {
            var client = HashNetRpcClient;

            var response = await client.GetAsync(new ListAddresses());
            return response;

            //Console.WriteLine(response.Result);
        }
    }
}
