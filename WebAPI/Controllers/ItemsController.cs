using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IDataStore<Item> _dataService;

        public ItemsController(IDataStore<Item> dataService)
        {
            _dataService = dataService;
        }

        // GET: api/<ItemController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            return Ok(await _dataService.GetItemsAsync(false));
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Item>> Get(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return  Ok(await _dataService.GetItemAsync(id));
        }

        // POST api/<ItemController>
        [HttpPost]
        public async Task<ActionResult<Item>> Post(Item item)
        {
            if (ModelState.IsValid)
            {
                await _dataService.AddItemAsync(item);
                return CreatedAtRoute(nameof(Get), new { id = item.Id }, item);
            }
            return BadRequest();
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Item item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            if (!_dataService.ItemExists(id))
            {
                return NotFound();
            }

            try
            {
                await _dataService.UpdateItemAsync(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();     // HTTP Status 204
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            if (!_dataService.ItemExists(id))
            {
                return NotFound();
            }

            await _dataService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
