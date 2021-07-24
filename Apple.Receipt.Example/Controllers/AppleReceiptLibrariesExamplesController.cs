using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Receipt.Models;
using Apple.Receipt.Parser.Services;
using Apple.Receipt.Verificator.Models;
using Apple.Receipt.Verificator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Apple.Receipt.Example.Controllers
{
    [ApiController]
    [Route("apple-receipt")]
    public class AppleReceiptLibrariesExamplesController : ControllerBase
    {
        private readonly AppConfig _config;
        private readonly IAppleReceiptVerificatorService _appleVerificationService;
        private readonly IAppleReceiptParserService _appleParserService;

        public AppleReceiptLibrariesExamplesController(AppConfig config, 
            IAppleReceiptVerificatorService appleVerificationService, 
            IAppleReceiptParserService appleParserService)
        {
            _config = config;
            _appleVerificationService = appleVerificationService;
            _appleParserService = appleParserService;
        }

        [HttpGet("verificator")]
        public async Task<string> CheckVerificator()
        {
            AppleReceiptVerificationResult verificationResult = await _appleVerificationService.VerifyAppleReceiptAsync(_config.AppleReceiptExample);
            return JsonConvert.SerializeObject(verificationResult);
        }
        
        [HttpGet("parser")]
        public string CheckParser()
        {
            var data = Convert.FromBase64String(_config.AppleReceiptExample);
            AppleAppReceipt verificationResult = _appleParserService.GetAppleReceiptFromBytes(data);
            return JsonConvert.SerializeObject(verificationResult);
        }
    }
}
