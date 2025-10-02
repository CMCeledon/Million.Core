using System.Diagnostics.CodeAnalysis;
namespace Application.Helpers;
[ExcludeFromCodeCoverage]
public class ResponseServices<T>
{
        public bool State { get; set; }
        public string Message { get; set; }
        public T Info { get; set; }
        public string Type { get; set; }
        public string Warning { get; set; }
        public string TransactionId { get; set; }
 }