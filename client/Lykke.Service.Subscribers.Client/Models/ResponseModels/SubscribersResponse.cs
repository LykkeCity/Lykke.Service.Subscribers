using Lykke.Service.Subscribers.Client.AutorestClient.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;

namespace Lykke.Service.Subscribers.Client.Models.ResponseModels
{
    public class SubscribersResponse : BaseResponseModel<IEnumerable<SubscriberResponseModel>>
    {
        public IEnumerable<SubscriberResponseModel> SubscribersResponceModel { get; set; }

        public static SubscribersResponse Prepare(HttpOperationResponse<object> apiResponse)
        {
            var error = apiResponse.Body as ErrorResponse;
            var result = apiResponse.Body as IEnumerable<SubscriberResponseModel>;

            if (error != null)
            {
                return new SubscribersResponse
                {
                    Error = new ErrorModel
                    {
                        Message = error.ErrorMessage
                    }
                };
            }

            if (result != null)
            {
                return new SubscribersResponse
                {
                    SubscribersResponceModel = result
                };
            }

            throw new ArgumentException("Unknown response object");
        }

        public override IEnumerable<SubscriberResponseModel> GetPayload()
        {
            return SubscribersResponceModel;
        }
    }
}
