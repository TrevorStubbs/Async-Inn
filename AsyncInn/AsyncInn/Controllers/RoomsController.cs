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
    public class RoomsController : ControllerBase
    {
        private IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "TopLevelPrivileges")]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO room)
        {
            await _room.Create(room);

            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        // GET: api/Rooms
        [HttpGet]
        [Authorize(Policy = "ElevatedPrivileges")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            List<RoomDTO> rooms = await _room.GetRooms();
            return rooms;
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [Authorize(Policy = "ElevatedPrivileges")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            RoomDTO room = await _room.GetRoom(id);
            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "ElevatedPrivileges")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

            var updateRoom = await _room.Update(room , id);

            return Ok(updateRoom);
        }

        // POST: api/Rooms/5/5
        [HttpPost]
        [Route("{roomId}/{amenityId}")]
        public async Task<IActionResult> PostAmenityToRoom(int roomId, int amenityId)
        {
            await _room.AddRoomAmenity(amenityId, roomId);
            return Ok();
        }

        // DELETE: api/Rooms/5/5
        [HttpDelete]
        [Route("{roomId}/{amenityId}")]
        public async Task<IActionResult> RemoveAmenityFromRoom(int amenityId, int roomId)
        {
            await _room.RemoveAmenityFromRoom(amenityId, roomId);
            return Ok();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "TopLevelPrivileges")]
        public async Task<ActionResult<RoomDTO>> DeleteRoom(int id)
        {
            await _room.Delete(id);

            return NoContent();
        }
    }
}
