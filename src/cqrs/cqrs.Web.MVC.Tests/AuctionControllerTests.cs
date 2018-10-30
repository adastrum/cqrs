using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using cqrs.Application.Specifications;
using cqrs.Domain.Entities;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;
using cqrs.Messaging.Interfaces;
using cqrs.Web.MVC.Controllers;
using cqrs.Web.MVC.Models;
using cqrs.Web.MVC.Models.MapperProfiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cqrs.Web.MVC.Tests
{
    public class AuctionControllerTests
    {
        private readonly Mock<IAuctionRepository> _auctionRepositoryMock;
        private readonly Mapper _mapper;
        private readonly Mock<ICommandDispatcher> _commandDispatcherMock;

        public AuctionControllerTests()
        {
            _auctionRepositoryMock = new Mock<IAuctionRepository>();
            var auctionProfile = new AuctionProfile();
            var mapperConfiguration = new MapperConfiguration(x => x.AddProfile(auctionProfile));
            _mapper = new Mapper(mapperConfiguration);
            _commandDispatcherMock = new Mock<ICommandDispatcher>();
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAuctions()
        {
            _auctionRepositoryMock
                .Setup(x => x.FindAllAsync(It.IsAny<AuctionByStatus>()))
                .ReturnsAsync(new List<Auction>
                {
                    new Auction("aaa", "bbb", TimeSpan.FromDays(1), new Money(1000), new User("ccc"))
                });

            var controller = new AuctionController(_auctionRepositoryMock.Object, _mapper, _commandDispatcherMock.Object);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AuctionListViewModel>(viewResult.ViewData.Model);
            Assert.Single(model.Auctions);
        }
    }
}
