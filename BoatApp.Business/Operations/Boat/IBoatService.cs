using SailTracker.Business.Operations.Boat.Dtos;
using SailTracker.Business.Types;

namespace SailTracker.Business.Operations.SailTracker
{
    public interface IVesselService
    {
        Task<ServiceMessage> AddVessel(AddVesselDto vessel);
        Task<VesselDto> GetVessel(int id);
        Task<List<VesselDto>> GetVessels();
        Task<ServiceMessage> AdjustEstimatedValue(int id, int changeTo);
        Task<ServiceMessage> DeleteVessel(int id);
        Task<ServiceMessage> UpdateVessel(UpdateVesselDto vessel);
    }
}

