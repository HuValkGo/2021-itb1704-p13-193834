using ITB1704Application.GraphQL;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITB1704Application.GraphQL.Types
{
    public class QueryType : ObjectType<Query>
    {
        
        //kui tahame ressursi nime muuta või midagi ignoreerida 
        /*protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(t => t.GetStudents(default, default))
                .Name("testid");

        }
        */

    }
}
