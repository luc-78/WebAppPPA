
using System.Threading.Tasks;
using WebAppPPA.Models.ViewModels;

namespace WebAppPPA.Models.Services.Application
{
    public interface IGrafService
    {
        Task<GrafEtaViewModel> GetEtaAsync();
        Task<GrafViewModel> GetNumeriVotatiAsync();
        
    }
}