//------------------------------------------------------------------------
// <copyright file="GendecArrivalBusinnes.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Itineraries;
    using VOI.SISAC.Business.Common;
    using AutoMapper;
    using MapConfiguration;
    using ExceptionBusiness;
    using Resources;
    using System.Globalization;
    using Dto.Airports;
    using Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;
    using System.Collections.ObjectModel;
    using System.Net.Mail;

    public class GendecArrivalBusiness : IGendecArrivalBusiness
    {
        /// <summary>
        /// unit Of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Gendec Arrival Repository
        /// </summary>
        private readonly IGendecArrivalRepository gendecArrivalRepository;

        /// <summary>
        /// The itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// Get the methods of each repository
        /// </summary>
        /// <param name="unitOfWork">unit of work</param>
        /// <param name="gendecArrivalRepository">gendec arrival repository</param>
        public GendecArrivalBusiness(IUnitOfWork unitOfWork,
                                     IGendecArrivalRepository gendecArrivalRepository,
                                     IItineraryRepository itineraryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.gendecArrivalRepository = gendecArrivalRepository;
            this.itineraryRepository = itineraryRepository;
        }

        /// <summary>
        /// Get Gendec Arrvial of a Flight
        /// </summary>
        /// <param name="sequence">sequence</param>
        /// <param name="airlinecode">airline code</param>
        /// <param name="flightnumber">flight number</param>
        /// <param name="itinerarykey">itinerary key</param>
        /// <returns></returns>
        public GendecArrivalDto GetGendecArrival(int sequence, string airlinecode, string flightnumber, string itinerarykey)
        {
            if (string.IsNullOrWhiteSpace(airlinecode) || string.IsNullOrWhiteSpace(flightnumber) || string.IsNullOrWhiteSpace(itinerarykey))
            {
                return null;
            }

            try
            {
                GendecArrival gendecArrival = this.gendecArrivalRepository.GetGendecArrival(sequence, airlinecode, flightnumber, itinerarykey);
                GendecArrivalDto gendecArrivalDto = Mapper.Map<GendecArrival, GendecArrivalDto>(gendecArrival);
                return gendecArrivalDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Sendings the email.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        public IList<string> SendingEmail(GendecArrivalDto gendecArrivalDto)
        {
            IList<string> errors = new List<string>();
            //MailModelDto milModel = new MailModelDto();

            if (gendecArrivalDto != null)
            {
                //Obtener información de Usuario (Jefe de Aeropuerto) para obtener su Email
                MailMessage mail = new MailMessage();
                //  mail.To.Add("jalvarez@linko.mx");
                //  mail.From = new MailAddress("jalvarez@linko.mx");
                //  mail.Subject = "Sistema de Servicios Aeroportuarios";
                //string Body = this.MailConfiguration(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey);
                string Body = this.MailConfiguration(gendecArrivalDto);
                // mail.Body = Body;
                mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient("10.10.32.5");
                SmtpClient smtp = new SmtpClient("127.127.0.1");
                Notifications.ServiceNotificationsClient wcf = new Notifications.ServiceNotificationsClient();
                try
                {
                    string token = string.Empty;
                    token = wcf.GetAuthenticationToken("USERTEST", "Volaris19$");

                    /// wcf.SendEmailSubject("Sistema de Servicios Aeroportuarios", "ijuarez@linko.mx", Body, true, token);
                    wcf.SendEmailSubject("Sistema de Servicios Aeroportuarios", "alcabelu@gmail.com", Body, true, token);
                    wcf.SendEmailSubject("Sistema de Servicios Aeroportuarios", "fcoagr@gmail.com", Body, true, token);
                    wcf.SendEmailSubject("Sistema de Servicios Aeroportuarios", "leramirez@linko.mx", Body, true, token);

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    errors.Add(ex.InnerException.Message);
                }
                catch (Exception exception)
                {
                    throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
                }
            }
            return errors;
        }

        /// <summary>
        /// Validates the gendec arrival information.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        public IList<string> ValidateGendecArrivalInformation(GendecArrivalDto gendecArrivalDto)
        {
            IList<string> errors = new List<string>();
            GendecArrivalDto gendecArrivalDtoDB = new GendecArrivalDto();
            gendecArrivalDtoDB = Mapper.Map<GendecArrival, GendecArrivalDto>(this.gendecArrivalRepository.GetGendecArrival(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey));

            if (gendecArrivalDtoDB == null)
            {   
                    this.AddGendecArrival(gendecArrivalDto);
            }
            else
            {
                    this.UpdateGendecArrival(gendecArrivalDto);
            }
            return errors;
        }

        /// <summary>
        /// Adds the gendec arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool AddGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            GendecArrival gendecArrival = new GendecArrival();
            if (gendecArrivalDto == null)
            {
                return false;
            }
            try
            {
                gendecArrival = Mapper.Map<GendecArrivalDto, GendecArrival>(gendecArrivalDto);
                IList<Crew> crews = gendecArrival.Crews.ToList();
                gendecArrival.Crews = new List<Crew>();
                gendecArrival.Closed = false;
                this.gendecArrivalRepository.AddGendecArrival(gendecArrival, crews);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the gendec arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            try
            {
                GendecArrival gendecArrival = this.gendecArrivalRepository.GetGendecArrival(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey);
                gendecArrival.TotalPax = gendecArrivalDto.TotalPax;
                gendecArrival.TotalCrew = gendecArrivalDto.TotalCrew;
                gendecArrival.BlockTime = gendecArrivalDto.BlockTime;
                gendecArrival.ActualTimeOfArrival = gendecArrivalDto.ActualTimeOfArrival;
                gendecArrival.ManifestNumber = gendecArrivalDto.ManifestNumber;
                gendecArrival.GateNumber = gendecArrivalDto.GateNumber;
                gendecArrival.ArrivalPlace = gendecArrivalDto.ArrivalPlace;
                gendecArrival.AuthorizedAgent = gendecArrivalDto.AuthorizedAgent;
                gendecArrival.Remarks = gendecArrivalDto.Remarks;
                gendecArrival.Disembanking = gendecArrivalDto.Disembanking;
                gendecArrival.FlightArrivalDescription = gendecArrivalDto.FlightArrivalDescription;
                gendecArrival.Member = gendecArrivalDto.Member;
                this.gendecArrivalRepository.Update(gendecArrival);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Closes the gendec arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool CloseGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            try
            {
                GendecArrival gendecArrival = new GendecArrival();
                gendecArrivalDto.Itinerary = null;
                gendecArrival = Mapper.Map<GendecArrivalDto, GendecArrival>(gendecArrivalDto);
                gendecArrival.Closed = true;
                this.gendecArrivalRepository.Update(gendecArrival);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Opens the gendec arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool OpenGendecArrival(GendecArrivalDto gendecArrivalDto)
        {
            try
            {
                GendecArrival gendecArrival = new GendecArrival();
                gendecArrival = this.gendecArrivalRepository.GetGendecArrival(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey);
                gendecArrival.Closed = false;
                this.gendecArrivalRepository.Update(gendecArrival);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Mails the configuration.
        /// </summary>
        /// <param name="gendecArrivalDto">The gendec arrival dto.</param>
        /// <returns></returns>
        private string MailConfiguration(GendecArrivalDto gendecArrivalDto)
        {
            string Body = "";
            //string url = "http://localhost:3991/ServiceOpenManifestServices.svc/";
            string url = "http://10.10.32.62:966/Gendec.svc/OpenA/";
            string page = "open/"+ gendecArrivalDto + "";
            ItineraryDto itineraryDto = new ItineraryDto();
            itineraryDto = Mapper.Map<Itinerary, ItineraryDto>(this.itineraryRepository.FindById(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey));

            Body = ConfigMailManifiesto(url+page).Replace("###fo###", itineraryDto.GendecArrivals.ManifestNumber);
            Body = Body.Replace("###so###", "Test Solicitante");
            Body = Body.Replace("###no###", "1223233");
            Body = Body.Replace("###pe###", "ASC");
            Body = Body.Replace("###vu###", itineraryDto.FlightNumber);
            Body = Body.Replace("###od###", itineraryDto.DepartureStation + "-" + itineraryDto.ArrivalStation);
            Body = Body.Replace("###fe###", itineraryDto.ArrivalDate.ToShortDateString());
            Body = Body.Replace("###ho###", itineraryDto.ArrivalDate.ToShortTimeString());
            Body = Body.Replace("###ma###", itineraryDto.EquipmentNumber);
            Body = Body.Replace("###eq###", itineraryDto.Airplane.AirplaneModel);
            Body = Body.Replace("###de###", "");
            Body = Body.Replace("###ti###", "Llegada");
            Body = Body.Replace("###id###", "1");
            Body = Body.Replace("###Mn###", itineraryDto.GendecArrivals.ManifestNumber);
            Body = Body.Replace("###us###", "4");
            Body = Body.Replace("###ni###", "1");
            Body = Body.Replace("###ed###", "Jose Cruz");
            return Body;
        }

        /// <summary>
        /// Configurations the mail manifiesto.
        /// </summary>
        /// <param name="sitio">The sitio.</param>
        /// <returns></returns>
        private string ConfigMailManifiesto(string sitio)
        {
            StringBuilder strb = new StringBuilder();

            strb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
            strb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            strb.Append("<head>");
            strb.Append("<meta content='text/html; charset=utf-8'http-equiv='Content-Type'/>");
            strb.Append("<title>SISA</title>");
            strb.Append("<style type='text/css'>");
            strb.Append(" table.MsoNormalTable");
            strb.Append("{font-size:11.0pt; font-family:'Calibri','sans-serif';}");
            strb.Append("p.MsoNormal");
            strb.Append("{margin-bottom:.0001pt;font-size:12.0pt;font-family:'Times New Roman','serif';	margin-left: 0cm;margin-right: 0cm;	margin-top: 0cm;}");
            strb.Append("p");
            strb.Append("{margin-right:0cm;	margin-left:0cm;font-size:12.0pt;font-family:'Times New Roman','serif';	}");
            strb.Append("span.txtcontenidomorado1");
            strb.Append("{font-family:'Arial','sans-serif';	font-variant:normal !important;	color:#AB4FA1; text-transform:none; font-weight:bold; font-style:normal;}");
            strb.Append("span.txtcontenido31");
            strb.Append("{font-family:'Arial','sans-serif';	font-variant:normal !important;	color:#666666;	text-transform:none; font-weight:normal; font-style:normal;}");
            strb.Append("span.txtcontenidonegro1");
            strb.Append("{font-family:'Arial','sans-serif'; font-variant:normal !important;	color:black; text-transform:none; font-weight:normal; font-style:normal;}");
            strb.Append(".style1 { text-align: right; }");
            strb.Append(".style2 {text-align: left;}");
            strb.Append(".style3 {text-align: justify;}");
            strb.Append("</style>");
            strb.Append("</head>");
            strb.Append("<body>");
            strb.Append("<div align='center'>");
            strb.Append("<table border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='width:498.0pt;mso-cellspacing:0cm;mso-yfti-tbllook:1184;mso-padding-alt:  0cm 0cm 0cm 0cm' width='664'>");
            strb.Append("<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes'>");
            strb.Append("<td style='padding:0cm 0cm 0cm 0cm'>");
            strb.Append("<p class='MsoNormal' style='mso-line-height-alt:0pt'>");
            strb.Append("<span style='mso-fareast-font-family:&quot;Times New Roman&quot;'><br />");
            strb.Append("<br style='mso-special-character:line-break' />");
            strb.Append("<![if !supportLineBreakNewLine]>");
            strb.Append("<br style='mso-special-character:line-break' />");
            strb.Append("<![endif]><o:p></o:p></span></p>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<!--  HEADER -->");
            strb.Append("<tr style='mso-yfti-irow:1;height:36.75pt'>");
            strb.Append("<td style='width:483.0pt;padding:0cm 0cm 0cm 0cm;height:36.75pt' width='644'>");
            strb.Append("<p>");
            strb.Append("<img id='_x0000_i1025' alt=' Volaris ' border='0' height='49' src='http://imagen.volaris.com.mx/correos/promocion/2009/Noviembre/20091113_mailing_confirmacion_gift_cards/heder_gifcards.jpg' width='664' /></p>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:2;height:357.0pt'>");
            strb.Append("<td background='http://imagen.volaris.com.mx/correos/promocion/2009/Noviembre/20091113_mailing_confirmacion_gift_cards/back_mailing_gifcards_confirmacion.jpg' style='width:483.0pt;padding:0cm 0cm 0cm 0cm; height:357.0pt' valign='top' width='644'>");
            strb.Append("<table border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='width:498.0pt;mso-cellspacing:0cm;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm; height: 512px;' width='664'>");
            strb.Append("<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:22.5pt;padding:0cm 0cm 0cm 0cm' width='30'>");
            strb.Append("<p class='MsoNormal'>");
            strb.Append("<span style='mso-fareast-font-family:&quot;Times New Roman&quot;'>");
            strb.Append("&nbsp;<o:p></o:p></span></p>");
            strb.Append("</td>");
            strb.Append("<td style='width:453.0pt;padding:0cm 0cm 0cm 0cm' valign='top' width='604'>");
            strb.Append("<table border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='width:453.0pt;mso-cellspacing:0cm;mso-yfti-tbllook:1184;mso-padding-alt: 0cm 0cm 0cm 0cm' width='604'>");
            strb.Append("<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;'>");
            strb.Append("<td colspan='2' style='padding:0cm 0cm 0cm 0cm;height:56pt'>");
            strb.Append("<p align='center' class='MsoNormal' style='text-align:center; font-family: Arial, sans-serif; font-size: 13.5pt; color: #AB4FA1;'>");
            strb.Append("Autorización de Activación Manifiesto ###ti###</p>");
            strb.Append("<hr style='color:#AB4FA1'/>");
            strb.Append("<p align='center' class='MsoNormal' style='text-align:center'>");
            strb.Append("<o:p></o:p></p>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:263pt; padding:0cm 0cm 0cm 0cm' valign='top'>");
            strb.Append("<table border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='width:453.0pt;mso-cellspacing:0cm;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm; height: 294px;' width='604'>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:263pt; padding:0cm 0cm 0cm 0cm' valign='top'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:10.5pt'> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("<td style='width:263pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style1'>");
            strb.Append("<span class='txtcontenidomorado1'>Folio:");
            strb.Append("<span style='font-size:10.5pt'><b>&nbsp;###fo###</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:453pt; padding:0cm 0cm 0cm 0cm' valign='top' colspan='2'>");
            strb.Append("<table border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='width:400px; mso-cellspacing:0cm;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm' align='center'>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>Solicitante:</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###so###</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");//noEmpleado
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm; height: 17px;' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>No. empleado:</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm; height: 17px;' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###no###</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("</tr>");//
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm; height: 17px;' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>Perfil:</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm; height: 17px;' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###pe###</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm; height: 18px;' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'>");
            strb.Append("<b>No. vuelo:</b></span></span></td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm; height: 18px;' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###vu###</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'>");
            strb.Append("<b>ORIGEN - DESTINO:</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###od###</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm; height: 18px;' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'>");
            strb.Append("<b>Fecha ###de###:</b></span></span>&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm; height: 18px;' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###fe###</b> </span>");
            strb.Append("</span>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>Hora ###de###:</b></span></span></td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###ho###</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'>");
            strb.Append("<b>Matricula:</b> </span>");
            strb.Append("</span>&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###ma###</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>Equipo:</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###eq###</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");//nombre embajador
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>Autoriza:</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###ed###</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("</tr>");//nombre embajador
            strb.Append("<tr style='mso-yfti-irow:1;mso-yfti-lastrow:yes'>");//nombre nivel
            strb.Append("<td style='width:150pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style2'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>Nivel autorización:</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("<td style='width:250pt; padding:0cm 0cm 0cm 0cm' valign='top' class='style3'>");
            strb.Append("<span class='txtcontenidomorado1'>");
            strb.Append("<span style='font-size:9pt'><b>###ni###</b> </span>");
            strb.Append("</span>");
            strb.Append("&nbsp;</td>");
            strb.Append("</tr>");//nombre nivel
            strb.Append("</table>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:100pt; padding:0cm 0cm 0cm 0cm; height: 30px;' valign='top' class='style2'>");
            strb.Append("</td>");
            strb.Append("<td style='width:343pt; padding:0cm 0cm 0cm 0cm; height: 30px;' valign='top' class='style2'>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='width:100pt; padding:0cm 0cm 0cm 0cm; height: 18px;' valign='top' class='style2'>");
            strb.Append("</td> ");
            strb.Append("<td style='width:343pt; padding:0cm 0cm 0cm 0cm; height: 18px;' valign='top' class='style2'>");
            strb.Append("<p>");
            //strb.Append("<a href='" + sitio + "?idSendMan=###id###&amp;idMan=###Mn###&amp;idUsr=###us###&amp;nivel=###ni###' target='_blank'>");
            strb.Append("<a href='" + sitio + "' target='_blank'>");
            strb.Append("<img id='_x0000_i1025' alt=' Volaris ' border='0' height='27' src='http://imagen.volaris.com.mx/correos/Servicios%20Aeroportuarios/btnAutorizar.png' width='149' />");
            strb.Append("</a>");
            strb.Append("</p>");
            strb.Append("</td>");
            strb.Append("'</tr>");
            strb.Append("</table>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("</table>");
            strb.Append("</td>");
            strb.Append("<td style='width:22.5pt;padding:0cm 0cm 0cm 0cm' width='30'>");
            strb.Append("<p class='MsoNormal'>");
            strb.Append("<span style='mso-fareast-font-family:&quot;Times New Roman&quot;'>");
            strb.Append("&nbsp;<o:p></o:p></span></p>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("</table>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:3;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='padding:0cm 0cm 0cm 0cm'></td>");
            strb.Append("</tr>");
            strb.Append("</table>");
            strb.Append("</div>");
            strb.Append("<p class='MsoNormal'>");
            strb.Append("<span style='mso-fareast-font-family:&quot;Times New Roman&quot;;");
            strb.Append("display:none;mso-hide:all'><o:p>&nbsp;</o:p></span></p>");
            strb.Append("<div align='center'>");
            strb.Append("<table border='0' cellpadding='0' cellspacing='0' class='MsoNormalTable' style='width:498.0pt;mso-cellspacing:0cm;mso-yfti-tbllook:1184;mso-padding-alt:0cm 0cm 0cm 0cm' width='664'>");
            strb.Append("<tr style='mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes'>");
            strb.Append("<td style='padding:0cm 0cm 0cm 0cm'></td>");
            strb.Append("</tr>");
            strb.Append("</table>");
            strb.Append("</div>");
            strb.Append("<p class='MsoNormal'>");
            strb.Append("<span style='mso-fareast-font-family:&quot;Times New Roman&quot;'><o:p>&nbsp;</o:p></span></p>");
            strb.Append("</body>");
            strb.Append("</html>");
            return strb.ToString();
        }
    }
}