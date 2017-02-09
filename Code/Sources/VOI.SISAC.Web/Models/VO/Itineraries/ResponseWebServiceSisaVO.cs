//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Web.Models.VO.Itineraries
{

    #region ~~~ Usings ~~~

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion ~~~ End of Usings ~~~

    public class ResponseWebServiceSisaVO
    {

        #region ~~~ Fields ~~~

        private string result;
        private string errorNumber;
        private string errorDescription;
        private ManifestNationalVO data;

        #endregion ~~~ End of Fields ~~~

        #region ~~~ Atributes ~~~

        public string Result
        {
            get { return result; }
            set { result = value; }
        }

        public string ErrorNumber
        {
            get { return errorNumber; }
            set { errorNumber = value; }
        }

        public string ErrorDescription
        {
            get { return errorDescription; }
            set { errorDescription = value; }
        }

        public ManifestNationalVO Data
        {
            get { return data; }
            set { data = value; }
        }

        #endregion ~~~ End of Atributes ~~~
    }
}