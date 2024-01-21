using Demo3.Data;
using Demo3.Employees;

namespace Demo3.Types.EmployeeTypes
{
    public class UpdateEmployeeInputType : InputObjectType<UpdateEmployeeInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UpdateEmployeeInput> descriptor)
        {
            descriptor.Field(_ => _.EmployeeId).Type<NonNullType<EmployeeIdType>>().ID();
        }
    }

    internal class EmployeeIdType : InputObjectType<EmployeeId>
    {
        protected override void Configure(IInputObjectTypeDescriptor<EmployeeId> descriptor)
        {
            descriptor.Field(_ => _.Value).Type<NonNullType<IdType>>();
        }
    }
}
