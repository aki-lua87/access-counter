using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
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

      var project = gatewayProxyRequest.PathParameters["project"];
      var username = gatewayProxyRequest.PathParameters["username"];

      Console.WriteLine ("username:::" + username);
      Console.WriteLine ("project:::" + project);

      var tableName = Environment.GetEnvironmentVariable ("ACCESSCOUNT_DYNAMODB_TABLE");

      var result = PutAccessCount(tableName,username,project);
      Console.WriteLine (JsonConvert.SerializeObject (result));

      var num = result.Attributes["AccessCount"].N;

      return new APIGatewayProxyResponse
      {
        StatusCode = 200,
          Headers = new Dictionary<string, string> ()
          { { "one", "abc" }, { "content-type", "image/svg+xml;charset=utf-8" }
          },
          Body = $@"<svg xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" width=""108"" height=""20""><linearGradient id=""b"" x2=""0"" y2=""100%""><stop offset=""0"" stop-color=""#bbb"" stop-opacity="".1""/><stop offset=""1"" stop-opacity="".1""/></linearGradient><clipPath id=""a""><rect width=""108"" height=""20"" rx=""3"" fill=""#fff""/></clipPath><g clip-path=""url(#a)""><path fill=""#555"" d=""M0 0h71v20H0z""/><path fill=""#007ec6"" d=""M71 0h37v20H71z""/><path fill=""url(#b)"" d=""M0 0h108v20H0z""/></g><g fill=""#fff"" text-anchor=""middle"" font-family=""DejaVu Sans,Verdana,Geneva,sans-serif"" font-size=""110""> <text x=""365"" y=""150"" fill=""#010101"" fill-opacity="".3"" transform=""scale(.1)"" textLength=""610"">totalAccess</text><text x=""365"" y=""140"" transform=""scale(.1)"" textLength=""610"">totalAccess</text><text x=""885"" y=""150"" fill=""#010101"" fill-opacity="".3"" transform=""scale(.1)"" textLength=""270"">{num}</text><text x=""885"" y=""140"" transform=""scale(.1)"" textLength=""270"">{num}</text></g> </svg>"
      };
    }

    public UpdateItemResponse PutAccessCount (string tableName, string username,string project)
    {
      var Client = new AmazonDynamoDBClient (RegionEndpoint.APNortheast1);
      Dictionary<string, AttributeValueUpdate> updates = new Dictionary<string, AttributeValueUpdate> ();
      updates["AccessCount"] = new AttributeValueUpdate ()
      {
        Action = AttributeAction.ADD,
        Value = new AttributeValue { N = "1" }
      };

      var request = new UpdateItemRequest
      {
        TableName = tableName,
        Key = new Dictionary<string, AttributeValue> ()
        { { "username", new AttributeValue { S = username } }, { "project", new AttributeValue { S = project } },
        },
        AttributeUpdates = updates,
        ReturnValues = "UPDATED_NEW",
      };
      return Client.UpdateItemAsync (request).Result;
    }

    public class AccessCountTable
    {
      public string Username { get; set; }
      public string Project { get; set; }
      public int AccessCount { get; set; }
      public DateTime UpdateDate { get; set; }
    }
  }
}