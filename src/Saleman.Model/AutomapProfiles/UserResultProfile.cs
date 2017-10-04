namespace Saleman.Model
{
	using System;
	using AutoMapper;
	using Saleman.Model.ServiceObjects;
	using Microsoft.AspNetCore.Identity;
	using System.Linq;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class UserResultProfile : Profile
	{
		public UserResultProfile()
		{
			this.CreateMap<IdentityResult, ResultServiceObject>()
			.ForMember(mn => mn.Status, (obj) =>  obj.MapFrom(src => src.Succeeded))
			.ForMember(mn => mn.Errors, (obj) =>  obj.Ignore());

			this.CreateMap<IdentityUser, UserServiceObject>();
		}
	}
}
