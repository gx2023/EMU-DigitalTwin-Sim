using EMU.DT.DataService.Models;
using EMU.DT.DataService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMU.DT.DataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProcessCardController : ControllerBase
    {
        private readonly IProcessCardService _processCardService;

        public ProcessCardController(IProcessCardService processCardService)
        {
            _processCardService = processCardService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProcessCard>>> GetProcessCards()
        {
            var cards = await _processCardService.GetAllProcessCardsAsync();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessCard>> GetProcessCard(int id)
        {
            var card = await _processCardService.GetProcessCardByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public async Task<ActionResult<ProcessCard>> CreateProcessCard([FromBody] ProcessCard card)
        {
            var createdCard = await _processCardService.CreateProcessCardAsync(card);
            return CreatedAtAction(nameof(GetProcessCard), new { id = createdCard.Id }, createdCard);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProcessCard>> UpdateProcessCard(int id, [FromBody] ProcessCard card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            var updatedCard = await _processCardService.UpdateProcessCardAsync(card);
            return Ok(updatedCard);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProcessCard(int id)
        {
            var result = await _processCardService.DeleteProcessCardAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}/steps")]
        public async Task<ActionResult<List<ProcessStep>>> GetProcessSteps(int id)
        {
            var steps = await _processCardService.GetProcessStepsAsync(id);
            return Ok(steps);
        }

        [HttpPost("{id}/steps")]
        public async Task<ActionResult<ProcessStep>> AddProcessStep(int id, [FromBody] ProcessStep step)
        {
            if (id != step.ProcessCardId)
            {
                return BadRequest();
            }

            var addedStep = await _processCardService.AddProcessStepAsync(step);
            return CreatedAtAction(nameof(GetProcessSteps), new { id = id }, addedStep);
        }

        [HttpPut("steps/{id}")]
        public async Task<ActionResult<ProcessStep>> UpdateProcessStep(int id, [FromBody] ProcessStep step)
        {
            if (id != step.Id)
            {
                return BadRequest();
            }

            var updatedStep = await _processCardService.UpdateProcessStepAsync(step);
            return Ok(updatedStep);
        }
    }
}