using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOI.SISAC.Dal.ExceptionDal
{
    public class DalException : Exception
    {
        private string code;
        private string messageOwn;
        public DalException(string code)
            : base()
        {
            this.messageOwn = code.ToString();
            this.code = code;
        }

        public DalException(string message, string code)
            : base()
        {
            this.messageOwn = message;
            this.code = code;
        }
        public DalException(string message, string code, Exception inner)
            : base(inner.Message, inner)
        {
            this.messageOwn = message;
            this.code = code;
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string MessageOwn
        {
            get { return messageOwn; }
            set { messageOwn = value; }
        }
    }
}
