using lession.API.DTOs.KhachHang;
using lession.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static lession.Application.DTOs.Common.QueryParameters;

namespace lession.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _khachHangService.GetAllAsync();
        //    return result.Success ? Ok(result) : BadRequest(result);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _khachHangService.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateKhachHangDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _khachHangService.CreateAsync(request);
            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateKhachHangDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _khachHangService.UpdateAsync(id, request);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _khachHangService.GetKhachHangIsDeleted(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _khachHangService.DeleteAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Get customers with pagination, filtering, searching, and sorting
        /// </summary>
        /// <param name="queryParameters">Query parameters for pagination, filtering, searching, and sorting</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] ActiveQueryParameters queryParameters)
        {
            var result = await _khachHangService.GetPagedAsync(queryParameters);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
