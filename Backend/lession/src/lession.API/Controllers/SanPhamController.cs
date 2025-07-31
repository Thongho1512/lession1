using lession.API.DTOs.SanPham;
using lession.Application.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static lession.Application.DTOs.Common.QueryParameters;

namespace lession.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamService _sanPhamService;
        public SanPhamController(ISanPhamService sanPhamService)
        {
            _sanPhamService = sanPhamService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _sanPhamService.GetAllAsync();
        //    return result.Success ? Ok(result) : BadRequest(result);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sanPhamService.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSanPhamDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _sanPhamService.CreateAsync(request);
            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSanPhamDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _sanPhamService.UpdateAsync(id, request);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sanPhamService.DeleteAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> GetSanPhamIsSoftDeleted(int id)
        {
            var result = await _sanPhamService.ActiveSanPhamIsSoftDeleted(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // <summary>
        /// Get products with pagination, filtering, searching, and sorting
        /// </summary>
        /// <param name="queryParameters">Query parameters for pagination, filtering, searching, and sorting</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] ActiveQueryParameters queryParameters)
        {
            var result = await _sanPhamService.GetPagedAsync(queryParameters);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
