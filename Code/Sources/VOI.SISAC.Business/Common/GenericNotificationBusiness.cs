//------------------------------------------------------------------------
// <copyright file="GenericNotificationBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Common
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using VOI.SISAC.Business.Dto.Generic;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;



    /// <summary>
    /// GenericNotificationBusiness class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Business.Common.IGenericNotificationBusiness" />
    public class GenericNotificationBusiness : IGenericNotificationBusiness
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericNotificationBusiness"/> class.
        /// </summary>
        public GenericNotificationBusiness()
        {

        }

        /// <summary>
        /// Sendings the email.
        /// </summary>
        /// <param name="myModel"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<string> SendingEmail(MailModelDto myModel)
        {
            IList<string> errorsSend = new List<string>();

            if (myModel != null)
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(myModel.SmtpClient);
                VOINotifications.ServiceNotificationsClient wcf = new VOINotifications.ServiceNotificationsClient();

                try
                {
                    var token = string.Empty;
                    token = wcf.GetAuthenticationToken(myModel.UserToken, myModel.PasswordToken);
                    wcf.SendEmailSubject(myModel.Subject, myModel.To, myModel.Body, true, token);
                }
                catch (SmtpException ex)
                {
                    errorsSend.Add(ex.InnerException.Message);
                }
                catch (Exception exception)
                {
                    throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
                }
            }
            return errorsSend;
        }

        /// <summary>
        /// Mails the configuration itinerary.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns></returns>
        public string MailConfigurationItinerary(ItineraryMailDto mail)
        {
            string Body = "";

            try
            {
                Body = ConfigMailItinerary(mail);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return Body;
        }

        /// <summary>
        /// Configurations the mail itinerary.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns></returns>
        private string ConfigMailItinerary(ItineraryMailDto mail)
        {
            var tableBody = string.Empty;
            var HtmlCode = string.Empty;

            try
            {
                foreach (var item in mail.ItineraryGroups)
                {
                    tableBody += "<tr>\n" +
                                "<td style=\'border: 1px solid #C0C0C0;padding: 5px;text-align: center;\'>&nbsp;" + item.ItineraryDate.ToShortDateString() + "</td>\n" +
                                "<td style=\'border: 1px solid #C0C0C0;padding: 5px;text-align: center;\'>&nbsp;" + item.TotalGroupProcess + "</td>\n" +
                                "<td style=\'border: 1px solid #C0C0C0;padding: 5px;text-align: center;\'>&nbsp;" + item.TotalGroupErrors + "</td>\n" +
                                "<td style=\'border: 1px solid #C0C0C0;padding: 5px;text-align: center;\'>&nbsp;" + item.TotalGroupFlights + "</td>\n" +
                                "</tr>\n";
                }

                HtmlCode = "<!DOCTYPE html PUBLIC \'-//W3C//DTD XHTML 1.0 Transitional//EN\' \'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\'>\n" +
                "<html xmlns=\'http://www.w3.org/1999/xhtml\'>\n" +
                "<head>\n" +
                "<meta http-equiv=\'Content-Type\' content=\'text/html; charset=UTF-8\'>\n" +
                "<meta name=\'viewport\' content=\'width=device-width, initial-scale=1\'>\n" +
                "<meta http-equiv=\'X-UA-Compatible\' content=\'IE=edge\'>\n" +
                "<meta name=\'format-detection\' content=\'telephone=no\'>\n" +
                "<title>\n" +
                "Volaris - Carga Automatica de Itinerario\n" +
                "</title>\n" +
                "<style type=\'text/css\'>\n" +
                "body \n" +
                "{margin: 0;padding: 0;-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}\n" +
                "table \n" +
                "{border-spacing: 0;}\n" +
                "table \n" +
                "td {border-collapse: collapse;}\n" +
                "table \n" +
                "{mso-table-lspace: 0pt;mso-table-rspace: 0pt;}\n" +
                "img \n" +
                "{-ms-interpolation-mode: bicubic;}\n" +
                "@media screen and (max-width: 599px) \n" +
                "{.force-row,.container {width: 100% !important;max-width: 100% !important;}}\n" +
                "@media screen and (max-width: 400px) \n" +
                "{.container-padding {padding-left: 12px !important;padding-right: 12px !important;}}\n" +
                "</style>\n" +
                "</head>\n" +
                "<body style=\'margin:0; padding:0;\' bgcolor=\'#F0F0F0\' leftmargin=\'0\' topmargin=\'0\' marginwidth=\'0\' marginheight=\'0\'>\n" +
                "<table border=\'0\' width=\'100%\' height=\'100%\' cellpadding=\'0\' cellspacing=\'0\' bgcolor=\'#F0F0F0\'>\n" +
                "<tbody>\n" +
                "<tr>\n" +
                "<td align=\'center\' valign=\'top\' bgcolor=\'#F0F0F0\' style=\'background-color: #F0F0F0;\'>\n" +
                "<br>\n" +
                "<table border=\'0\' width=\'600\' cellpadding=\'0\' cellspacing=\'0\' class=\'container\' style=\'width:600px;max-width:600px\'>\n" +
                "<tbody>\n" +
                "<tr>\n" +
                "</tr>\n" +
                "<tr>\n" +
                "<td class=\'container-padding content\' align=\'left\' style=\'padding-left:24px;padding-right:24px;padding-top:12px;padding-bottom:12px;background-color:#ffffff\'>\n" +
                "<br>\n" +
                "<div>\n" +
                "<img style=\'display: block;margin-left: auto;margin-right: auto\' alt=\'Volaris\' src=\'https://www.volaris.com/Static/img/volaris-new-logo.jpg\'>\n" +
                "</div>\n" +
                "<br>\n" +
                "<br>\n" +
                "<div class=\'title\' style=\'text-align: center; font-family:Helvetica, Arial, sans-serif;font-size:38px;font-weight:600;color:#000000;\'>\n" +
                "SISAC\n" +
                "</div>\n" +
                "<div class=\'title\' style=\'text-align: center; font-family:Helvetica, Arial, sans-serif;font-size:18px;font-weight:600;color:#000000;\'>\n" +
                "Detalle carga autom\u00e1tica de itinerario\n" +
                "</div>\n" +
                "<br>\n" +
                "<div class=\'body-text\' style=\'font-family:Helvetica, Arial, sans-serif;font-size:14px;line-height:20px;text-align:left;color:#333333\'>\n" +
                "<div style=\'text-align: center;\'>\n" +
                "<table style=\'margin-left: auto;margin-right: auto; border: 1px solid #C0C0C0;border-collapse: collapse;padding: 5px;\'>\n" +
                "<thead>\n" +
                "<tr>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;\'>Fecha Salida</th>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;\'>Procesados</th>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;\'>Errores</th>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;\'>Vuelos</th>\n" +
                "</tr>\n" +
                "</thead>\n" +
                "<tbody>\n" +
                "\n" +
                tableBody +
                "<tr>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;text-align: center;\'>&nbsp;Total</th>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;text-align: center;\'>&nbsp;" + mail.flightsProcessed + "</th>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;text-align: center;\'>&nbsp;" + mail.TotalErrors + "</th>\n" +
                "<th style=\'border: 1px solid #C0C0C0;padding: 5px;background: #F0F0F0;text-align: center;\'>&nbsp;" + mail.flightsFile + "</th>\n" +
                "</tr>\n" +
                "\n" +
                "</tbody>\n" +
                "</table>\n" +
                "</div>\n" +
                "<br>\n" +
                "<div style=\'text-align: center; \'>\n" +
                "<span style=\'color:#000000\'>Fecha de carga:\n" +
                "<br>\n" +
                "<span style=\'font-size:10.5pt color:#000000\'>\n" +
                "<b>\n" +
                "&nbsp;" + DateTime.Now.ToString() + "\n" +
                "</b> \n" +
                "</span>\n" +
                "</span>\n" +
                "</div>\n" +
                "<br>\n" +
                "</div>\n" +
                "</td>\n" +
                "</tr>\n" +
                "<tr>\n" +
                "<td class=\'container-padding footer-text\' align=\'left\' style=\'font-family:Helvetica, Arial, sans-serif;font-size:12px;line-height:16px;color:#aaaaaa;padding-left:24px;padding-right:24px\'>\n" +
                "<br>\n" +
                "<br> \u00ae " + DateTime.Now.Year.ToString() + " Volaris y su logo son marcas registradas de Volaris\n" +
                "<br>\n" +
                "<a href=\'https://www.volaris.com/\' style=\'color:#aaaaaa\'>https://www.volaris.com/</a>\n" +
                "<br>\n" +
                "<br>\n" +
                "<br>\n" +
                "</td>\n" +
                "</tr>\n" +
                "</tbody>\n" +
                "</table>\n" +
                "</td>\n" +
                "</tr>\n" +
                "</tbody>\n" +
                "</table>\n" +
                "</body>\n" +
                "</html>";
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

            return HtmlCode;

        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }

}
