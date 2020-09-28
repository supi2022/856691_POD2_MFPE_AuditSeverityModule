using AuditSeverityModule.Models;
using AuditSeverityModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityModule.Providers
{
    public class SeverityProvider : ISeverityProvider
    {
        ISeverityRepo objRepository;
        readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SeverityProvider));
        public SeverityProvider(ISeverityRepo _objRepository)
        {
            objRepository = _objRepository;
        }       

        public AuditResponse SeverityResponse(AuditRequest auditRequest)
        {            
            try
            {
                _log4net.Info(" Http POST request from " + nameof(SeverityProvider));
                List<AuditBenchmark> listfromRepository = objRepository.Response();               
                int count = 0, acceptableNo = 0;

                if (auditRequest.Auditdetails.questions.Question1 == false)
                    count++;
                if (auditRequest.Auditdetails.questions.Question2 == false)
                    count++;
                if (auditRequest.Auditdetails.questions.Question3 == false)
                    count++;
                if (auditRequest.Auditdetails.questions.Question4 == false)
                    count++;
                if (auditRequest.Auditdetails.questions.Question5 == false)
                    count++;

                if (auditRequest.Auditdetails.Type == listfromRepository[0].AuditType)
                    acceptableNo = listfromRepository[0].BenchmarkNoAnswers;
                else if (auditRequest.Auditdetails.Type == listfromRepository[1].AuditType)
                    acceptableNo = listfromRepository[1].BenchmarkNoAnswers;
                

                Random randomNumber = new Random();
                

                AuditResponse auditResponse = new AuditResponse();
                if (auditRequest.Auditdetails.Type == "Internal" && count <= acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = SeverityRepo.CriteriasfromRepository[0].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = SeverityRepo.CriteriasfromRepository[0].RemedialActionDuration;
                }
                else if (auditRequest.Auditdetails.Type == "Internal" && count > acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = SeverityRepo.CriteriasfromRepository[1].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = SeverityRepo.CriteriasfromRepository[1].RemedialActionDuration;
                }
                else if (auditRequest.Auditdetails.Type == "SOX" && count <= acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = SeverityRepo.CriteriasfromRepository[0].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = SeverityRepo.CriteriasfromRepository[0].RemedialActionDuration;
                }
                else if (auditRequest.Auditdetails.Type == "SOX" && count > acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = SeverityRepo.CriteriasfromRepository[1].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = SeverityRepo.CriteriasfromRepository[1].RemedialActionDuration;
                }


                return auditResponse;
            }
            catch(Exception e)
            {
                _log4net.Error("Exception Occured " + e.Message + " from " + nameof(SeverityProvider));
                return null;
            }
                  
        }
    }
}
