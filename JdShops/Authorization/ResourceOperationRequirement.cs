using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JdShops.Entities;
using Microsoft.AspNetCore.Authorization;

namespace JdShops.Authorization
{
    public enum ResourceOperation
    {
        Create, 
        Read, 
        Update, 
        Delete
    }

    public class ResourceOperationRequirement :IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;

        }
        
        public ResourceOperation ResourceOperation { get; }

        
    }




}
