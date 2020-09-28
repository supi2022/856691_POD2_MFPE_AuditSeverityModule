using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AuditSeverityModule.Models;
using AuditSeverityModule.Providers;
using AuditSeverityModule.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditSeverityModule.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditSeverityController : ControllerBase
    {
        private readonly ISeverityProvider objProvider;
        public AuditSeverityController(ISeverityProvider _objProvider)
        {
            objProvider = _objProvider;
        }
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
                
        [HttpPost]
        public IActionResult Post([FromBody]AuditRequest req)
        {
            _log4net.Info(" Http POST request from "+nameof(AuditSeverityController));
            if (req == null)
                return BadRequest();

            if (req.Auditdetails.Type != "Internal" && req.Auditdetails.Type != "SOX")
                return BadRequest("You have given wrong audit type");

            try
            {
                var response = objProvider.SeverityResponse(req);                
                return Ok(response);
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured "+e.Message+" from " + nameof(AuditSeverityController));
                return StatusCode(500);
            }
            
        }
        
    }
}
//Changed
