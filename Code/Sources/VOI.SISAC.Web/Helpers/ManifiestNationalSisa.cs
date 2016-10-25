//------------------------------------------------------------------------
// <copyright file="AuthenticateUserHelper.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Helpers
{

    #region ~~~ Usings ~~~
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using Newtonsoft.Json;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    #endregion ~~~ End of Usings ~~~

    public static class ManifiestNationalSisa
    {

        public static ResponseWebServiceSisaVO ManifiestNationalGet(WebServiceSisaRequestVO webServiceSisaRequestVO)
        {
            ResponseWebServiceSisaVO responseWebServiceSisaVO = new ResponseWebServiceSisaVO();

            string URLConsumoWS = Resources.Resource.UrlWebServiceSISA;

            URLConsumoWS = URLConsumoWS + webServiceSisaRequestVO.DepartureStation + "/";
            URLConsumoWS = URLConsumoWS + webServiceSisaRequestVO.ArrivalStation + "/";
            URLConsumoWS = URLConsumoWS + webServiceSisaRequestVO.FlightNumber + "/";
            URLConsumoWS = URLConsumoWS + webServiceSisaRequestVO.MaticulaAeronave + "/";
            URLConsumoWS = URLConsumoWS + webServiceSisaRequestVO.DepartureDate;

            HttpWebRequest request = WebRequest.Create(URLConsumoWS) as HttpWebRequest;            
            request.Method = "GET";            
            request.ContentLength = 0;            
            request.ContentType = @"text/plain;charset=utf-8";
            

            /// Reswpuesta
            /// 

            try
            {

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("Error POST. Received HTTP {0}", response.StatusCode);
                        responseWebServiceSisaVO.ErrorDescription = message;
                        responseWebServiceSisaVO.Result = "Error";
                        //throw new ApplicationException(message);
                    }
                    else
                    {
                        string responseBody = ((new StreamReader(response.GetResponseStream())).ReadToEnd());

                        responseWebServiceSisaVO = JsonConvert.DeserializeObject<ResponseWebServiceSisaVO>(responseBody);


                    }
                }
            }
            catch (WebException webException)
            {

                string message = webException.Message.ToString();
                responseWebServiceSisaVO.ErrorDescription = message;
                responseWebServiceSisaVO.Result = "Error";

            }



            return responseWebServiceSisaVO;
        }

    }
}