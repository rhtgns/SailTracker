using SailTracker.Business.Operations.Boat;
using SailTracker.Business.Operations.Boat.Dtos;
using SailTracker.WebApi.Filters;
using SailTracker.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SailTracker.Business.Operations.SailTracker;

namespace SailTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VesselsController : ControllerBase
    {
        private readonly IVesselService _vesselService;

        public VesselsController(IVesselService vesselService)
        {
            _vesselService = vesselService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVessel(int id)
        {
            var vessel = await _vesselService.GetVessel(id);
            if (vessel is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vessel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVessels()
        {
            var vessels = await _vesselService.GetVessels();
            return Ok(vessels);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddVessel(AddVesselRequest request)
        {
            var addVesselDto = new AddVesselDto
            {
                VesselName = request.VesselName,
                HullModel = request.HullModel,
                EstimatedValue = request.EstimatedValue,
                VesselType = request.VesselType,
                NavigationFeatureIds = request.NavigationFeatureIds
            };
            var result = await _vesselService.AddVessel(addVesselDto);

            if (!result.IsSucceeded)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPatch("{id}/value")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdjustEstimatedValue(int id, int changeTo)
        {
            var result = await _vesselService.AdjustEstimatedValue(id, changeTo);
            if (!result.IsSucceeded)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVessel(int id)
        {
            var result = await _vesselService.DeleteVessel(id);
            if (!result.IsSucceeded)
                return NotFound();
            else
                return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdateVessel(int id, UpdateVesselRequest request)
        {
            var updateVesselDto = new UpdateVesselDto
            {
                Id = id,
                VesselName = request.VesselName,
                HullModel = request.HullModel,
                EstimatedValue = request.EstimatedValue,
                VesselType = request.VesselType,
                NavigationFeatureIds = request.NavigationFeatureIds
            };

            var result = await _vesselService.UpdateVessel(updateVesselDto);

            if (!result.IsSucceeded)
            {
                return NotFound(result.Message);
            }
            else
            {
                return await GetVessel(id);
            }
        }
    }
}
