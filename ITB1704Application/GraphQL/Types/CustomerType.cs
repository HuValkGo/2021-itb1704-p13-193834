using System;
using System.Collections.Generic;
using System.Linq;
using ITB1704Application.GraphQL.Resolvers;
using ITB1704Application.Model;
using System.Threading.Tasks;
using HotChocolate.Types;
using HotChocolate.Data;

namespace ITB1704Application.GraphQL.Types
{
    public class CustomerType : ObjectType<Customer>
    {
        [UseFiltering]
        [UseSorting]
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {

            descriptor.Field<CustomerResolver>(r => r.GetTransactionsByCustomerAsync(default, default, default, default, default))
                .Name("transactions");
        }
    }
}
