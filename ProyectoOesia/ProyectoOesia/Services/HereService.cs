using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProyectoOesia.Services
{
    public class HereService : IHereService
    {
        public async Task<GeoCodingArray> SearchGeocoding(string address, string city, string postalcode)
        {
            var token = await HereApiToken();
            var client = new RestClient($"https://geocode.search.hereapi.com/v1/geocode?q={address}+{city}+{postalcode}%2C+Spain");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {token}");
            IRestResponse response = await client.ExecuteAsync(request);
            var coords = JsonSerializer.Deserialize<GeoCodingArray>(response.Content);
            return coords;
        }
        private async Task<string> HereApiToken()
        {
            var url = "https://account.api.here.com/oauth2/token";
            var client = new RestClient(url);
            client.Timeout = -1;
            var nonce = GetNonce();
            var timestamp = GetTimestamp();
            var accessKey = "UN8j3UMRzVt24twANirImg";
            var secret = "jeIR6E5Mx44Lbj8_75lFjLx9WHk6WR3H9V6LRTzE1Z8zM_V2n3lP_TRzTGM3rhZgIxI7nNwoGGgnJeBbyXNroQ";
            var baseString = @"grant_type=client_credentials&oauth_consumer_key=" + accessKey + "&oauth_nonce=" + nonce + "&oauth_signature_method=HMAC-SHA256&oauth_timestamp=" + timestamp + "&oauth_version=1.0";

            var workingString = new List<string>();
            foreach (var parameter in baseString.Split('&').ToList())
            {
                workingString.Add(Uri.EscapeDataString(parameter.Split('=')[0] + "=" + parameter.Split('=')[1].Trim()));
            }
            var urlEncodeParamaterString = String.Join(Uri.EscapeDataString("&"), workingString.ToArray());
            var fullBaseString = $"POST&{Uri.EscapeDataString(url)}&{urlEncodeParamaterString}";

            var signature = CreateToken(fullBaseString, (Uri.EscapeDataString(secret) + "&"));
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "" +
                "OAuth oauth_consumer_key=\"" + accessKey + "\"," +
                "oauth_signature_method=\"HMAC-SHA256\"," +
                "oauth_timestamp=\"" + timestamp + "\"," +
                "oauth_nonce=\"" + nonce + "\"," +
                "oauth_version=\"1.0\"," +
                "oauth_signature=\"" + Uri.EscapeDataString(signature) + "\"");
            request.AddParameter("grant_type", "client_credentials");
            IRestResponse response = await client.ExecuteAsync(request);

            var req = response.Content;
            var access = req.Substring(17, req.IndexOf(",") - 18);
            return access;
        }

        private string GetNonce()
        {
            var rand = new Random();
            var nonce = rand.Next(1000000000);
            return nonce.ToString();

        }

        private string CreateToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        private string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public class GeoCodingArray
        {

            public GeoCoding[] items { get; set; }

            public class GeoCoding
            {

                public GeoPosition position { get; set; }

                public class GeoPosition
                {
                    public double lat { get; set; }
                    public double lng { get; set; }

                }
            }
        }

    }
}
