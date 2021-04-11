using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest2.Controllers
{
    public class MockupsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult AllPhotos()
        {
            return this.View();
        }

        public IActionResult TaggedPhotos()
        {
            return this.View();
        }

        public IActionResult UserPhotos()
        {
            return this.View();
        }

        public IActionResult PhotosAndCreate()
        {
            return this.View();
        }

        public IActionResult Details()
        {
            return this.View();
        }

        public IActionResult DetailsAuthenticated()
        {
            return this.View();
        }

        public IActionResult DetailsCanManage()
        {
            return this.View();
        }

        public IActionResult Delete()
        {
            return this.View();
        }
    }
}
