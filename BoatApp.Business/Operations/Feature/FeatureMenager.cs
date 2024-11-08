using SailTracker.Business.Operations.Feature.Dtos;
using SailTracker.Business.Types;
using SailTracker.Data.Repositories;
using SailTracker.Data.UnitOfWork;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SailTracker.Business.Operations.Feature
{
    public class NavigationFeatureManager(IUnitOfWork unitOfWork, IRepository<FeatureEntity> repository) : INavigationFeatureService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IRepository<FeatureEntity> _repository = repository;

        public async Task<ServiceMessage> AddNavigationFeature(AddNavigationFeatureDto feature)
        {
            var hasFeature = _repository.GetAll(x => x.FeatureName.ToLower() == feature.FeatureName.ToLower()).Any();
            if (hasFeature)
            {
                return new ServiceMessage
                {
                    IsSucceeded = false,
                    Message = "Özellik zaten bulunuyor."
                };
            }

            var featureEntity = new FeatureEntity
            {
                FeatureName = feature.FeatureName
            };
            _repository.Add(featureEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Özellik kaydında bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucceeded = true
            };
        }
    }
}
