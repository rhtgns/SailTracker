using SailTracker.Business.Operations.Feature;
using SailTracker.Business.Operations.Feature.Dtos;
using SailTracker.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SailTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationFeaturesController : ControllerBase
    {
        private readonly INavigationFeatureService _navigationFeatureService;

        public NavigationFeaturesController(INavigationFeatureService navigationFeatureService)
        {
            _navigationFeatureService = navigationFeatureService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNavigationFeature(NavigationFeatureRequest request)
        {
            var addNavigationFeatureDto = new AddNavigationFeatureDto
            {
                FeatureName = request.FeatureName
            };

            var result = await _navigationFeatureService.AddNavigationFeature(addNavigationFeatureDto);

            if (result.IsSucceeded)
                return Ok();
            else
                return BadRequest(result.Message);
        }
    }
}
