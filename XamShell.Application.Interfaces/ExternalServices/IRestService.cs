using System.Collections.Generic;
using System.Threading.Tasks;
using XamShell.Application.Models.DTOs;

namespace XamShell.Infrastructure.ExternalServices
{
    public interface IRestService
    {
        Task<UserDto> RefreshDataAsync ();

    }
}