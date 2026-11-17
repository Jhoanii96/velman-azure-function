using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class OAuthClient
{
    private static string OAuthToken = null;
    private static DateTime TokenExpiry = DateTime.MinValue;

    public static async Task<string> MakeRequestWithOAuth20Async(string url, string grantType, string accessTokenUrl, string clientId,
        string clientSecret, string username, string password, string headerPrefix)
    {
        if (grantType == "Password Credentials")
        {
            // Check if token is null or expired
            if (string.IsNullOrEmpty(OAuthToken) || TokenExpiry <= DateTime.UtcNow)
            {
                try
                {
                    OAuthToken = await GetNewTokenAsync(accessTokenUrl, clientId, clientSecret, username, password);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to obtain OAuth token", ex);
                }
            }

            // Make the request using the token
            return await MakeAuthenticatedRequestAsync(url, headerPrefix, OAuthToken);
        }

        throw new ArgumentException("Unsupported grant type");
    }

    private static async Task<string> GetNewTokenAsync(string tokenUrl, string clientId, string clientSecret, string username, string password)
    {
        using (var client = new HttpClient())
        {
            var req = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "password",
                    ["client_id"] = clientId,
                    ["client_secret"] = clientSecret,
                    ["username"] = username,
                    ["password"] = password
                })
            };

            var res = await client.SendAsync(req);
            res.EnsureSuccessStatusCode();

            string json = await res.Content.ReadAsStringAsync();
            dynamic resp = JObject.Parse(json);

            OAuthToken = resp.access_token;
            int expiresIn = resp.expires_in;
            TokenExpiry = DateTime.UtcNow.AddSeconds(expiresIn);

            return OAuthToken;
        }
    }

    private static async Task<string> MakeAuthenticatedRequestAsync(string url, string headerPrefix, string token)
    {
        using (var client = new HttpClient())
        {
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(headerPrefix, token);

            var res = await client.SendAsync(req);
            res.EnsureSuccessStatusCode();

            return await res.Content.ReadAsStringAsync();
        }
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
