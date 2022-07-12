using MarketPlace.Domain;
using MarketPlace.Interfaces;
using Polly;
using Polly.Retry;

namespace MarketPlace.DomainEvents.EventConsumers
{
    public class ProductAddedEventHandler : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ProductAddedEventHandler> _logger;
        private CancellationToken _stoppingToken;
        private IEmailService? _emailService;

        public ProductAddedEventHandler(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProductAddedEventHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            DomainEventManager.Register<ProductAdded>(ev => { _ = SendEmailNotification(ev); });
        }

        private async Task SendEmailNotification(ProductAdded ev)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            Task SendAsync(CancellationToken cancellationToken)
            {
                var emailTo = "nickita_piter@mail.ru";
                var subject = "test";
                var emailBody = "If u r reading this text, somewhere one little good was added to the Catalog";
                return _emailService.SendEmailAsync(emailTo, subject, emailBody, cancellationToken);
            }

            AsyncRetryPolicy? policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(3)
                    }, (exception, retryAttempt) =>
                    {
                        _logger.LogWarning(exception, "Error while sending email. Retrying: {Attempt}", retryAttempt);
                    });

            PolicyResult? result = await policy.ExecuteAndCaptureAsync(SendAsync, _stoppingToken);

            if (result.Outcome == OutcomeType.Failure)
            {
                _logger.LogError(result.FinalException, "There was an error while sending email");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            return Task.CompletedTask;
        }
    }
}
