using MarketPlace.Interfaces;
using System.Diagnostics;

namespace MarketPlace.BackgroundServices
{
    public class ServerRuningEmailSender : BackgroundService
    {
        private readonly ILogger<ServerRuningEmailSender> _logger;
        private IEmailService? _emailService;
        private readonly IServiceProvider _serviceProvider;

        public ServerRuningEmailSender(IServiceProvider serviceProvider, ILogger<ServerRuningEmailSender> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using (var serviceScope = _serviceProvider.CreateAsyncScope())
            {
                var provider = serviceScope.ServiceProvider;
                _emailService = provider.GetRequiredService<IEmailService>();

                var emailTo = "nickita_piter@mail.ru";
                var subject = "runing server time";
                try
                {
                    using var timer = new PeriodicTimer(TimeSpan.FromHours(1));
                    Stopwatch sw = Stopwatch.StartNew();

                    while (await timer.WaitForNextTickAsync(stoppingToken))
                    {
                        await _emailService.SendEmailAsync(emailTo, subject, $"Сервер работает уже {sw.Elapsed}", stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }               
        }
    }
}
