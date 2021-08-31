using ITB1704Application.GraphQL.Resolvers;
using ITB1704Application.Model;
using HotChocolate.Types;


namespace ITB1704Application.GraphQL.Types
{
    public class TestType : ObjectType<Test>
    {
        protected override void Configure(IObjectTypeDescriptor<Test> descriptor)
        {
            
            /*descriptor.Field(t => t.Name)
                .Type<StringType>();
            */
            
        }

    }


}
