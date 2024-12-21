using System.Text.Json.Serialization;
using AuthLib.Models;

namespace WpfApp1.Models;
[Serializable]
public class ResponseLogin
{
    [JsonPropertyName("access_token")]
    public string access_token{ get; set; }
    
}