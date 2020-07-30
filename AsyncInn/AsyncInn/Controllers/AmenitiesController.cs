using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "BottomLevelPrivileges")]
    public class AmenitiesController : ControllerBase
    {
        private IAmenities _amenity;

        public AmenitiesController(IAmenities amenity)
        {
            _amenity = amenity;
        }

        // POST: api/Amenities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(AmenityDTO amenity)
        {
            await _amenity.Create(amenity);

            return CreatedAtAction("GetAmenity", new { id = amenity.ID }, amenity);
        }

        // GET: api/Amenities
        [HttpGet]
        [Authorize(Policy = "ElevatedPrivileges")]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            List<AmenityDTO> amenities = await _amenity.GetAmenities();
            return amenities;

            // foreach(var item in list)
            // { aminites.add(GetAmity(await item.ID);}
            //
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ElevatedPrivileges")]
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            AmenityDTO amenity = await _amenity.GetAmenity(id);

            return amenity;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "ElevatedPrivileges")]
        public async Task<IActionResult> PutAmenity(int id, AmenityDTO amenity)
        {
            if (id != amenity.ID)
            {
                return BadRequest();
            }

            var updatedAmenity = await _amenity.Update(amenity, id);

            return Ok(updatedAmenity);
        }

        // DELETE: api/Amenities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AmenityDTO>> DeleteAmenity(int id)
        {            
            await _amenity.Delete(id);

            return NoContent();
        }
    }
}
