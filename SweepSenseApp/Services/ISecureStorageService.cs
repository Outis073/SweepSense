using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseApp.Services
{
    public interface ISecureStorageService
    {
        Task<string> GetAsync(string key);
        Task SetAsync(string key, string value);
    }

}
