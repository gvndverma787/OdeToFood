using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private readonly int _maxWords;

        public MaxWordsAttribute(int maxWords):base("{0} has too many word.")
        {
            _maxWords = maxWords;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           if(value != default(object))
            {
                var valueAsString = value.ToString();
                if(valueAsString.Split(' ').Length > _maxWords)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
    public class RestaurantReview : IValidatableObject
    {
        public int Id { get; set; }
        [Range(1,10)]
        public int Rating { get; set; }
        [Required(ErrorMessageResourceType = typeof(OdeToFood.Views.Home.Resources),ErrorMessageResourceName = "ReviewBody")]
        [StringLength(1024)]
        public string Body { get; set; }
        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "anonymous")]
        [MaxWords(1)]
        public string ReviewerName { get; set; }
        public int RestaurantId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Rating < 2 && ReviewerName.ToLower().StartsWith("scott"))
            {
                yield return new ValidationResult("Sorry Scott, you can't do this");
            }
        }
    }
}