using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationDemo.Core.Models
{
    public class BaseResponse
    {
        protected const string DEFAULT_ERROR = "GENERAL ERROR";
        public bool OK { get; set; } = true;
        public string Message { get; set; }

        /******************************** CTOR ********************************/

        public BaseResponse()
        {
            OK = true;
        }
        public BaseResponse(Exception ex)
        {
            SetError(ex);
        }
        public BaseResponse(bool ok)
        {
            OK = ok;
        }
        public BaseResponse(bool ok, string msg)
        {
            OK = ok;
            Message = msg;
        }
        /******************************** PUBLIC ********************************/
        public void SetError(Exception ex = null, string message = null)
        {
            try
            {
                this.OK = false;
                if (ex != null)
                {
                    if (!string.IsNullOrEmpty(message))
                        ex.Data.Add("Message", message);
                }
                SetErrorMessage(message);
            }
            catch
            {
            }
        }
        public void SetError(string message, params object[] args)
        {
            try
            {
                this.OK = false;
                SetErrorMessage(message, args);
            }
            catch
            {
            }
        }
        protected void SetErrorMessage(string error, params object[] args)
        {
            Message = !string.IsNullOrEmpty(error) ? string.Format(error, args) : DEFAULT_ERROR;
        }

        public static BaseResponse<X> Create<X>(X data)
        {
            return new BaseResponse<X>(data, true);
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public BaseResponse() : base() { }
        public BaseResponse(Exception ex) : base(ex) { }

        public BaseResponse(T data)
        {
            Data = data;
        }

        public BaseResponse(T data, bool ok)
        {
            Data = data;
            OK = ok;
        }


    }

    public class BaseResponse<T, X> : BaseResponse
    {
        public X Extra { get; set; }
        public T Data { get; set; }

        public BaseResponse() : base() { }
        public BaseResponse(Exception ex) : base(ex) { }
        public BaseResponse(bool ok) : base(ok) { }

        public BaseResponse(T data)
        {
            Data = data;
        }

        public BaseResponse(T data, bool ok)
        {
            Data = data;
            OK = ok;
        }
    }
}
