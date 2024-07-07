using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseApp.Services
{
    public class SecureStorageService : ISecureStorageService
    {
        public async Task<string> GetAsync(string key)
        {
            return await SecureStorage.GetAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await SecureStorage.SetAsync(key, value);
        }
    }

}
