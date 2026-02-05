using IRIS.Domain.Entities;          
using IRIS.Presentation.Forms;        
using IRIS.Presentation.UserControls.PagesUC;
using IRIS.Services.Interfaces;       
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Presentation.Presenters
{
    public class RestockPresenter
    {

        private readonly RestockPage _view;
        private readonly IRestockService _service;

        private string _currentCategory = "All";
        private string _currentStatus = "All";
        public RestockPresenter(RestockPage view, IRestockService service)
        {
            _view = view;
            _service = service;
        }

        public void OnViewLoad()
        {
            var categories = _service.GetCategories();
            _view.BuildCategoryMenu(categories);
            LoadData();
        }

        // Logic for Category Filter
        public void FilterByCategory(string category)
        {
            _currentCategory = category;
            string labelText = category == "All" ? "Categories ▼" : category;

            _view.UpdateCategoryButtonText(labelText);
            LoadData();
        }

        // Logic for Status Filter
        public void FilterByStatus(string status)
        {
            _currentStatus = status;
            LoadData();
        }

        // Logic for Refresh Button
        public void RefreshData()
        {
  
            _service.RefreshRestockData();
            _view.RefreshStatusCards();
            LoadData();
        }

        private void LoadData()
        {

            var data = _service.GetFilteredRestockList(_currentCategory, _currentStatus);
            _view.DisplayRestockList(data.ToList());
        }
    }
}