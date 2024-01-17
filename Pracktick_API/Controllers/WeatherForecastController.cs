using Microsoft.AspNetCore.Mvc;

namespace Pracktick_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAll", Name = "GetAllWeather")]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (sortStrategy == null)
            {
                return Ok(Summaries);
            }

            List<string> sortedList;
            switch (sortStrategy)
            {
                case 1:
                    sortedList = Summaries.OrderBy(s => s).ToList();
                    return Ok(sortedList);
                case -1:
                    sortedList = Summaries.OrderByDescending(s => s).ToList();
                    return Ok(sortedList);
                default:
                    return BadRequest("Некорректное значение параметра sortStrategy");
            }
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpGet("{index}")] 
        public ActionResult<string> GetByIndex(int index)
        {
            if (index >= 0 && index < Summaries.Count)
            {
                return Ok(Summaries[index]);
            }
            else
            {
                return NotFound("Индекс некоректен");
            }
        }

        [HttpGet("find-by-name")] 
        public ActionResult<int> GetCountByName(string name)
        {
            int count = Summaries.Count(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Ok(count);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if(index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Не коректный индекс");
            }
                
            Summaries[index] = name;
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            Summaries.RemoveAt(index);
            return Ok();
        }
    }
}
