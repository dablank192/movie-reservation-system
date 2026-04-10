using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Auth;

public class AuthApi : Group
{
    public AuthApi ()
    {
        Configure("api/v1/auth", ep =>
        {
            ep.AllowAnonymous();
            ep.Description(t => t.WithTags("Auth"));
        });
    }
}
