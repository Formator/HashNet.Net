using ServiceStack;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HashNet.Net.Model.Account
{


    [DataContract]
    public class Create : IReturn<CreateResponse>
    {
        [DataMember(Order = 1, Name = "master_password")]
        public string MasterPassword { get; set; }
    }

    [DataContract]
    public class CreateResponse
    {
        [DataMember(Order = 1, Name = "result")]
        public bool Result { get; set; }
    }

    [DataContract]
    public class ListAddresses : IReturn<ListAddressesResponse>
    {
    }

    [DataContract]
    public class ListAddressesResponse
    {
        [DataMember(Order = 1, Name = "addresses")]
        public List<string> Addresses { get; set; }
    }

}
