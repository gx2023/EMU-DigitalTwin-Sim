using EMU.DT.DataService.Data;
using EMU.DT.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace EMU.DT.DataService.Services
{
    public class ProcessCardServiceImpl : IProcessCardService
    {
        private readonly DataDbContext _dbContext;

        public ProcessCardServiceImpl(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProcessCard>> GetAllProcessCardsAsync()
        {
            return await _dbContext.ProcessCards
                .Include(pc => pc.Steps)
                .ToListAsync();
        }

        public async Task<ProcessCard> GetProcessCardByIdAsync(int id)
        {
            return await _dbContext.ProcessCards
                .Include(pc => pc.Steps)
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<ProcessCard> CreateProcessCardAsync(ProcessCard card)
        {
            _dbContext.ProcessCards.Add(card);
            await _dbContext.SaveChangesAsync();
            return card;
        }

        public async Task<ProcessCard> UpdateProcessCardAsync(ProcessCard card)
        {
            _dbContext.ProcessCards.Update(card);
            await _dbContext.SaveChangesAsync();
            return card;
        }

        public async Task<bool> DeleteProcessCardAsync(int id)
        {
            var card = await _dbContext.ProcessCards.FindAsync(id);
            if (card == null)
            {
                return false;
            }

            _dbContext.ProcessCards.Remove(card);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProcessCard>> GetProcessCardsByVehicleModelAsync(string vehicleModel)
        {
            return await _dbContext.ProcessCards
                .Where(pc => pc.VehicleModel == vehicleModel)
                .Include(pc => pc.Steps)
                .ToListAsync();
        }

        public async Task<List<ProcessCard>> GetProcessCardsByTypeAsync(string type)
        {
            return await _dbContext.ProcessCards
                .Where(pc => pc.Type == type)
                .Include(pc => pc.Steps)
                .ToListAsync();
        }

        public async Task<List<ProcessStep>> GetProcessStepsAsync(int processCardId)
        {
            return await _dbContext.ProcessSteps
                .Where(ps => ps.ProcessCardId == processCardId)
                .OrderBy(ps => ps.StepNumber)
                .ToListAsync();
        }

        public async Task<ProcessStep> CreateProcessStepAsync(ProcessStep step)
        {
            _dbContext.ProcessSteps.Add(step);
            await _dbContext.SaveChangesAsync();
            return step;
        }

        public async Task<ProcessStep> UpdateProcessStepAsync(ProcessStep step)
        {
            _dbContext.ProcessSteps.Update(step);
            await _dbContext.SaveChangesAsync();
            return step;
        }

        public async Task<bool> DeleteProcessStepAsync(int id)
        {
            var step = await _dbContext.ProcessSteps.FindAsync(id);
            if (step == null)
            {
                return false;
            }

            _dbContext.ProcessSteps.Remove(step);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}