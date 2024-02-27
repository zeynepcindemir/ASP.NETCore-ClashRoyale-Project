using Microsoft.AspNetCore.Mvc;
using WebApiProject.Dto;
using WebApiProject.Repository;



namespace WebApiProject.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        public CardsController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            try
            {
                var cards = await _cardRepository.GetAllCards();
                return Ok(cards);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpGet("{id}", Name = "CardById")]
        public async Task<IActionResult> GetCard(int id)
        {
            try
            {
                var card = await _cardRepository.GetCardById(id);
                if (card == null)
                {
                    return NotFound();
                }
                return Ok(card);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardDto card)
        {
            try
            {
                var createdCard = await _cardRepository.CreateCard(card);
                return CreatedAtRoute("CardById", new { id = createdCard.Id }, createdCard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, [FromBody] UpdateCardDto card)
        {
            try
            {
                var dbCard = await _cardRepository.GetCardById(id);
                if (dbCard == null)
                {
                    return NotFound();
                }
                await _cardRepository.UpdateCard(id, card);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            try
            {
                var dbCard = await _cardRepository.GetCardById(id);
                if (dbCard == null)
                {
                    return NotFound();
                }
                await _cardRepository.DeleteCard(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
