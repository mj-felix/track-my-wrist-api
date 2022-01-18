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
            var watchService = new WatchService(Mapper, dbContext, UserServiceStub.Object);

            // WHEN
            var result = await watchService.GetAllWatches();

            // THEN
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetWatchById_WithExistingWatchId_ReturnsMatchingWatch()
        {
            // GIVEN
            var dbName = Guid.NewGuid().ToString();
            var dbContext1 = BuildContext(dbName);
            dbContext1.Watches.Add(new Watch() { Manufacturer = "Longines" });
            dbContext1.SaveChanges();
            var dbContext2 = BuildContext(dbName);
            var watchService = new WatchService(Mapper, dbContext2, UserServiceStub.Object);

            // WHEN
            var getWatchDto = await watchService.GetWatchById(1);

            // THEN
            getWatchDto.Id.Should().Be(1);
            getWatchDto.Manufacturer.Should().Be("Longines");
        }

        [Fact]
        public async Task AddWatch_WithNewAddWatchDto_ReturnsNewGetWatchDtoAndCreatesNewWatch()
        {
            // GIVEN
            var dbName = Guid.NewGuid().ToString();
            var dbContext1 = BuildContext(dbName);
            var watchService = new WatchService(Mapper, dbContext1, UserServiceStub.Object);
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
