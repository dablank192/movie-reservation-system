using System;

namespace movie_reservation_system.Features.Payment.Webhook;

public class RequestModel
{
    public int id { get; set; }
    public string gateway { get; set; } = default!;
    public string transactionDate { get; set; } = default!;
    public string accountNumber { get; set; } = default!;
    public string? subAccount { get; set; }
    public string content { get; set; } = default!;
    public string transferType { get; set; } = default!;
    public decimal transferAmount { get; set; }
    public string? referenceCode { get; set; }
}
