using FluentAssertions;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService = new();

        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>

        #region Name Test
        //Test unitaire - Nom manquant
        [Fact]
        public void ProductService_DoitAfficherUneErreur_LorsqueLeNomEstManquant()
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
        public void ProductService_DoitAfficherUneErreur_LorsqueLePrixEstManquant()
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

            // Assert
            Assert.NotEmpty(errors); // Vérifie que la liste d'erreurs n'est pas vide
            Assert.Equal(1, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Prix manquant", errors); // Vérifie que le bon message d'erreur est retourné
        }

        //Test unitaire - Prix pas nombre - Prix pas supérieur à zéro
        [Fact]
        public void ProductService_DoitAfficherUneErreur_LorsqueLePrixEstPasNombreEntier()
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
        public void ProductService_DoitAfficherUneErreur_LorsqueLePrixEstPasSuperieurAZero()
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
            //Assert.Equal(2, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Prix pas supérieur à zéro", errors); // Vérifie que le bon message d'erreur est retourné
        }
        #endregion

        #region Stock Test
        //Test unitaire - Prix manquant
        [Fact]
        public void ProductService_DoitAfficherUneErreur_LorsqueLeStockEstManquant()
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
        public void ProductService_DoitAfficherUneErreur_LorsqueLeStockEstPasNombreEntier()
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
        public void ProductService_DoitAfficherUneErreur_LorsqueLeStockEstPasSuperieurAZero()
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
            //Assert.Equal(2, errors.Count); // Vérifie qu'il n'y a qu'une seule erreur
            Assert.Contains("Quantité pas supérieure à zéro", errors); // Vérifie que le bon message d'erreur est retourné
        }
        #endregion
        // TODO write test methods to ensure a correct coverage of all possibilities

        [Fact]
        public void AddAndRemoveProduct_ShouldWorkCorrectly()
        {
            // Arrange
            ProductViewModel product1 = new ProductViewModel
            {
                Name = "NameTest",
                Description = "DescriptionTest",
                Details = "DetailsTest",
                Price = "8",
                Stock = "5"
            };

            // Act: Ajouter le produit
            _productService.SaveProduct(product1);

            // Assert: Vérifier que le produit est ajouté correctement
            var addedProduct = _productService.MapToProductEntity(product1); // Obtenir le produit ajouté

            Assert.NotNull(addedProduct);
            Assert.Equal(1, addedProduct.Id); // Vérifier que le produit a un ID correct

            Assert.Equal(product1.Name, addedProduct.Name); // Vérifier que le nom de l'utilisateur est correct
            Assert.Equal(product1.Details, addedProduct.Details);   // Vérifier que l'âge de l'utilisateur est correct


            // Act: Supprimer le produit
            _productService.DeleteProduct(product1.Id);

            // Assert: Vérifier que le produit est supprimé correctement
            var retrievedProduct = _productService.GetProduct(product1.Id);
            Assert.Null(retrievedProduct); // Vérifier que l'utilisateur n'existe plus
        }
    }
}