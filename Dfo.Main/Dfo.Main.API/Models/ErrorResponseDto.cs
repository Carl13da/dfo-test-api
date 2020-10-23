namespace Dfo.Main.Api.Models
{
    public class ErrorResponseDto
    {
        public bool Success { get { return false; } }
        public string Error { get; }

        public ErrorResponseDto(string error)
        {
            Error = error;
        }
    }
}
