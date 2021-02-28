using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotvvmValidationSample.Model
{
    public class CreateAccountForm : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string Password2 { get; set; }

        public bool AgreeWithConditions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!AgreeWithConditions)
            {
                yield return new ValidationResult(
                    "You need to agree with terms & conditions.",
                    new[] { nameof(AgreeWithConditions) }
                );
            }
        }
    }
}
