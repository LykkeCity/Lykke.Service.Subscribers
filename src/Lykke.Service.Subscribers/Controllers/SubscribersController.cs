using Common.Log;
using JetBrains.Annotations;
using Lykke.Service.Subscribers.AzureRepositories.Subscribers;
using Lykke.Service.Subscribers.Core.Repositories;
using Lykke.Service.Subscribers.Models.Subsribers;
using Lykke.Service.Subscribers.Strings;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Lykke.Service.Subscribers.Controllers
{
    [Route("api/subscribers")]
    [Produces("application/json")]
    public class SubscribersController : Controller
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ILog _log;

        public SubscribersController([NotNull]ISubscriberRepository subscriberRepository, [NotNull] ILog log)
        {
            _subscriberRepository = subscriberRepository;
            _log = log;
        }

        /// <summary>
        /// Get a subscribers.
        /// </summary>
        ///<param name="source">Source from which is created the subscription</param>
        [HttpGet]
        [SwaggerOperation("GetSubsribers")]
        [ProducesResponseType(typeof(IEnumerable<SubscriberResponseModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSubscribers([FromQuery] string source)
        {
            var result = (await _subscriberRepository.GetAllAsync(source)).Select(s => SubscriberResponseModel.Create(s));
            return Ok(result);
        }

        /// <summary>
        /// Get a subscriber by email.
        /// </summary>
        ///<param name="source">Source from which is created the subscription</param>
        ///<param name="email">Email of subscriber</param>
        [HttpGet("getSubsribersByEmail")]
        [SwaggerOperation("GetSubsribersByEmail")]
        [ProducesResponseType(typeof(SubscriberResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByEmail([FromQuery] string email, [FromQuery] string source)
        {
            var result = await _subscriberRepository.GetAsync(email, source);

            if (result == null)
                return NotFound(Phrases.NotFound);

            return Ok(SubscriberResponseModel.Create(result));
        }

        /// <summary>
        /// Create a subscriber.
        /// </summary>
        /// <param name="subscriber">Subscriber request model</param>
        [HttpPut("create")]
        [SwaggerOperation("CreateAsync")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] SubscriberRequestModel subscriber)
        {
            await _subscriberRepository.CreateAsync(new Subscriber()
            {
                Email = subscriber.Email,
                PartnerId = subscriber.PartnerId,
                Source = subscriber.Source
            });
            return Ok();
        }

        /// <summary>
        /// Delete a subscriber.
        /// </summary>
        /// <param name="source">Source from which is created the subscription</param>
        /// <param name="email">Email of subscriber</param>
        [HttpDelete("delete")]
        [SwaggerOperation("DeleteAsync")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAsync([FromQuery] string email, [FromQuery] string source)
        {
            var result = await _subscriberRepository.GetAsync(email, source);

            if (result == null)
                return NotFound(Phrases.NotFound);

            await _subscriberRepository.DeleteAsync(email, source);
            return Ok();
        }
    }
}
