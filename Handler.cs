using System;
using System.Collections.Generic;
using System.Linq;
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

      var pj = gatewayProxyRequest.PathParameters["project"];
      var name = gatewayProxyRequest.PathParameters["username"];

      Console.WriteLine ("username:::" + name);
      Console.WriteLine ("project:::" + pj);

      return new APIGatewayProxyResponse
      {
        StatusCode = 200,
          Headers = new Dictionary<string, string> ()
          { 
            { "one", "abc" }, { "content-type", "image/svg+xml;charset=utf-8" }
          },
        Body = @"<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""82"" height=""20""><linearGradient id=""b"" x2=""0"" y2=""100%""><stop offset=""0"" stop-color=""#bbb"" stop-opacity="".1""/><stop offset=""1"" stop-opacity="".1""/></linearGradient><clipPath id=""a""><rect width=""82"" height=""20"" rx=""3"" fill=""#fff""/></clipPath><g clip-path=""url(#a)""><path fill=""#555"" d=""M0 0h37v20H0z""/><path fill=""#ff69b4"" d=""M37 0h45v20H37z""/><path fill=""url(#b)"" d=""M0 0h82v20H0z""/></g><g fill=""#fff"" text-anchor=""middle"" font-family=""DejaVu Sans,Verdana,Geneva,sans-serif"" font-size=""110""> <text x=""195"" y=""150"" fill=""#010101"" fill-opacity="".3"" transform=""scale(.1)"" textLength=""270"">color</text><text x=""195"" y=""140"" transform=""scale(.1)"" textLength=""270"">color</text><text x=""585"" y=""150"" fill=""#010101"" fill-opacity="".3"" transform=""scale(.1)"" textLength=""350"">ff69b4</text><text x=""585"" y=""140"" transform=""scale(.1)"" textLength=""350"">ff69b4</text></g> </svg>"
      };
    }
  }
}