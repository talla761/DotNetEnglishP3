using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

using System;
using System.Resources;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        //[Required(ErrorMessage = "MissingQuantity", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [Required(ErrorMessage = "MissingQuantity")]
        [RegularExpression("([0-9]*)", ErrorMessage = "QuantityNotAnInteger")]
        [Range(1, int.MaxValue, ErrorMessage = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        [RegularExpression("([0-9]*)", ErrorMessage = "PriceNotANumber")]
        [Range(1, double.MaxValue, ErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }
}
