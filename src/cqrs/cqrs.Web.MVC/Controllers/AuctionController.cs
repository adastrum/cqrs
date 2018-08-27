using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cqrs.Application.Specifications;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.Interfaces;
using cqrs.Domain.ValueObjects;
using cqrs.Messaging.Commands;
using cqrs.Messaging.Interfaces;
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
        private readonly IBus _bus;

        public AuctionController(
            UserManager<IdentityUser> userManager,
            IRepository<User> userRepository,
            IAuctionRepository auctionRepository,
            IMapper mapper,
            IBus bus
        )
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _bus = bus;
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
            //todo: validation
            var currentUser = await GetCurrentUser();

            //todo: user not found
            var auction = new Auction(model.Name, model.Description, new TimeSpan(model.Days, model.Hours, model.Minutes, 0), new Money(model.InitialAmount), currentUser);
            auction.Start();

            var created = await _auctionRepository.CreateAsync(auction);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            var auction = await _auctionRepository.FindOneAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<AuctionViewModel>(auction);

            var currentUser = await GetCurrentUser();

            model.CanManage = currentUser.Id == model.Seller.Id;

            return View(model);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Close(string id)
        {
            var commandResult = await _bus.SendCommandAsync(new CloseAuctionCommand(id));

            //todo: handle failures

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Cancel(string id)
        {
            var commandResult = await _bus.SendCommandAsync(new CancelAuctionCommand(id));

            //todo: handle failures

            return RedirectToAction(nameof(Index));
        }

        private async Task<User> GetCurrentUser()
        {
            var identityUser = await _userManager.GetUserAsync(User);

            var users = await _userRepository.FindAllAsync(new UserByName(identityUser.UserName));

            return users.SingleOrDefault();
        }
    }
}