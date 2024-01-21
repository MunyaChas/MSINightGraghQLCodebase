using Demo3.Common;
using Demo3.Data;

namespace Demo3.OpenRequestBUs
{
    public class OpenRequestBUPayload : OpenRequestBUPayloadBase
    {
        public OpenRequestBUPayload(OpenRequestBU openRequestBU)
             : base(openRequestBU)
        {
        }
        public OpenRequestBUPayload(IReadOnlyList<ErrorResult> errors)
            : base(errors)
        {
        }
        public OpenRequestBUPayload(ErrorResult error)
            : base(new[] { error })
        {
        }
    }
}
