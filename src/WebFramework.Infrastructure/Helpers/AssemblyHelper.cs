namespace WebFramework.Infrastructure.Helpers
{
    using Microsoft.Extensions.DependencyModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AssemblyHelper
    {
        public static IEnumerable<Assembly> LoadCodingAssemblies()
        {
            var dependencies = DependencyContext.Default.CompileLibraries;
            var assemblies = new List<Assembly>();
            foreach (var library in dependencies.Where(l => l.Name.StartsWith("saleman.", StringComparison.OrdinalIgnoreCase)
            || l.Name.StartsWith("webframework.", StringComparison.OrdinalIgnoreCase)))
            {
                try
                {
                    var asem = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(asem);
                }
                catch(Exception ex)
                {
                    // Log
                }
            }

            return assemblies;
        }

        public static IEnumerable<Type> LoadByType(Type type)
        {
            return LoadCodingAssemblies()
                   .SelectMany(s => s.GetTypes())
                   .Where(p => type.IsAssignableFrom(p)
                   && p.GetTypeInfo().IsClass);
        }
    }
}