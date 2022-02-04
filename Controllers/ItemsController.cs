using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = (await _itemsRepository.GetItemsAsync())
            .Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _itemsRepository.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());
        }


        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto createItemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTime.UtcNow
            };

            await _itemsRepository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = await _itemsRepository.GetItemAsync(id);

            if(existingItem is null) return NotFound();

            Item updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price,
            };

            await _itemsRepository.UpdateItemAsync(updatedItem);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var existingItem = await _itemsRepository.GetItemAsync(id);

            if(existingItem is null) return NotFound();

            await _itemsRepository.DeleteItemAsync(existingItem.Id);

            return NoContent();
        }
    }
}