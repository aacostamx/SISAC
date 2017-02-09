//------------------------------------------------------------------------
// <copyright file="GendecDepartureBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using AutoMapper;
    using ExceptionBusiness;
    using Resources;    
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Itineraries;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Class Gendec Departure Business
    /// </summary>
    public class GendecDepartureBusiness : IGendecDepartureBusiness
    {
        /// <summary>
        /// The Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Itinerary repository
        /// </summary>
        private readonly IItineraryRepository itineraryRepository;

        /// <summary>
        /// Airplane Repository
        /// </summary>
        private readonly IAirplaneRepository airplaneRepository;

        /// <summary>
        /// Gendec Departure Repository
        /// </summary>
        private readonly IGendecDepartureRepository gendecDepartureRepository;

        /// <summary>
        /// Crew Repository
        /// </summary>
        private readonly ICrewRepository crewRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="GendecDepartureBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="itineraryRepository"></param>
        /// <param name="airplaneRepository"></param>
        /// <param name="gendecDepartureRepository"></param>
        /// <param name="crewRepository"></param>
        public GendecDepartureBusiness(IUnitOfWork unitOfWork,
                                       IItineraryRepository itineraryRepository,
                                       IAirplaneRepository airplaneRepository,
                                       IGendecDepartureRepository gendecDepartureRepository,
                                       ICrewRepository crewRepository)
        {
            this.unitOfWork = unitOfWork;
            this.itineraryRepository = itineraryRepository;
            this.airplaneRepository = airplaneRepository;
            this.gendecDepartureRepository = gendecDepartureRepository;
            this.crewRepository = crewRepository;
        }

        /// <summary>
        /// Find Gendec Departue
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlinecode"></param>
        /// <param name="flightnumber"></param>
        /// <param name="itinerarykey"></param>
        /// <returns>GendecDepartureDto Entity</returns>
        public GendecDepartureDto GetGendecDeparture(int sequence, string airlinecode, string flightnumber, string itinerarykey)
        {
            if (string.IsNullOrWhiteSpace(airlinecode) || string.IsNullOrWhiteSpace(flightnumber) || string.IsNullOrWhiteSpace(itinerarykey))
            {
                return null;
            }

            try
            {
                GendecDeparture gendecDeparture = this.gendecDepartureRepository.GetGendecDeparture(sequence, airlinecode, flightnumber, itinerarykey);
                GendecDepartureDto gendecDepartureDto = Mapper.Map<GendecDeparture, GendecDepartureDto>(gendecDeparture);

                return gendecDepartureDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Add Gendec with Crew
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        public bool AddGendec(GendecDepartureDto gendecDepartureDto)
        {
            if (this.AddGendecDeparture(gendecDepartureDto))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update Gendec with Crew
        /// Firt Delete a Entity Gendec with Relationship and then Add
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns><c>true</c> if was edited <c>false</c> otherwise.</returns>
        public bool EditGendec(GendecDepartureDto gendecDepartureDto)
        {
            GendecDeparture gendecDeparture = new GendecDeparture();
           
            if (gendecDepartureDto == null)
            {
                return false;
            }
            try
            {
                gendecDeparture = this.gendecDepartureRepository.GetGendecDeparture(gendecDepartureDto.Sequence, gendecDepartureDto.AirlineCode, gendecDepartureDto.FlightNumber, gendecDepartureDto.Itinerarykey);
                this.gendecDepartureRepository.Delete(gendecDeparture);
                this.unitOfWork.Commit();
                if (this.AddGendecDeparture(gendecDepartureDto))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Close Gendec Departure
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns></returns>
        public bool CloseGendecDeparture(GendecDepartureDto gendecDepartureDto)
        {
            try
            {
                gendecDepartureDto.Itinerary = null;
                GendecDeparture gendecDeparture = new GendecDeparture();
                gendecDeparture = Mapper.Map<GendecDepartureDto, GendecDeparture>(gendecDepartureDto);
                gendecDeparture.Closed = true;
                this.gendecDepartureRepository.Update(gendecDeparture);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Open Gendec Departure
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns></returns>
        public bool OpenGendecDepartureButton(GendecDepartureDto gendecDepartureDto)
        {
            try
            {
                gendecDepartureDto.Itinerary = null;
                GendecDeparture gendecDeparture = new GendecDeparture();
                gendecDeparture = Mapper.Map<GendecDepartureDto, GendecDeparture>(gendecDepartureDto);
                gendecDeparture.Closed = false;
                this.gendecDepartureRepository.Update(gendecDeparture);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Sendings the email.
        /// </summary>
        /// <param name="gendecDepartureDto">The gendec departure dto.</param>
        /// <returns></returns>
        public IList<string> SendingEmail(GendecDepartureDto gendecDepartureDto)
        {
            IList<string> errors = new List<string>();
            //MailModelDto milModel = new MailModelDto();

            if (gendecDepartureDto != null)
            {
                //Obtener información de Usuario (Jefe de Aeropuerto) para obtener su Email
                MailMessage mail = new MailMessage();
              //  mail.To.Add("jalvarez@linko.mx");
              //  mail.From = new MailAddress("jalvarez@linko.mx");
              //  mail.Subject = "Sistema de Servicios Aeroportuarios";
                string Body = this.MailConfiguration(gendecDepartureDto.Sequence, gendecDepartureDto.AirlineCode, gendecDepartureDto.FlightNumber, gendecDepartureDto.Itinerarykey);
               // mail.Body = Body;
                mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient("10.10.32.5");
                SmtpClient smtp = new SmtpClient("127.127.0.1");
                VOINotifications.ServiceNotificationsClient wcf = new VOINotifications.ServiceNotificationsClient();

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
        /// Delete Crew of a Gendec
        /// </summary>
        /// <param name="CrewID"></param>
        /// <param name="gendecDepartureDto"></param>
        /// <returns>GendecDepartureDto Entity</returns>
        public GendecDepartureDto DeleteGendecCrew(long CrewID, GendecDepartureDto gendecDepartureDto)
        {
            try
            {
                // Initialize Controls
                GendecDeparture gendecDeparture = new GendecDeparture();
                Crew crewEntity = new Crew();
                GendecDepartureDto gendecDepartureDtoRes = new GendecDepartureDto();

                // Get a Gendec to Remove
                gendecDeparture = this.gendecDepartureRepository.GetGendecDeparture((int)gendecDepartureDto.Sequence, (string)gendecDepartureDto.AirlineCode, (string)gendecDepartureDto.FlightNumber, (string)gendecDepartureDto.Itinerarykey);
                // Get a Crew to Remove
                crewEntity = this.crewRepository.FindById(CrewID);
                // Remove a Crew Entity
                gendecDeparture.Crews.Remove(crewEntity);
                
                gendecDepartureDtoRes = Mapper.Map<GendecDeparture, GendecDepartureDto>(gendecDeparture);
                this.unitOfWork.Commit();

                //Se actualiza la entidad de GendecDeparture con el nuevo número de Crew
                gendecDeparture.TotalCrew = gendecDeparture.TotalCrew -1;
                this.gendecDepartureRepository.Update(gendecDeparture);
                this.unitOfWork.Commit();

                return gendecDepartureDtoRes;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

        }

        /// <summary>
        /// Add a Gendec with Crew
        /// </summary>
        /// <param name="gendecDepartureDto"></param>
        /// <returns></returns>
        private bool AddGendecDeparture(GendecDepartureDto gendecDepartureDto)
        {
            GendecDeparture gendecDeparture = new GendecDeparture();
            if (gendecDepartureDto == null)
            {
                return false;
            }
            try
            {
                gendecDeparture = Mapper.Map<GendecDepartureDto, GendecDeparture>(gendecDepartureDto);
                IList<Crew> crews = gendecDeparture.Crews.ToList();
                gendecDeparture.Crews = new List<Crew>();
                this.gendecDepartureRepository.AddGendec(gendecDeparture, crews);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }


        /// <summary>
        /// Opens the gendec departure.
        /// </summary>
        /// <param name="gendecDepartureDto">The gendec departure dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool OpenGendecDeparture(GendecDepartureDto gendecDepartureDto)
        {
            try
            {
                GendecDeparture gendecDeparture = new GendecDeparture();
                gendecDeparture = this.gendecDepartureRepository.GetGendecDeparture(gendecDepartureDto.Sequence, gendecDepartureDto.AirlineCode, gendecDepartureDto.FlightNumber, gendecDepartureDto.Itinerarykey);
                gendecDeparture.Closed = false;
                this.gendecDepartureRepository.Update(gendecDeparture);
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
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightnumber">The flightnumber.</param>
        /// <param name="itinerarykey">The itinerarykey.</param>
        /// <returns></returns>
        private string MailConfiguration(int sequence, string airlineCode, string flightnumber, string itinerarykey)
        {
            string Body = "";
            ItineraryDto itineraryDto = new ItineraryDto();
            itineraryDto = Mapper.Map<Itinerary, ItineraryDto>(this.itineraryRepository.FindById(sequence, airlineCode, flightnumber, itinerarykey));
            //string url = "http://localhost:3991/ServiceOpenManifestServices.svc/OpenD";
            string url = "http://10.10.32.62:966/Gendec.svc/OpenD";
            Body = ConfigMailManifiesto(url).Replace("###fo###", itineraryDto.GendecDepartures.ManifestNumber);
            Body = Body.Replace("###so###", "Luis Alvarez del Castillo Bermudez");
            Body = Body.Replace("###no###", "PS0005");
            Body = Body.Replace("###pe###", "ASC");
            Body = Body.Replace("###vu###", itineraryDto.FlightNumber);
            Body = Body.Replace("###od###", itineraryDto.DepartureStation + "-" + itineraryDto.ArrivalStation);
            Body = Body.Replace("###fe###", itineraryDto.DepartureDate.ToShortDateString());
            Body = Body.Replace("###ho###", itineraryDto.DepartureDate.ToShortTimeString());
            Body = Body.Replace("###ma###", itineraryDto.EquipmentNumber);
            Body = Body.Replace("###eq###", itineraryDto.Airplane.AirplaneModel);
            Body = Body.Replace("###de###", "");
            Body = Body.Replace("###ti###", "Salida");
            Body = Body.Replace("###id###", itineraryDto.Sequence.ToString());
            Body = Body.Replace("###Mn###", itineraryDto.GendecDepartures.ManifestNumber);
            Body = Body.Replace("###us###", "4");
            Body = Body.Replace("###ni###", "1");
            Body = Body.Replace("###ed###", "Usuario Jefe Aeropuerto");
            Body = Body.Replace("###em###", "Y4");
            Body = Body.Replace("###ky###", itineraryDto.ItineraryKey);
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
            strb.Append("<img id='_x0000_i1025' alt=' AACOSTA ' border='0' height='49' src='http://imagen.http://aacosta.com.mx/com.mx/correos/promocion/2009/Noviembre/20091113_mailing_confirmacion_gift_cards/heder_gifcards.jpg' width='664' /></p>");
            strb.Append("</td>");
            strb.Append("</tr>");
            strb.Append("<tr style='mso-yfti-irow:2;height:357.0pt'>");
            strb.Append("<td background='http://imagen.http://aacosta.com.mx/com.mx/correos/promocion/2009/Noviembre/20091113_mailing_confirmacion_gift_cards/back_mailing_gifcards_confirmacion.jpg' style='width:483.0pt;padding:0cm 0cm 0cm 0cm; height:357.0pt' valign='top' width='644'>");
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
            strb.Append("<a href='" + sitio + "/###id###/###em###/###vu###/###ky###' target='_blank'>");
            strb.Append("<img id='_x0000_i1025' alt=' AACOSTA ' border='0' height='27' src='http://imagen.http://aacosta.com.mx/com.mx/correos/Servicios%20Aeroportuarios/btnAutorizar.png' width='149' />");
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
