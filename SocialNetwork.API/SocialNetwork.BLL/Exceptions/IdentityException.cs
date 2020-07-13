using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.BLL.Exceptions
{
    public class IdentityException : Exception
    {
        public IEnumerable<IdentityError> Errors { get; }

        public IdentityException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }
    }
}
