﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cqrs.Application.Specifications;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;
using cqrs.Web.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cqrs.Web.MVC.Controllers
{
    public class AuctionController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<User> _userRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;

        public AuctionController(
            UserManager<IdentityUser> userManager,
            IRepository<User> userRepository,
            IAuctionRepository auctionRepository,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var auctions = await _auctionRepository.FindAllAsync(new AuctionByStatus(AuctionStatus.Active));

            var auctionListItems = auctions
                .Select(x => _mapper.Map<AuctionListItemViewModel>(x))
                .ToList();

            var model = new AuctionListViewModel(auctionListItems);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateAuctionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuctionViewModel model)
        {
            var identityUser = await _userManager.GetUserAsync(User);

            var users = await _userRepository.FindAllAsync(new UserByName(identityUser.UserName));
            var user = users.SingleOrDefault();

            //todo: user not found
            var auction = new Auction(model.Name, model.Description, new TimeSpan(model.Days, model.Hours, model.Minutes, 0), new Money(model.InitialAmount), user);

            var created = await _auctionRepository.CreateAsync(auction);

            return RedirectToAction(nameof(Index));
        }
    }
}