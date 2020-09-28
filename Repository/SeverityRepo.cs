using AuditSeverityModule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuditSeverityModule.Repository
{
    public class SeverityRepo : ISeverityRepo
    {
        Uri baseAddress;    
        HttpClient client;
        IConfiguration config;
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SeverityRepo));
        public SeverityRepo(IConfiguration _config)
        {
            config = _config;
            baseAddress = new Uri(config["Links:Benchmark"]);
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        
        public List<AuditBenchmark> Response()
        {
            try
            {
                _log4net.Info(" Http POST request from " + nameof(SeverityRepo));
                List<AuditBenchmark> listFromAuditBenchmark = new List<AuditBenchmark>();

                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/AuditBenchmark").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    listFromAuditBenchmark = JsonConvert.DeserializeObject<List<AuditBenchmark>>(data);
                }
                return listFromAuditBenchmark;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured " + e.Message + " from " + nameof(SeverityRepo));
                return null;
            }           
            

        }

        public static List<AuditResponse> CriteriasfromRepository = new List<AuditResponse>()
        {
            new AuditResponse
            {
                RemedialActionDuration="No Action Needed",
                ProjectExexutionStatus="GREEN"
            },
            new AuditResponse
            {
                RemedialActionDuration="Action to be taken in 2 weeks",
                ProjectExexutionStatus="RED"
            }
        };
        
    }
}
