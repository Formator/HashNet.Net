using System;
using ServiceStack;
using HashNet.Net.Model;

namespace HashNet.Net
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
