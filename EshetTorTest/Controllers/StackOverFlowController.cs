using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace EshetTorTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StackOverFlowController : Controller
    {
        private IHttpClientFactory _clientFactory;
        private readonly ILogger<StackOverFlowController> _logger;

        public StackOverFlowController(IHttpClientFactory clientFactory,
            ILogger<StackOverFlowController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet("GetBestQuestions")]
        public async Task<IActionResult> GetBestQuestions()
        {
            try
            {
                var responce = await MakeRequest("https://api.stackexchange.com/2.3/questions?key=U4DMV*8nvpm3EOpvf69Rxw((&site=stackoverflow&order=desc&sort=votes&filter=default");

                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpGet("GetBestAnswer")]
        public async Task<IActionResult> GetBestAnswer(int questionId)
        {
            try
            {
                var responce = await MakeRequest($"https://api.stackexchange.com/2.3/answers/{questionId}?key=U4DMV*8nvpm3EOpvf69Rxw((&site=stackoverflow&order=desc&sort=votes&filter=default");

                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        [HttpGet("GetOwner")]
        public async Task<IActionResult> GetOwner(int answeredId)
        {
            try
            {
                var responce = await MakeRequest($"https://api.stackexchange.com/2.3/answers/{answeredId}?key=U4DMV*8nvpm3EOpvf69Rxw((&site=stackoverflow&page=1&order=desc&sort=activity&filter=default");
                var dataObject = JsonSerializer.Deserialize<DataObject>(responce);

                return Ok(dataObject.items.First().owner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest();
            }
        }

        private async Task<string> MakeRequest(string request)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var httpClient = new HttpClient(handler))
            {
                var apiUrl = (request);

                httpClient.BaseAddress = new Uri(apiUrl);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetStringAsync(apiUrl);

                return response;
            }
        }
    }
}
