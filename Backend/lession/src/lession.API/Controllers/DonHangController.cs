using lession.API.DTOs.DonHang;
using lession.Application.DTOs.Common;
using lession.Application.Service.Implementation;
using lession.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lession.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly IDonHangService _donHangService;
        private readonly ILogger<DonHangController> _logger;
        public DonHangController(IDonHangService donHangService, ILogger<DonHangController> logger)
        {
            _donHangService = donHangService;
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _donHangService.GetAllAsync();
        //    return result.Success ? Ok(result) : BadRequest(result);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _donHangService.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDonHangDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _donHangService.CreateAsync(request);
            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDonHangDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _donHangService.UpdateAsync(id, request);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donHangService.DeleteAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Get orders with pagination, searching, and sorting
        /// </summary>
        /// <param name="queryParameters">Query parameters for pagination, searching, and sorting</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] QueryParameters queryParameters)
        {
            var result = await _donHangService.GetPagedAsync(queryParameters);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
