namespace Saleman.Data.EntityFramework.Mapping
{
    using Microsoft.EntityFrameworkCore;

    public abstract class MappingEntityTypeBase
    {
        public abstract void Config(ModelBuilder builder);
    }    
}
