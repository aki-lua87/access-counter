using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

[assembly : LambdaSerializer (typeof (Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
  public class Handler
  {
    public async Task<APIGatewayProxyResponse> Process (APIGatewayProxyRequest gatewayProxyRequest, ILambdaContext context)
    {
      Console.WriteLine (gatewayProxyRequest);
      return new APIGatewayProxyResponse ();
    }
  }
}