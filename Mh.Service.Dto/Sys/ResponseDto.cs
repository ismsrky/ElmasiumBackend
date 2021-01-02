using Mh.Service.Dto.Auth;
using System;
using System.Collections.Generic;

namespace Mh.Service.Dto.Sys
{
    public class ResponseDto<TDto>
    {
        public ResponseDto()
        {
            IsSuccess = false;
            HasException = false;
            Message = null;
            Dto = default(TDto);
        }

        public bool IsSuccess { get; set; }
        public bool HasException { get; set; }
        public string Message { get; set; }
        public long? ReturnedId { get; set; }
        public TDto Dto { get; set; }

        public void Set(object data)
        {

        }

        public ResponseDto<T> CopyWithoutDto<T>()
        {
            ResponseDto<T> response = new ResponseDto<T>();
            response.Dto = default(T);
            response.IsSuccess = IsSuccess;
            response.HasException = HasException;
            response.Message = Message;
            response.ReturnedId = ReturnedId;

            return response;
        }

        public ResponseDto<TDto> ToException(Exception ex)
        {
            IsSuccess = false;
            HasException = true;
            Message = ex.Message;
            return this;
        }
    }

    public class ResponseDto
    {
        public ResponseDto()
        {
            IsSuccess = false;
            HasException = false;
            Message = null;
            ReturnedId = null;
        }

        public bool IsSuccess { get; set; }
        public bool HasException { get; set; }
        public string Message { get; set; }

        public long? ReturnedId { get; set; }

        public LoginReturnDto LoginReturnDto { get; set; }
        public ResponseDto<T> ToResponse<T>()
        {
            return new ResponseDto<T>() { IsSuccess = this.IsSuccess, Message = this.Message, Dto = default(T) };
        }
        public ResponseDto<T> ToResponse<T>(T dto)
        {
            return new ResponseDto<T>() { IsSuccess = this.IsSuccess, Message = this.Message, Dto = dto };
        }

        public ResponseDto ToException(Exception ex)
        {
            IsSuccess = false;
            HasException = true;
            Message = ex.Message;
            return this;
        }
    }
}