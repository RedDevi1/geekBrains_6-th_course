using Microsoft.AspNetCore.Mvc;
using MarketPlace.Models;
using MarketPlace.Interfaces;
using Serilog;
using Serilog.Core;
using Polly;
using Polly.Retry;

namespace MarketPlace.Controllers
{
    public class GoodsCatalogsController : Controller
    {
        private readonly ILogger<GoodsCatalogsController> _logger;
        private readonly IGoodsCatalog catalog;
        private readonly object _syncObj_1 = new();
        private readonly IEmailService _emailService;
        private readonly Func<int, TimeSpan>? GetSleepDurationByRetryAttempt;

        public GoodsCatalogsController(ILogger<GoodsCatalogsController> logger, IGoodsCatalog catalog, IEmailService emailService)
        {
            _logger = logger;
            this.catalog = catalog;
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> GoodsCreationAsync(Good model)
        {
            try
            {
                catalog.Create(model);
                AsyncRetryPolicy? policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(3, GetSleepDurationByRetryAttempt, onRetry: (exception, retryAttempt) =>
                    {
                        _logger.LogWarning(exception, "Error while sending email. Retrying: {Attempt}", retryAttempt);
                    });

                PolicyResult? result = await policy.ExecuteAndCaptureAsync(token =>
                _emailService.SendEmailAsync("nickita_piter@mail.ru", "test", "If u r reading this text, somewhere one little good was added to the Catalog"), );

                if (result.Outcome == OutcomeType.Failure)
                {
                    _logger.LogError(result.FinalException, "There was an error while sending email");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return View();
        }

        [HttpGet]
        public IActionResult GoodsCreation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Products()
        {
            lock (_syncObj_1)
            {
                return View(catalog);
            }
        }
        [HttpPost]
        public IActionResult GoodsRemoving(long article)
        {
            lock (_syncObj_1)
            {
                try
                {
                    catalog.Delete(article);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult GoodsRemoving()
        {
            return View();
        }
    }
}
