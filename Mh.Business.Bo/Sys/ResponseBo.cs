using Mh.Business.Bo.Person;
using System;
using System.Collections.Generic;

namespace Mh.Business.Bo.Sys
{
    public class ResponseBo<TBo>
    {
        public ResponseBo()
        {
            IsSuccess = false;
            HasException = false;
            Message = null;
            ReturnedId = null;
            Bo = default(TBo);
        }

        public bool IsSuccess { get; set; }
        public bool HasException { get; set; }
        public string Message { get; set; }
        public long? ReturnedId { get; set; }
        public TBo Bo { get; set; }

        //public ResponseBo<T> CopyWithoutBo<T>()
        //{
        //    ResponseBo<T> response = new ResponseBo<T>();
        //    response.Bo = default(T);
        //    response.IsSuccess = IsSuccess;
        //    response.HasException = HasException;
        //    response.Message = Message;

        //    return response;
        //}

        public ResponseBo<TBo> ToException(Exception ex, long? logExceptionId = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = ex.Message;
            return this;
        }
    }

    public class ResponseBo
    {
        public ResponseBo()
        {
            IsSuccess = false;
            HasException = false;
            Message = null;
            ReturnedId = null;
            PersonNotifyList = null;
        }

        public bool IsSuccess { get; set; }
        public bool HasException { get; set; }
        public string Message { get; set; }

        public long? ReturnedId { get; set; }

        public List<PersonNotifyListBo> PersonNotifyList { get; set; }
        public ResponseBo<T> ToResponse<T>()
        {
            return new ResponseBo<T>() { IsSuccess = this.IsSuccess, Message = this.Message, ReturnedId = this.ReturnedId, Bo = default(T) };
        }
        public ResponseBo<T> ToResponse<T>(T dto)
        {
            return new ResponseBo<T>() { IsSuccess = this.IsSuccess, Message = this.Message, ReturnedId = this.ReturnedId, Bo = dto };
        }

        public ResponseBo ToException(Exception ex, long? logExceptionId = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = ex.Message;
            return this;
        }
    }
}