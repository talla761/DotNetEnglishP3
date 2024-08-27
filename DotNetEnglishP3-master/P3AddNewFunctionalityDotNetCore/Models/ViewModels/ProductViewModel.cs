using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

using System;
using System.Resources;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using P3AddNewFunctionalityDotNetCore.Models.Services;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "MissingQuantity")]
        [RegularExpression("([0-9]*)", ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "QuantityNotAnInteger")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "MissingPrice")]
        [RegularExpression("([0-9]*)", ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "PriceNotANumber")]
        [Range(1, double.MaxValue, ErrorMessageResourceType = typeof(P3AddNewFunctionalityDotNetCore.Resources.Models.Services.ProductService), ErrorMessageResourceName = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }
}
