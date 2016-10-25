<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerControl.ascx.cs" Inherits="VOI.SISAC.Web.Views.Shared.Report.ReportViewerControl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<div class="container">
    <form id="form1" runat="server">
        <div align="center">
            <asp:ScriptManager ID="scriptManager" runat="server" ScriptMode="Release" EnablePartialRendering="false" />
            <rsweb:ReportViewer ID="reportViewer" SizeToReportContent ="true" Width="100%" Height="100%" AutoSize="true" ShowPrintButton="true" KeepSessionAlive="true" runat="server" ProcessingMode="Remote">
                <ServerReport />
            </rsweb:ReportViewer>

        </div>
    </form>
</div>
<br />
