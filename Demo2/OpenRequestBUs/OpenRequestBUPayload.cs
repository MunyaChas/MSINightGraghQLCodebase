using Demo2.Common;
using Demo2.Data;

namespace Demo2.OpenRequestBUs
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
