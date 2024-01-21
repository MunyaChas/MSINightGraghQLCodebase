using Demo3.Common;
using Demo3.Data;

namespace Demo3.OpenRequestBUs
{
    public class OpenRequestBUPayloadBase : Payload
    {
        protected OpenRequestBUPayloadBase(OpenRequestBU openRequestBU)
        {
            OpenRequestBU = openRequestBU;
        }

        protected OpenRequestBUPayloadBase(IReadOnlyList<ErrorResult> errors)
            : base(errors)
        {
        }

        public OpenRequestBU? OpenRequestBU { get; }
    }
}
