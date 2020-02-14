using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

[assembly : LambdaSerializer (typeof (Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
  public class Handler
  {
    public async Task<APIGatewayProxyResponse> Process (APIGatewayProxyRequest gatewayProxyRequest, ILambdaContext context)
    {
      Console.WriteLine (JsonConvert.SerializeObject (gatewayProxyRequest));
      return new APIGatewayProxyResponse
      {
        StatusCode = 200,
        Body = "JsonConvert.SerializeObject (body)"
      };
    }
  }
}