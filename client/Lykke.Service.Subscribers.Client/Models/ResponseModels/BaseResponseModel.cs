using System;

namespace Lykke.Service.Subscribers.Client.Models.ResponseModels
{
    public abstract class BaseResponseModel<T>
    {
        public ErrorModel Error { get; set; }

        public abstract T GetPayload();

        public BaseResponseModel<T> Validate()
        {
            if (Error != null)
            {
                throw new Exception(Error.Message);
            }

            return this;
        }
    }
}
