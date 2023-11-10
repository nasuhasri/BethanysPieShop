using BethanysPieShop.Controllers;
using BethanysPieShop.ViewModels;
using BethanysPieShopTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BethanysPieShopTests.Controllers;

public class PieControllerTests
{
    [Fact] // using this attribute, this becomes unit testing method
    public void List_EmptyCategory_ReturnsAllPies()
    {
        // arrange
        var mockPieRepository = RepositoryMocks.GetPieRepository();
        var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

        var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

        // act
        var result = pieController.List("");

        // return View(new PieListViewModel(pies, currentCategory));

        // assert
        // check return type of ViewResult - answer question: did we controller return the ViewResult?
        var viewResult = Assert.IsType<ViewResult>(result);
        // check if this one (viewResult.ViewData.Model) is the model we passed in -> PieListViewModel
        var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
        Assert.Equal(10, pieListViewModel.Pies.Count());
    }
}
