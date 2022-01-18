using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TrackMyWristAPI.Data;
using TrackMyWristAPI.Services.UserService;

namespace TrackMyWristAPI.Tests
{
    public class BaseTests
    {
        protected IMapper Mapper { get; }
        protected Mock<IUserService> UserServiceStub { get; } = new();

        public BaseTests()
        {
            Mapper = BuildMap();
        }

        protected DataContext BuildContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dataContext = new DataContext(options);
            return dataContext;
        }

        private IMapper BuildMap()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfile());
            });

            return config.CreateMapper();
        }
    }
}