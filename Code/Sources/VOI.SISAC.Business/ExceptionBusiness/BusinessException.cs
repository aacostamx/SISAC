//------------------------------------------------------------------------
// <copyright file="BusinessException.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Business.ExceptionBusiness
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Class business Exception
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// The code
        /// </summary>
        private string code;

        /// <summary>
        /// The message own
        /// </summary>
        private string messageOwn;

        /// <summary>
        /// The number
        /// </summary>
        private int number;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        public BusinessException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public BusinessException(string code)
            : base()
        {
            this.code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        public BusinessException(string message, string code)
            : base(message)
        {
            this.messageOwn = message;
            this.code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="inner">The inner.</param>
        public BusinessException(string message, string code, Exception inner)
            : base(message, inner)
        {
            this.messageOwn = message;
            this.code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public BusinessException(string message, Exception inner)
            : base(message, inner)
        {
            this.messageOwn = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="number">The number.</param>
        /// <param name="inner">The inner.</param>
        public BusinessException(string message, int number, Exception inner)
            : base(message, inner)
        {
            this.messageOwn = message;
            this.number = number;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="number">The number.</param>
        public BusinessException(string message, int number)
            : base(message)
        {
            this.messageOwn = message;
            this.number = number;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="number">The number.</param>
        public BusinessException(string message, string code, int number)
            : base(message)
        {
            this.messageOwn = message;
            this.number = number;
            this.code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <param name="number">The number.</param>
        /// <param name="inner">The inner.</param>
        public BusinessException(string message, string code, int number, Exception inner)
            : base(message, inner)
        {
            this.messageOwn = message;
            this.number = number;
            this.code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }

        /// <summary>
        /// Gets or sets the message own.
        /// </summary>
        /// <value>
        /// The message own.
        /// </value>
        public string MessageOwn
        {
            get { return this.messageOwn; }
            set { this.messageOwn = value; }
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }
    }
}
