using System.Text.Json;
namespace Contracts.GlobalErrorHandling
{
    public class ErrorDetails
    {
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    
    public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
