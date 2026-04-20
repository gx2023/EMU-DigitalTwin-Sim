using EMU.DT.DataService.Models;

namespace EMU.DT.DataService.Services
{
    public interface IProcessCardService
    {
        Task<List<ProcessCard>> GetAllProcessCardsAsync();
        Task<ProcessCard> GetProcessCardByIdAsync(int id);
        Task<ProcessCard> CreateProcessCardAsync(ProcessCard card);
        Task<ProcessCard> UpdateProcessCardAsync(ProcessCard card);
        Task<bool> DeleteProcessCardAsync(int id);
        Task<List<ProcessCard>> GetProcessCardsByVehicleModelAsync(string vehicleModel);
        Task<List<ProcessCard>> GetProcessCardsByTypeAsync(string type);
        Task<List<ProcessStep>> GetProcessStepsAsync(int processCardId);
        Task<ProcessStep> CreateProcessStepAsync(ProcessStep step);
        Task<ProcessStep> UpdateProcessStepAsync(ProcessStep step);
        Task<bool> DeleteProcessStepAsync(int id);
    }
}