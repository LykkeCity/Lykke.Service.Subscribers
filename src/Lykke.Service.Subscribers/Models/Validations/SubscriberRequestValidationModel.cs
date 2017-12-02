using FluentValidation;
using Lykke.Service.Subscribers.Models.Subsribers;
using Lykke.Service.Subscribers.Strings;

namespace Lykke.Service.Subscribers.Models.Validations
{
    public class SubscriberRequestValidationModel : AbstractValidator<SubscriberRequestModel>
    {
        public SubscriberRequestValidationModel()
        {
            RuleFor(reg => reg.Email).NotNull().WithMessage(Phrases.FieldShouldNotBeEmpty);
            RuleFor(reg => reg.Email).EmailAddress().WithMessage(Phrases.InvalidEmailFormat);

            RuleFor(reg => reg.Source).NotNull().WithMessage(Phrases.FieldShouldNotBeEmpty);
            RuleFor(reg => reg.Source).NotEmpty().WithMessage(Phrases.FieldShouldNotBeEmpty);
        }
    }
}
