using Demo2.Common;
using Demo2.Data;

namespace Demo2.OpenRequestBUs
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
