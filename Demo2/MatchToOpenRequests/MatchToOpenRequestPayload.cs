using Demo2.Common;
using Demo2.Data;

namespace Demo2.MatchToOpenRequests
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
