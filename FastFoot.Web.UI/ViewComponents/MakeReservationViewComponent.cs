﻿using Microsoft.AspNetCore.Mvc;

namespace FastFoot.Web.UI.ViewComponents
{
    public class MakeReservationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}