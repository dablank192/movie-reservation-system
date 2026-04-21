using System;
using FastEndpoints;
using movie_reservation_system.Exception.Reservation;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Payment.Gateway;

public class GatewayEp : EndpointWithoutRequest<ResponseModel>
{
    public AppDbContext? _context {get; set;}
    public IConfiguration? _config {get; set;}

    public override void Configure()
    {
        Get("/{reservationId}");
        Group<PaymentApi>();
        Roles("User", "Admin");
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        int reservationId = Route<int>("reservationId");

        var reservations = await _context!.Reservations.FindAsync(reservationId, ct)
        ?? throw new ReservationNotFoundException();

        var sePay = _config!.GetSection("SeQRPay");

        var accountNumber = sePay["AccountNum"];
        var bankName = sePay["Bank"];
        var template = sePay["Template"];
        var download = sePay["Download"];

        var price = reservations.TotalAmount;

        DateTime paymentTime = DateTime.UtcNow;
        string receipt = $"TICKET_{reservationId}_{paymentTime:yyyyMMdd}";

        var paymentUrl = $"https://qr.sepay.vn/img?acc={accountNumber}&bank={bankName}&amount={price}&des={receipt}&template={template}&download={download}";
    
        var response = new ResponseModel
        {
            PaymentUrl= paymentUrl,
            Amount= price,
            Message= receipt
        };

        await Send.OkAsync(response, ct);
    }
}
