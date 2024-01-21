using Demo2.Common;
using Demo2.Data;

namespace Demo2.MatchToOpenRequests
{
    public class MatchToOpenRequestPayloadBase : Payload
    {
        public MatchToOpenRequestPayloadBase(MatchToOpenRequest matchToOpenRequest)
        {
            MatchToOpenRequest = matchToOpenRequest;
        }

        public MatchToOpenRequestPayloadBase(IReadOnlyList<ErrorResult> errors)
            : base(errors)
        {
        }

        public MatchToOpenRequest? MatchToOpenRequest { get; }
    }
}
