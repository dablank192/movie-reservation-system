using System;
using System.Threading.Channels;
using movie_reservation_system.Dto.Email;

namespace movie_reservation_system.Infrastructure.BackgroundService;

public class EmailQueue
{
    private readonly Channel<EmailMessage> _queue;
    
    public EmailQueue ()
    {
        _queue = Channel.CreateUnbounded<EmailMessage>();
    }

    public async Task IntoQueueAsync(EmailMessage emailMessage)
    {
        await _queue.Writer.WriteAsync(emailMessage);
    }

    public IAsyncEnumerable<EmailMessage> GetQueueAsync (CancellationToken ct)
    {
        return _queue.Reader.ReadAllAsync(ct);
    }
}
