using System;
using Xunit;
using Moq;
using FluentAssertions;
using TrackMyWristAPI.Models;
using TrackMyWristAPI.Services.UserService;
using TrackMyWristAPI.Services.WatchService;
using System.Threading.Tasks;
using TrackMyWristAPI.Dtos.Watch;
using Microsoft.EntityFrameworkCore;

namespace TrackMyWristAPI.Tests
{
    public class WatchServiceTests : BaseTests
    {
        [Fact]
        public async Task GetAllWatches_WithNoExistingWatches_ReturnsEmptyList()
        {
            // GIVEN
            var dbName = Guid.NewGuid().ToString();
            var dbContext = BuildContext(dbName);
            var mapper = BuildMap();
            var userServiceStub = new Mock<IUserService>();
            var watchService = new WatchService(mapper, dbContext, userServiceStub.Object);

            // WHEN
            var result = await watchService.GetAllWatches();

            // THEN
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetWatchById_WithMatchingWatch_ReturnsMatchingWatch()
        {
            // GIVEN
            var dbName = Guid.NewGuid().ToString();
            var dbContext1 = BuildContext(dbName);

            dbContext1.Watches.Add(new Watch() { Manufacturer = "Longines" });
            dbContext1.SaveChanges();

            var mapper = BuildMap();
            var userServiceStub = new Mock<IUserService>();
            var dbContext2 = BuildContext(dbName);
            var watchService = new WatchService(mapper, dbContext2, userServiceStub.Object);

            // WHEN
            var getWatchDto = await watchService.GetWatchById(1);

            // THEN
            getWatchDto.Id.Should().Be(1);
            getWatchDto.Manufacturer.Should().Be("Longines");
        }

        [Fact]
        public async Task AddWatch_WithNewAddWatchDto_ReturnsNewGetWatchDto()
        {
            // GIVEN
            var dbName = Guid.NewGuid().ToString();
            var dbContext1 = BuildContext(dbName);
            var mapper = BuildMap();
            var userServiceStub = new Mock<IUserService>();
            // userServiceStub.Setup(user => user.GetUserId()).Returns(2);
            var watchService = new WatchService(mapper, dbContext1, userServiceStub.Object);

            var addWatchDto = new AddWatchDto() { Manufacturer = "Longines" };

            var dbContext2 = BuildContext(dbName);

            // WHEN
            var getWatchDto = await watchService.AddWatch(addWatchDto);

            // THEN
            getWatchDto.Id.Should().Be(1);
            getWatchDto.Manufacturer.Should().Be("Longines");

            var addedWatch = await dbContext2.Watches.FirstOrDefaultAsync(w => w.Id == 1);
            addedWatch.Id.Should().Be(1);
            addedWatch.Manufacturer.Should().Be("Longines");
        }
    }
}
