using Demo3.Data;

namespace Demo3.Types.OpenRequestTypes
{
    public class OpenRequestIdType : InputObjectType<OpenRequestId>
    {
        protected override void Configure(IInputObjectTypeDescriptor<OpenRequestId> descriptor)
        {
            // descriptor.Name("OpenRequestId");
            descriptor.Field(x => x.Value).Type<NonNullType<IdType>>().ID();
        }
    }
}
