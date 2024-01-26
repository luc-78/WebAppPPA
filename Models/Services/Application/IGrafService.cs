
using System.Threading.Tasks;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public interface IGrafService
    {
        Task<GrafViewModel> GetNumeriVotatiAsync();
        
    }
}