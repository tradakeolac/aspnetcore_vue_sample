using AutoMapper;
using Moq;
using Saleman.Web.Infrastructure.Factories;
using System;
using Xunit;

namespace WebFramework.Infrastructure.Tests
{
    public class AutoMapperObjectFactoryTests
    {
        [Fact]
        public void Create_Should_Throw_RequestNullExeption()
        {
            var mapper = new Mock<IMapper>();
            var factory = new AutoMapperObjectFactory(mapper.Object);

            var s = factory.Create<object>(null);
        }
    }
}
