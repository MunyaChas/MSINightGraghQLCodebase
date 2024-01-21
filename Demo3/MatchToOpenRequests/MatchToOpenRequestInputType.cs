using Demo3.Types.EmployeeTypes;
using Demo3.Types.OpenRequestTypes;

namespace Demo3.MatchToOpenRequests
{
    public class MatchToOpenRequestInputType : InputObjectType<AddMatchToOpenRequestInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AddMatchToOpenRequestInput> descriptor)
        {
            descriptor.Name("AddMatchToOpenRequestInput");
            descriptor.Field(x => x.OpenRequestId).Type<NonNullType<OpenRequestIdType>>().ID();
            descriptor.Field(x => x.EmployeeId).Type<NonNullType<EmployeeIdType>>().ID();
        }
    }
}
