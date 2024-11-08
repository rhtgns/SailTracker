using SailTracker.Business.Types;
using SailTracker.Data.Repositories;
using SailTracker.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SailTracker.Business.Operations.Boat.Dtos;
using SailTracker.Business.Operations.SailTracker;
using SailTracker.Data.Entities;

namespace SailTracker.Business.Operations.Boat
{
    public class VesselManager : IVesselService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<VesselEntity> _vesselRepository;
        private readonly IRepository<VesselFeatureEntity> _vesselFeatureRepository;

        public VesselManager(IUnitOfWork unitOfWork, IRepository<VesselEntity> vesselRepository, IRepository<VesselFeatureEntity> vesselFeatureRepository)
        {
            _unitOfWork = unitOfWork;
            _vesselRepository = vesselRepository;
            _vesselFeatureRepository = vesselFeatureRepository;
        }

        public async Task<ServiceMessage> AddVessel(AddVesselDto vessel)
        {
            var hasVessel = _vesselRepository.GetAll(x => x.VesselName.ToLower() == vessel.VesselName.ToLower()).Any();

            if (hasVessel)
            {
                return new ServiceMessage
                {
                    IsSucceeded = false,
                    Message = "Gemi mevcut"
                };
            }

            await _unitOfWork.BeginTransaction();

            var vesselEntity = new VesselEntity
            {
                VesselName = vessel.VesselName,
                HullModel = vessel.HullModel,
                EstimatedValue = vessel.EstimatedValue,
                VesselType = vessel.VesselType,
            };

            _vesselRepository.Add(vesselEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Gemi kaydı sırasında hata oldu");
            }

            foreach (var featureId in vessel.NavigationFeatureIds)
            {
                var vesselFeature = new VesselFeatureEntity
                {
                    VesselId = vesselEntity.Id,
                    FeatureId = featureId,
                };

                _vesselFeatureRepository.Add(vesselFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Gemi özellikleri eklenirken hata oldu, süreç başa sarıldı");
            }

            return new ServiceMessage
            {
                IsSucceeded = true
            };
        }

        public async Task<ServiceMessage> AdjustEstimatedValue(int id, int changeTo)
        {
            var vessel = _vesselRepository.GetById(id);
            if (vessel is null)
            {
                return new ServiceMessage
                {
                    IsSucceeded = false,
                    Message = "Bu id ile eşleşen gemi yok"
                };
            }
            vessel.EstimatedValue = changeTo;
            _vesselRepository.Update(vessel);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Değer değiştirilirken bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucceeded = true
            };
        }

        public async Task<ServiceMessage> DeleteVessel(int id)
        {
            var vessel = _vesselRepository.GetById(id);
            if (vessel is null)
            {
                return new ServiceMessage
                {
                    IsSucceeded = false,
                    Message = "Böyle bir gemi yok"
                };
            }

            _vesselRepository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Silme işlemi tamamlanmadı");
            }

            return new ServiceMessage
            {
                IsSucceeded = true
            };
        }

        public async Task<VesselDto> GetVessel(int id)
        {
            var vessel = await _vesselRepository.GetAll(x => x.Id == id)
                .Select(x => new VesselDto
                {
                    Id = x.Id,
                    VesselName = x.VesselName,
                    HullModel = x.HullModel,
                    EstimatedValue = x.EstimatedValue,
                    VesselType = x.VesselType,
                    NavigationFeatures = x.VesselFeatures.Select(s => new VesselFeatureDto
                    {
                        Id = s.Id,
                        FeatureName = s.Feature.FeatureName
                    }).ToList()
                }).FirstOrDefaultAsync();

            return vessel;
        }

        public async Task<List<VesselDto>> GetVessels()
        {
            var vessels = await _vesselRepository.GetAll()
                .Select(x => new VesselDto
                {
                    Id = x.Id,
                    VesselName = x.VesselName,
                    HullModel = x.HullModel,
                    EstimatedValue = x.EstimatedValue,
                    VesselType = x.VesselType,
                    NavigationFeatures = x.VesselFeatures.Select(s => new VesselFeatureDto
                    {
                        Id = s.Id,
                        FeatureName = s.Feature.FeatureName
                    }).ToList()
                }).ToListAsync();

            return vessels;
        }

        public async Task<ServiceMessage> UpdateVessel(UpdateVesselDto vessel)
        {
            var vesselEntity = _vesselRepository.GetById(vessel.Id);
            if (vesselEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceeded = false,
                    Message = "Gemi bulunamadı"
                };
            }

            await _unitOfWork.BeginTransaction();

            vesselEntity.VesselName = vessel.VesselName;
            vesselEntity.HullModel = vessel.HullModel;
            vesselEntity.VesselType = vessel.VesselType;

            _vesselRepository.Update(vesselEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Gemi bilgileri güncellenirken bir hata oluştu");
            }

            var vesselFeatures = _vesselFeatureRepository.GetAll(x => x.VesselId == vessel.Id).ToList();
            foreach (var vesselFeature in vesselFeatures)
            {
                _vesselFeatureRepository.Delete(vesselFeature, false);
            }

            foreach (var featureId in vessel.NavigationFeatureIds)
            {
                var vesselFeature = new VesselFeatureEntity
                {
                    VesselId = vesselEntity.Id,
                    FeatureId = featureId
                };
                _vesselFeatureRepository.Add(vesselFeature);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Gemi bilgileri güncellenirken bir hata oluştu, işlemler geri alındı");
            }

            return new ServiceMessage
            {
                IsSucceeded = true
            };
        }
    }
}
