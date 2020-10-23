namespace Dfo.Main.Api.Models
{
    public class SuccessResponseDto<TResponseData>
    {
        public bool Success { get { return true; } }
        public TResponseData Data { get; }

        public SuccessResponseDto(TResponseData data)
        {
            Data = data;
        }
    }
}
