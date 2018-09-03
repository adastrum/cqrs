using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using cqrs.Application.Specifications;
using cqrs.CommandStack.Commands;
using cqrs.Domain.Entities;
using cqrs.Domain.Enums;
using cqrs.Domain.Interfaces;
using cqrs.Messaging.Interfaces;
using cqrs.Web.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace cqrs.Web.MVC.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IMapper _mapper;
        private readonly ICommandDispatcher _commandDispatcher;

        public User CurrentUser => (User)HttpContext.Items[nameof(CurrentUser)];

        public AuctionController(
            IAuctionRepository auctionRepository,
            IMapper mapper,
            ICommandDispatcher commandDispatcher
        )
        {
            _auctionRepository = auctionRepository;
            _mapper = mapper;
            _commandDispatcher = commandDispatcher;
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

            await _commandDispatcher.PublishAsync(command);

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

            model.CanManage = CurrentUser.Id == model.Seller.Id;

            return View(model);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(string id)
        {
            var command = new CancelAuctionCommand(id);

            await _commandDispatcher.PublishAsync(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("{id}/bid")]
        public async Task<IActionResult> Bid(string id, decimal amount)
        {
            var command = new BidCommand(id, amount, CurrentUser);

            await _commandDispatcher.PublishAsync(command);

            return RedirectToAction(nameof(Index));
        }
    }
}