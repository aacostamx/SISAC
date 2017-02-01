//------------------------------------------------------------------------
// <copyright file="PolicyService.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using AutoMapper;
    using Newtonsoft.Json;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Web.Models.VO.Process;

    /// <summary>
    /// Policy service
    /// </summary>
    public class PolicyService : IPolicyService
    {
        /// <summary>
        /// The jet fuel policy
        /// </summary>
        private readonly IJetFuelPolicyBusiness jetFuelPolicy;

        /// <summary>
        /// The national jet fuel policy
        /// </summary>
        private readonly INationalJetFuelPolicyBusiness nationalJetFuelPolicy;

        /// <summary>
        /// The national jet fuel policy
        /// </summary>
        private readonly INationalJetFuelInvoicePolicyBusiness invoicePolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyService" /> class.
        /// </summary>
        /// <param name="jetFuelPolicy">The jet fuel policy.</param>
        /// <param name="nationalJetFuelPolicy">The national jet fuel policy.</param>
        public PolicyService(
            IJetFuelPolicyBusiness jetFuelPolicy, 
            INationalJetFuelPolicyBusiness nationalJetFuelPolicy,
            INationalJetFuelInvoicePolicyBusiness invoicePolicy)
        {
            this.jetFuelPolicy = jetFuelPolicy;
            this.nationalJetFuelPolicy = nationalJetFuelPolicy;
            this.invoicePolicy = invoicePolicy;
        }

        /// <summary>
        /// Sends the policy to sap.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <param name="information">The information.</param>
        /// <returns>
        /// List of policies with the response from the service.
        /// </returns>
        public IList<JetFuelPolicyVO> SendPolicyToService(long policyId, ref string information, ref string status)
        {
            if (policyId <= 0)
            {
                return null;
            }

            try
            {
                // Variables to store thet data that will be sent
                List<JetFuelPolicyDto> policyDto = new List<JetFuelPolicyDto>();
                List<JetFuelPolicyVO> policyVo = new List<JetFuelPolicyVO>();

                // Variable to store the result from the response of the service
                List<JetFuelPolicyVO> policiesResponse = new List<JetFuelPolicyVO>();

                // Gets all the policies that will be sent
                policyDto = this.jetFuelPolicy.GetUnsuccessful(policyId).ToList();

                if (policyDto == null || policyDto.Count == 0)
                {
                    information = "No documents found.";
                    return policiesResponse;
                }

                policyVo = Mapper.Map<List<JetFuelPolicyDto>, List<JetFuelPolicyVO>>(policyDto);

                // Gets the documents' group numbers
                List<int> documentNumbers = policyVo.Select(c => c.DocumentNumber).Distinct().ToList<int>();
                foreach (int document in documentNumbers)
                {
                    // Gets the policies for each document number
                    List<PolicyRequestInformation> requestInformation =
                        Mapper.Map<List<PolicyRequestInformation>>(policyVo.Where(c => c.DocumentNumber == document).ToList());

                    // Creates the structure for the request body and sends it to the service
                    PolicyDocument request = SendInformation(requestInformation);
                    
                    // Gets the reponse information
                    policiesResponse.Add(new JetFuelPolicyVO
                    {
                        PolicyId = policyId,
                        DocumentNumber = document,
                        IDREG = request.T_DOCUMENTO.FirstOrDefault().IDREG,
                        BELNR = request.T_DOCUMENTO.FirstOrDefault().BELNR,
                        XBLNR = request.T_DOCUMENTO.FirstOrDefault().XBLNR == null ? string.Empty : request.T_DOCUMENTO.FirstOrDefault().XBLNR,
                        MENV = request.T_DOCUMENTO.FirstOrDefault().MENV,
                        MSGID = request.T_DOCUMENTO.FirstOrDefault().MSGID,
                        RFCLOG = ConcatCommentFromResponse(request.T_DOCUMENTO)
                    });
                }

                int total, succes, error;
                total = policyVo.Select(c => c.DocumentNumber).Distinct().Count();
                succes = policyVo.Where(c => c.MENV == "S").Count();
                error = policyVo.Where(c => c.MENV == "E").Count();

                information = "Total documents sent: "
                    + total
                    + ". Success: "
                    + succes
                    + ". Errors: "
                    + error;

                //Valida Status Sent o Error
                if (total > 0 && total == succes)
                {
                    status = "SENT";
                }

                if (total > 0 && total != succes)
                {
                    status = "ERROR";
                }

                return policiesResponse;
            }
            catch (BusinessException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
            catch (TimeoutException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
            catch (WebException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
        }

        /// <summary>
        /// Sends the national policy to sap.
        /// </summary>
        /// <param name="nationalPolicyId">The policy identifier.</param>
        /// <param name="information">The information.</param>
        /// <returns>
        /// List of policies with the response from the service.
        /// </returns>
        public IList<NationalJetFuelPolicyVO> SendNationalPolicyToService(long nationalPolicyId, ref string information, ref string status)
        {
            if (nationalPolicyId <= 0)
            {
                return null;
            }

            try
            {
                // Variables to store thet data that will be sent
                List<NationalJetFuelPolicyDto> nationalPolicyDto = new List<NationalJetFuelPolicyDto>();
                List<NationalJetFuelPolicyVO> nationalPolicyVo = new List<NationalJetFuelPolicyVO>();

                // Variable to store the result from the response of the service
                List<NationalJetFuelPolicyVO> nationalPoliciesResponse = new List<NationalJetFuelPolicyVO>();

                // Gets all the policies that will be sent
                nationalPolicyDto = this.nationalJetFuelPolicy.GetUnsuccessfulNationalPolicies(
                    new NationalJetFuelPolicyDto() 
                    { 
                        NationalPolicyID = nationalPolicyId 
                    }).ToList();

                if (nationalPolicyDto == null || nationalPolicyDto.Count == 0)
                {
                    information = "No documents found.";
                    return nationalPoliciesResponse;
                }

                nationalPolicyVo = Mapper.Map<List<NationalJetFuelPolicyDto>, List<NationalJetFuelPolicyVO>>(nationalPolicyDto);

                // Gets the documents' group numbers
                List<int> documentNumbers = nationalPolicyVo.Select(c => c.DocumentNumber).Distinct().ToList<int>();
                foreach (int document in documentNumbers)
                {
                    // Gets the policies for each document number
                    List<PolicyRequestInformation> requestInformation =
                        Mapper.Map<List<PolicyRequestInformation>>(nationalPolicyVo.Where(c => c.DocumentNumber == document).ToList());

                    // Creates the structure for the request body and sends it to the service
                    PolicyDocument request = SendInformation(requestInformation);

                    // Gets the reponse information
                    nationalPoliciesResponse.Add(new NationalJetFuelPolicyVO
                    {
                        NationalPolicyID = nationalPolicyId,
                        DocumentNumber = document,
                        IDREG = request.T_DOCUMENTO.FirstOrDefault().IDREG,
                        BELNR = request.T_DOCUMENTO.FirstOrDefault().BELNR,
                        XBLNR = request.T_DOCUMENTO.FirstOrDefault().XBLNR == null ? string.Empty : request.T_DOCUMENTO.FirstOrDefault().XBLNR,
                        MENV = request.T_DOCUMENTO.FirstOrDefault().MENV,
                        MSGID = request.T_DOCUMENTO.FirstOrDefault().MSGID,
                        RFCLOG = ConcatCommentFromResponse(request.T_DOCUMENTO)
                    });
                }

                int total, succes, error;
                total = nationalPolicyVo.Select(c => c.DocumentNumber).Distinct().Count();
                succes = nationalPoliciesResponse.Where(c => c.MENV == "S").Count();
                error = nationalPoliciesResponse.Where(c => c.MENV == "E").Count();

                information = "Total documents sent: "
                    + total
                    + ". Success: "
                    + succes
                    + ". Errors: "
                    + error;


                //Valida Status Sent o Error
                if (total > 0 && total == succes)
                {
                    status = "SENT";
                }

                if (total > 0 && total != succes)
                {
                    status = "ERROR";                
                }


                return nationalPoliciesResponse;
            }
            catch (BusinessException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
            catch (TimeoutException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
            catch (WebException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
        }


        /// <summary>
        /// Sends the national invoice policy to service.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <param name="information">The information.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException">
        /// See inner exception
        /// or
        /// See inner exception
        /// or
        /// See inner exception
        /// </exception>
        public IList<NationalJetFuelInvoicePolicyVO> SendNationalInvoicePolicyToService(string remittanceId, string monthYear, string period, ref string information)
        {
            if (string.IsNullOrWhiteSpace(remittanceId))
            {
                return null;
            }

            try
            {
                // Variables to store thet data that will be sent
                List<NationalJetFuelInvoicePolicyDto> invoicePolicyDto = new List<NationalJetFuelInvoicePolicyDto>();
                List<NationalJetFuelInvoicePolicyVO> invoicePolicyVo = new List<NationalJetFuelInvoicePolicyVO>();

                // Gets the policies for the remittance
                invoicePolicyDto = this.invoicePolicy.GetPoliciesByRemittanceId(remittanceId, monthYear, period).ToList();

                // Variable to store the result from the response of the service
                List<NationalJetFuelInvoicePolicyVO> invoicePoliciesResponse = new List<NationalJetFuelInvoicePolicyVO>();
                invoicePolicyVo = Mapper.Map<List<NationalJetFuelInvoicePolicyDto>, List<NationalJetFuelInvoicePolicyVO>>(invoicePolicyDto);

                // Gets the documents' group numbers
                List<int> documentNumbers = invoicePolicyVo.Select(c => c.DocumentNumber).Distinct().ToList<int>();
                foreach (int document in documentNumbers)
                {
                    // Gets the policies for each document number
                    List<PolicyRequestInformation> requestInformation =
                        Mapper.Map<List<PolicyRequestInformation>>(invoicePolicyVo.Where(c => c.DocumentNumber == document).ToList());

                    // Creates the structure for the request body and sends it to the service
                    PolicyDocument request = SendInformation(requestInformation);

                    // Gets the reponse information
                    invoicePoliciesResponse.Add(new NationalJetFuelInvoicePolicyVO
                    {
                        RemittanceId = remittanceId,
                        MonthYear = monthYear,
                        Period = period,
                        DocumentNumber = document,
                        IDREG = request.T_DOCUMENTO.FirstOrDefault().IDREG,
                        BELNR = request.T_DOCUMENTO.FirstOrDefault().BELNR,
                        XBLNR = request.T_DOCUMENTO.FirstOrDefault().XBLNR == null ? string.Empty : request.T_DOCUMENTO.FirstOrDefault().XBLNR,
                        MENV = request.T_DOCUMENTO.FirstOrDefault().MENV,
                        MSGID = request.T_DOCUMENTO.FirstOrDefault().MSGID,
                        RFCLOG = ConcatCommentFromResponse(request.T_DOCUMENTO)
                    });
                }

                information = "Total documents sent: "
                    + invoicePolicyVo.Select(c => c.DocumentNumber).Distinct().Count()
                    + ". Success: "
                    + invoicePoliciesResponse.Where(c => c.MENV == "S").Count()
                    + ". Errors: "
                    + invoicePoliciesResponse.Where(c => c.MENV == "E").Count();
                return invoicePoliciesResponse;
            }
            catch (BusinessException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
            catch (TimeoutException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
            catch (WebException exception)
            {
                throw new BusinessException("See inner exception", exception);
            }
        }

        /// <summary>
        /// Gets the message from response.
        /// </summary>
        /// <param name="responses">The responses.</param>
        /// <returns>The concatenated messages.</returns>
        private static string ConcatCommentFromResponse(IList<PolicyResponseInformation> responses)
        {
            if (responses == null)
            {
                return string.Empty;
            }

            string message = string.Empty;
            foreach (PolicyResponseInformation item in responses)
            {
                message = message + item.MSGID + ". " + item.RFCLOG + "\n";
            }

            return message;
        }

        /// <summary>
        /// Sends the rest service.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Policy document with the response information.</returns>
        private static PolicyDocument SendInformation(IList<PolicyRequestInformation> requestInformation)
        {
            PolicyDocument policiesRequest = new PolicyDocument();
            PolicyDocument policiesResponse = new PolicyDocument();
            
            // Information that will be sent
            policiesRequest.T_POLIZA = requestInformation;
            string policyRequest = JsonConvert.SerializeObject(policiesRequest);
            byte[] data = UTF8Encoding.UTF8.GetBytes(policyRequest);
            
            // Build of the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Resources.Resource.UrlWebServiceSapSendPolicy);
            request.Method = "POST";
            request.ContentType = "application/json charset=utf-8";
            request.Date = DateTime.Now;
            request.KeepAlive = true;
            request.Timeout = 120000;
            ServicePointManager.ServerCertificateValidationCallback = delegate(
                object s,
                X509Certificate certificate,
                X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(data, 0, data.Length);
            }

            // Calling the web service
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string body = reader.ReadToEnd();
                policiesResponse = JsonConvert.DeserializeObject<PolicyDocument>(body);
            }

            if (policiesResponse == null)
            {
                throw new WebException("An error in the structure of the body request.", WebExceptionStatus.SendFailure);
            }

            return policiesResponse;
        }
    }
}