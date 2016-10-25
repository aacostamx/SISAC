using System;
using System.Linq;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using VOI.SISAC.Web.Helpers;

namespace VOI.SISAC.Web.Views.Shared.Report
{
    public partial class ReportViewerControl : System.Web.Mvc.ViewUserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Required for report events to be handled properly.
            //reportViewer.ServerReport.ReportServerCredentials = new ReportServerCredentials();
            Context.Handler = Page;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReportingServiceViewModel model = (ReportingServiceViewModel)Model;
                reportViewer.ServerReport.ReportServerCredentials = model.ServerCredentials;
                ReportParameter[] RptParameters = model.parameters;

                reportViewer.ServerReport.ReportPath = model.ReportPath;
                reportViewer.ServerReport.ReportServerUrl = model.ReportServerURL;

                try
                {
                    if (RptParameters.Count() > 0)
                        this.reportViewer.ServerReport.SetParameters(RptParameters);

                    this.reportViewer.ServerReport.Refresh();
                }
                catch
                {
                }
            }    
        }
    }
}