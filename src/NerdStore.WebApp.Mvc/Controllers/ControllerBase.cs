using Microsoft.AspNetCore.Mvc;
using System;

namespace NerdStore.WebApp.Mvc.Controllers
{
    public abstract class ControllerBase : Controller
    {
        public Guid ClienteId = Guid.Parse("036437B9-1CCA-4F83-974A-8EC27A8C06C5");
    }
}
