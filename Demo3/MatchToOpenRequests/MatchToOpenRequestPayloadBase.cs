using Demo3.Common;
using Demo3.Data;

namespace Demo3.MatchToOpenRequests
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
