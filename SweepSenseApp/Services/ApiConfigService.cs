﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepSenseApp.Services
{
    public class ApiConfigService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiConfigService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public ApiConfigService()
        {
            _httpClient = new HttpClient();
            _baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://wl2w05k9-7210.euw.devtunnels.ms/api" : "https://localhost:7210/api";
        }

        public HttpClient HttpClient => _httpClient;
        public string BaseUrl => _baseUrl;
    }
}
