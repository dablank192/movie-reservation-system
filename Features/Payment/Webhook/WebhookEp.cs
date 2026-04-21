using System;
using System.Text.RegularExpressions;
using FastEndpoints;
using movie_reservation_system.Dto;
using movie_reservation_system.Exception.Reservation;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Payment.Webhook;

public class WebhookEp : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext? _context {get; set;}
    public IConfiguration? _config {get; set;}

    public override void Configure()
    {
        Post("/webhook");
        Group<PaymentApi>();
        AllowAnonymous();
    }

    public override async Task HandleAsync (RequestModel req, CancellationToken ct)
    {
        var sePay = _config!.GetSection("SeQRPay");

        var authHeader = HttpContext.Request.Headers.Authorization.FirstOrDefault();
        var validToken = sePay["WebhookToken"];

        if (string.IsNullOrEmpty(authHeader) || !authHeader.Contains(validToken!))
        {
            await Send.ForbiddenAsync(ct);
            return;
        }

        var myVaAccount = sePay["AccountNum"];
        
        if (req.subAccount != myVaAccount)
        {
            await Send.OkAsync(
                new ResponseModel
                {
                    Success= true,
                    Message= "Invalid Account Number"
                },
                ct
            );
            return;
        }

        var matchInvoice = Regex.Match(req.content, @"TICKET_(\d+)", RegexOptions.IgnoreCase);

        if (!matchInvoice.Success)
        {
            await Send.OkAsync(
                new ResponseModel
                {
                    Success= true,
                    Message= "Invalid Invoice Details"
                },
                ct
            );
            return;
        }

        int reservationId = int.Parse(matchInvoice.Groups[1].Value);

        var reservations = await _context!.Reservations.FindAsync(reservationId, ct);

        if (reservations == null)
        {
            await Send.OkAsync(
                new ResponseModel
                {
                    Success= true,
                    Message= "Reservation not existed"
                },
                ct
            );
            return;            
        }


        if (reservations.Status != ReservationStatus.Pending)
        {
            await Send.OkAsync(
                new ResponseModel
                {
                    Success= true,
                    Message= "Reservation has been pay"
                },
                ct
            );
            return;
        }

        if (reservations.TotalAmount != req.transferAmount)
        {
            await Send.OkAsync (
                new ResponseModel
                {
                    Success= true,
                    Message= "Invalid amount, please try again"
                },
                ct
            );
            return;
        }


        reservations.Status = ReservationStatus.Confirmed;

        await _context.SaveChangesAsync(ct);

        await Send.OkAsync(
            new ResponseModel
            {
                Success= true,
                Message= "Payment Successfully"
            },
            ct
        );
    }
}
