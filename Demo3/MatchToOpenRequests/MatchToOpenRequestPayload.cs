using Demo3.Common;
using Demo3.Data;

namespace Demo3.MatchToOpenRequests
{
    public class MatchToOpenRequestPayload : MatchToOpenRequestPayloadBase
    {
        public MatchToOpenRequestPayload(MatchToOpenRequest matchToOpenRequest)
            : base(matchToOpenRequest)
        {
        }

        public MatchToOpenRequestPayload(IReadOnlyList<ErrorResult> errors)
            : base(errors)
        {
        }

        public MatchToOpenRequestPayload(ErrorResult error)
            : base(new[] { error })
        {

        }
    }
}
