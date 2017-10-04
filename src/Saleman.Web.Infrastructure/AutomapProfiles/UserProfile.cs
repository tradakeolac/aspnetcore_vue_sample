using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Saleman.Model.ServiceObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            this.CreateMap<IdentityUser, AuthenticationServiceObject>();
        }
    }
}
