using AutoMapper;
using lession.API.DTOs.KhachHang;
using lession.API.DTOs.SanPham;
using lession.Application.Service.Interfaces;
using lession.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace lession.Application.Service.Implementation
{
    public class StaticJsonGeneratorService : IStaticJsonGeneratorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<StaticJsonGeneratorService> _logger;
        private readonly string _dataPath;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IServiceScopeFactory _scopeFactory;

        public StaticJsonGeneratorService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHostEnvironment environment,
            ILogger<StaticJsonGeneratorService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
            _logger = logger;
            _scopeFactory = scopeFactory;

            // Configure data path
            _dataPath = Path.Combine(_environment.ContentRootPath, "wwwroot", "data");

            // Configure JSON options
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public async Task GenerateKhachHangJsonAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            try
            {
                _logger.LogInformation("Bắt đầu tạo file json cho khách hàng...");

                // Ensure directory exists
                EnsureDirectoryExists();

                // Get active customers only
                var khachHangs = await unitOfWork.KhachHangRepository
                    .GetAllAsync();

                // Map to DTOs
                var khachHangDtos = _mapper.Map<List<KhachHangDto>>(khachHangs);

                // Create wrapper object with metadata
                var jsonData = new
                {
                    generated = DateTime.UtcNow,
                    totalCount = khachHangDtos.Count,
                    data = khachHangDtos
                };

                // Serialize to JSON
                var json = JsonSerializer.Serialize(jsonData, _jsonOptions);

                // Write to file
                var filePath = Path.Combine(_dataPath, "khachhangs.json");
                await File.WriteAllTextAsync(filePath, json);

                _logger.LogInformation($"Thành công tạo json khách hàng với {khachHangDtos.Count} dòng.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi tạo khách hàng json.");
                throw;
            }
        }

        public async Task GenerateSanPhamJsonAsync()
        {
            using var scope = _scopeFactory.CreateScope(); // Tự động giải phóng tài nguyên sau khi sử dụng xong nhờ using
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            try
            {
                _logger.LogInformation("Bắt đầu tạo sản phẩm json...");

                // Ensure directory exists
                EnsureDirectoryExists();

                // Get active products only
                var sanPhams = await unitOfWork.SanPhamRepository
                    .GetAllAsync();

                // Map to DTOs
                var sanPhamDtos = _mapper.Map<List<SanPhamDto>>(sanPhams);

                // Create wrapper object with metadata
                var jsonData = new
                {
                    generated = DateTime.UtcNow,
                    totalCount = sanPhamDtos.Count,
                    data = sanPhamDtos
                };

                // Serialize to JSON
                var json = JsonSerializer.Serialize(jsonData, _jsonOptions);

                // Write to file
                var filePath = Path.Combine(_dataPath, "sanphams.json");
                await File.WriteAllTextAsync(filePath, json);

                _logger.LogInformation($"Thành công tạo sản phẩm json với {sanPhamDtos.Count} dòng.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi tạo sản phẩm json.");
                throw;
            }
        }

        public async Task GenerateAllJsonFilesAsync()
        {
            _logger.LogInformation("Bắt đầu tạo tắt cả file Json.");

            var tasks = new List<Task>
            {
                GenerateKhachHangJsonAsync(),
                GenerateSanPhamJsonAsync()
            };

            await Task.WhenAll(tasks);

            _logger.LogInformation("Thành công tạo tất cả file Json.");

        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
                _logger.LogInformation($"Tạo thư mục data tại: {_dataPath}");
            }
        }
    }
}

