using Microsoft.AspNetCore.Mvc;
using Oms.Application.Orders;
using Oms.HttpApi.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Oms.HttpApi
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : AbpController
    {
        private readonly IOutboundOrderAppService outboundOrderService;

        public FeedbackController(IOutboundOrderAppService outboundOrderService)
        {
            this.outboundOrderService = outboundOrderService;
        }

        // POST api/feedback/checkstock
        [HttpPost("checkstock")]
        public async Task<IActionResult> ReceiveCheckStockResult([FromBody]CheckstockResultDto payload)
        {
            var result = await outboundOrderService.SetCheckStockResultAsync(payload);
            return Ok(result);
        }

        // POST api/feedback/wms
        [HttpPost("wms")]
        public async Task<IActionResult> ReceiveWmsProcessingStatus()
        {
            throw new NotImplementedException();
        }

        // POST api/feedback/tms
        [HttpPost("tms")]
        public async Task<IActionResult> ReceiveTmsProcessingStatus()
        {
            throw new NotImplementedException();
        }
    }
}
