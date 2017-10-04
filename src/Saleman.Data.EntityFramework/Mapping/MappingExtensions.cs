using Microsoft.EntityFrameworkCore;
using WebFramework.Infrastructure.Helpers;
using System;
using System.Linq;
using System.Reflection;

namespace Saleman.Data.EntityFramework.Mapping
{
    public static class MappingExtensions
    {
        public static void ScanAssembly(this ModelBuilder builder)
        {
            var builderClasses = AssemblyHelper.LoadByType(typeof(MappingEntityTypeBase));

            if(builderClasses != null && builderClasses.Any())
            {
                foreach(var builderClass in builderClasses)
                {
                    if(!builderClass.GetTypeInfo().IsAbstract)
                    {
                        var instance = Activator.CreateInstance(builderClass) as MappingEntityTypeBase;

                        instance.Config(builder);
                    }
                }
            }
        }
    }
}
