using SailTracker.Business.Operations.Feature.Dtos;
using SailTracker.Business.Types;
using SailTracker.Business.Operations.Feature.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Business.Operations.Feature
{
    public interface INavigationFeatureService
    {
        Task<ServiceMessage> AddNavigationFeature(AddNavigationFeatureDto feature);
    }
}
