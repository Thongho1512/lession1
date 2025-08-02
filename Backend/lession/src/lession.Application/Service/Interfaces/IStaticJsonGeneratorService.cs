using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lession.Application.Service.Interfaces
{
    public interface IStaticJsonGeneratorService
    {
        Task GenerateKhachHangJsonAsync();
        Task GenerateSanPhamJsonAsync();
        Task GenerateAllJsonFilesAsync();
    }
}
