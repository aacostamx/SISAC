//------------------------------------------------------------------------
// <copyright file="MailModelDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Generic
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    /// <summary>
    /// MailModelDto
    /// </summary>
    public class MailModelDto
    {

        /// <summary>
        /// Gets or sets the SMTP client.
        /// </summary>
        /// <value>
        /// The SMTP client.
        /// </value>
        public string SmtpClient { get; set; }

        /// <summary>
        /// Gets or sets the user token.
        /// </summary>
        /// <value>
        /// The user token.
        /// </value>
        public string UserToken { get; set; }

        /// <summary>
        /// Gets or sets the password token.
        /// </summary>
        /// <value>
        /// The password token.
        /// </value>
        public string PasswordToken { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The Attached File Name.
        /// </value>
        public string AttachedFileName { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public IList<string> Errors { get; set; }
    }
}
