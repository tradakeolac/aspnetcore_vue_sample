namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    using AutoMapper;
    using Saleman.Model.ServiceObjects;
    using Saleman.Web.ViewModel;

    public sealed class ResultProfile : Profile
    {
        public ResultProfile()
        {
            this.CreateMap<ResultServiceObject, ResultViewModel>()
                .ConvertUsing(new ResultFactory());
        }

        private class ResultFactory : ITypeConverter<ResultServiceObject, ResultViewModel>
        {
            public ResultViewModel Convert(ResultServiceObject source, ResultViewModel destination, ResolutionContext context)
            {
                return source.Status ? ResultViewModel.Success : ResultViewModel.Fail;
            }
        }
    }
}
