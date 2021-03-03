using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest1.Controllers
{
    public class MockupsController : Controller
    {
        public IActionResult Index() => this.View();

        public IActionResult SinglePlayer() => this.View();

        public IActionResult SetUpCivilization() => this.View();

        public IActionResult SetUpOpponents() => this.View();

        public IActionResult SetUpBasicSettings() => this.View();

        public IActionResult SetUpAdvancedSettings() => this.View();

        public IActionResult LoadGame() => this.View();

        public IActionResult LoadGameSpecific() => this.View();
    }
}
