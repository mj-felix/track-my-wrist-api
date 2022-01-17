using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrackMyWristAPI.Data;

namespace TrackMyWristAPI.Tests
{
    public class BaseTests
    {
        protected DataContext BuildContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dataContext = new DataContext(options);
            return dataContext;
        }

        protected IMapper BuildMap()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfile());
            });

            return config.CreateMapper();
        }
    }
}