using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Resources.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {

        private readonly P3AddNewFunctionalityDotNetCore.Models.Services.ProductService _productService = new();

        // Arrange
        private readonly Mock<ICart> _mockCart = new ();
        private static DbContextOptions<P3Referential> option = new DbContextOptionsBuilder<P3Referential>().UseSqlServer("Server=PCYVAN\\SQLEXPRESS;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
        private static IConfiguration _configuration;
        private static P3Referential _context = new P3Referential(option, _configuration);

        private readonly IProductRepository  _ProductRepository = new ProductRepository(_context);
        private readonly Mock<IOrderRepository>  _mockOrderRepository = new ();
        private readonly Mock<IStringLocalizer<P3AddNewFunctionalityDotNetCore.Models.Services.ProductService>> _mockLocalizer = new ();

        public ProductServiceTests()
        {
            _productService = new(_mockCart.Object, _ProductRepository, _mockOrderRepository.Object, _mockLocalizer.Object);
        }

        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>

        #region Name Test
        //Test unitaire - Nom manquant
        [Fact]
        public void ProductService_ShouldShowAnError_WhenNameIsMissing() //ProductService_DoitAfficherUneErreur_LorsqueLeNomEstManquant
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "8",
                Stock = "5"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Equal(1, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Nom manquant", errors); // Vérifie que le bon message d'erreur est retourné
        }
        #endregion

        #region Price Test
        //Test unitaire - Prix manquant
        [Fact]
        public void ProductService_ShouldShowAnError_WhenPriceIsMissing() //ProductService_DoitAfficherUneErreur_LorsqueLePrixEstManquant
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Stock = "5"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            //Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Equal(1, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Prix manquant", errors); // Vérifie que le bon message d'erreur est retourné
        }

        //Test unitaire - Prix pas nombre - Prix pas supérieur à zéro
        [Fact]
        public void ProductService_ShouldShowAnError_WhenPriceIsNotIntegerNumber() //ProductService_DoitAfficherUneErreur_LorsqueLePrixEstPasNombreEntier
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "Texte",
                Stock = "5"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Equal(2, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Prix pas nombre", errors); // Vérifie que le bon message d'erreur est retourné
            Assert.Contains("Prix pas supérieur à zéro", errors); // Vérifie que le bon message d'erreur est retourné
        }

        //Test unitaire - Prix pas supérieur à zéro
        [Fact]
        public void ProductService_ShouldShowAnError_WhenThePriceIsNotHigherZero() //ProductService_DoitAfficherUneErreur_LorsqueLePrixEstPasSuperieurAZero
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "-4",
                Stock = "5"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Contains("Prix pas supérieur à zéro", errors); // Vérifie que le bon message d'erreur est retourné
        }
        #endregion

        #region Stock Test
        //Test unitaire - Prix manquant
        [Fact]
        public void ProductService_ShouldShowAnError_WhenStockIsMissing() //ProductService_DoitAfficherUneErreur_LorsqueLeStockEstManquant
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price="20"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Equal(1, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Quantité manquante", errors); // Vérifie que le bon message d'erreur est retourné
        }

        //Test unitaire - Stock pas nombre - Stock pas supérieur à zéro
        [Fact]
        public void ProductService_ShouldShowAnError_WhenStockIsNotIntegerNumber() //ProductService_DoitAfficherUneErreur_LorsqueLeStockEstPasNombreEntier
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "70",
                Stock = "Texte"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Equal(2, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Quantité pas nombre entier", errors); // Vérifie que le bon message d'erreur est retourné
            Assert.Contains("Quantité pas supérieure à zéro", errors); // Vérifie que le bon message d'erreur est retourné
        }

        //Test unitaire - Stock pas supérieur à zéro
        [Fact]
        public void ProductService_ShouldShowAnError_WhenStockIsNotHigherZero() //ProductService_DoitAfficherUneErreur_LorsqueLeStockEstPasSuperieurAZero
        {
            //Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "80",
                Stock = "-2"
            };

            //Action
            List<string> errors = _productService.CheckProductModelErrors(product1);

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Contains("Quantité pas supérieure à zéro", errors); // Vérifie que le bon message d'erreur est retourné
        }
        #endregion

        #region Test Intégration 
        [Fact]
        public void ProductService_ShouldAddAndRemoveProduct_WhenProductCorrectly() //L'ajout et la suppression de produits devraient fonctionner correctement
        {
            ProductViewModel product = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "8",
                Stock = "5"
            };

            //AJOUTER UN NOUVEAU PRODUIT
            //Arr -- Verifier le nombre de produit en base de donnéé
            int countProduct = _productService.GetAllProducts().Count(); 

            //Act -- Ajout d'un nouveau produit
            _productService.SaveProduct(product);

            int countNewProduct = _productService.GetAllProducts().Count(); 

            //Assert -- Verifier qu'un nouveau produit a été a
            Assert.Equal(countNewProduct, countProduct+1);


            //SUPPRIMMER UN NOUVEAU PRODUIT
            //Arr -- On recupere l'id qu'on vient d'ajouter (maxId)
            int maxId = _productService.GetAllProducts().Max(product => product.Id); // Simulated database-generated ID

            // Act -- Suppression de maxId
            _productService.DeleteProduct(maxId);

            //Assert -- Verifier que maxId n'existe plus car il a été supprimer
            bool result = _productService.GetAllProducts().Any(product => product.Id == maxId);
            countNewProduct = _productService.GetAllProducts().Count();
            Assert.Equal(countNewProduct, countProduct); // On se rassure que le nombre de produit = au nombre de produit de départ
            Assert.False(result);

        }
#endregion
    }
}