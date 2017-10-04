using Moq;
using Saleman.Data.Repositories;
using Saleman.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Saleman.Service.Tests.Services
{
    public class AddressServiceTests
    {
        [Fact]
        public async void AddDistrictAsync_With_Null_Request_Param_Should_Throw_Exception()
        {
            // Arrange
            IAddressService addressService = new AddressService(null, null, null, null, null, null);

            // Act
            var result = await addressService.AddDistrictAsync(null);

            // Assert

        }
    }
}
