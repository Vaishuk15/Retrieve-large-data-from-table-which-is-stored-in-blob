using AzureStorage.Models.ViewModel;
using AzureStorage.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableStorage : ControllerBase
    {
        private readonly ILogger<TableStorage> _logger;
        private readonly IAzureTableStorageService _tableService;

        public TableStorage(ILogger<TableStorage> logger, IAzureTableStorageService formService)
        {
            _logger = logger;
            _tableService = formService;
        }
        [HttpPost("AddSavedForm")]
        public async Task<IActionResult> AddSavedFormAsync([FromBody] SavedFormDemoViewModel input)
        {
            try
            {
                var result = await _tableService.AddToTable(input);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "AddSavedForm", input);
                throw;
            }
        }
    }
}
