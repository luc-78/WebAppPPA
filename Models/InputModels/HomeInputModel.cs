using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using WebAppPPA.Customizations.ModelBinders;

namespace WebAppPPA.Models.InputModels
{
    [ModelBinder(BinderType = typeof(HomeInputModelBinder))]
    public class HomeInputModel
    {
        
        public HomeInputModel(int page, int limit)
        {
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);
            Offset = (Page - 1) * Limit;
        }

        public int Page { get; }
        public int Limit { get; }
        public int Offset { get; }
    }
}