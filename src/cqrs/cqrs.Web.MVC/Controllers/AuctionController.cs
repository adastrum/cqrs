using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cqrs.Application.Specifications;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Commands;
using cqrs.Messaging.Common;
using cqrs.Messaging.Interfaces;
using cqrs.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace cqrs.Web.MVC.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public User CurrentUser => (User)HttpContext.Items[nameof(CurrentUser)];

        public AuctionController(
            IAuctionRepository auctionRepository,
            IMapper mapper,
            IBus bus
        )
        {
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
            var command = new CreateAuctionCommand(model.Name, model.Description, model.Days, model.Hours, model.Minutes, model.InitialAmount, CurrentUser);

            var commandResult = await _bus.SendCommandAsync(command);

            return HandleCommandResult(commandResult, nameof(Create), nameof(Index));
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

            model.CanManage = CurrentUser.Id == model.Seller.Id;

            return View(model);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(string id)
        {
            var command = new CancelAuctionCommand(id);

            var commandResult = await _bus.SendCommandAsync(command);

            return HandleCommandResult(commandResult, nameof(Detail), nameof(Index));
        }

        [HttpPost("{id}/bid")]
        public async Task<IActionResult> Bid(string id, decimal amount)
        {
            var command = new BidCommand(id, amount, CurrentUser);

            var commandResult = await _bus.SendCommandAsync(command);

            return HandleCommandResult(commandResult, nameof(Detail), nameof(Index));
        }

        private IActionResult HandleCommandResult(CommandResult commandResult, string errorViewName, string successActionName)
        {
            if (commandResult.Succeeded)
            {
                return RedirectToAction(successActionName);
            }

            ModelState.AddModelError(string.Empty, commandResult.Details);

            return View(errorViewName);
        }
    }
}