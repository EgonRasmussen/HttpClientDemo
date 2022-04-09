using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IDataStore<Item> _dataService;
    private readonly ILogger<ItemsController> _logger;

    int errorPercent = 100;  // Er tallet 0 returneres altid HTTP 200, jo større værdi jo større chanche for fejl. Vælges 100 vil den hver gang returnere HTTP 500.
    public ItemsController(IDataStore<Item> dataService, ILogger<ItemsController> logger)
    {
        _dataService = dataService;
        _logger = logger;
    }

    // GET: api/<ItemController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> Get()
    {
        Random rnd = new Random();
        int rndInteger = rnd.Next(1, 101);
        if (rndInteger <= errorPercent)
        {
            _logger.LogDebug("---> StatusCode 500");
            return StatusCode(StatusCodes.Status500InternalServerError, new List<Task>());
        }
        else
        {
            _logger.LogDebug("---> StatusCode 200");
            return Ok(await _dataService.GetItemsAsync(false));
        }
    }

    // GET api/<ItemController>/5
    [HttpGet("{id}", Name = "Get")]
    public async Task<ActionResult<Item>> Get(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return Ok(await _dataService.GetItemAsync(id));
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
