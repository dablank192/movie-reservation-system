using System;
using MailKit.Net.Smtp;
using Microsoft.VisualBasic.FileIO;
using MimeKit;
using MimeKit.Text;
using movie_reservation_system.Dto.Email;


namespace movie_reservation_system.Infrastructure.BackgroundService;

public class SendEmailService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly EmailQueue _queue;
    private readonly IConfiguration _config;
    private readonly ILogger<SendEmailService> _logger;

    public SendEmailService (EmailQueue queue, IConfiguration config, ILogger<SendEmailService> logger)
    {
        _queue = queue;
        _config = config;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        _logger.LogInformation("Email Service is working...");

        await foreach (var msg in _queue.GetQueueAsync(ct))
        {
            try
            {
                await SendEmailAsync(msg);
                _logger.LogInformation($"Email sent successfully to {msg.ToEmail}");
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error occured while sending email: {ex}");
            }
         }
    }

    public async Task SendEmailAsync (EmailMessage emailMessage)
    {
        var smtpSetting = _config.GetSection("SmtpSettings");

        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse(smtpSetting["SenderEmail"]!));
        email.To.Add(MailboxAddress.Parse(emailMessage.ToEmail!));
        email.Subject = emailMessage.Subject!;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text= emailMessage.Body!
        };

        using var smtp = new SmtpClient();

        // Bỏ qua lỗi xác thực SSL/Thu hồi chứng chỉ khi dùng môi trường Dev/Test (do not use this line of code in production)
        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

        await smtp.ConnectAsync(
            smtpSetting["Host"]!,
            int.Parse(smtpSetting["Port"]!),
            MailKit.Security.SecureSocketOptions.StartTls
            );
        await smtp.AuthenticateAsync(
            smtpSetting["Username"]!,
            smtpSetting["Password"]!
        );
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
