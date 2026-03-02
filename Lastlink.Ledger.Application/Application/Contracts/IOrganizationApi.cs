using Lastlink.Ledger.Application.DTOs;
using Lastlink.Ledger.Domain.Models;
using Refit;

namespace Lastlink.Ledger.Application.Contracts;

[Headers("Content-Type: application/json")]
public interface IOrganizationApi
{
    [Get("/organizations")]
    Task<PagedResult<Organization>> GetOrganizationsAsync();

    [Post("/organizations")]
    Task<Organization> CreateOrganizationAsync([Body] CreateOrganizationRequest request);
}
