using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTest1.Models;
using BackendTest1.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest1.Controllers
{
    public class CivilizationController : Controller
    {
        private readonly IMapTypeDataProviderService _mapTypeDataProviderService;
        private readonly IMapSizeDataProviderService _mapSizeDataProviderService;
        private readonly IDifficultyDataProviderService _difficultyDataProviderService;
        private readonly IVictoryTypeDataProviderService _victoryTypeDataProviderService;
        private readonly ICivilizationDataProviderService _civilizationDataProviderService;
        private readonly IDatabase _database;

        public CivilizationController(
            IMapTypeDataProviderService mapTypeDataProviderService,
            IMapSizeDataProviderService mapSizeDataProviderService,
            IDifficultyDataProviderService difficultyDataProviderService,
            IVictoryTypeDataProviderService victoryTypeDataProviderService,
            ICivilizationDataProviderService civilizationDataProviderService,
            IDatabase database)
        {
            _mapTypeDataProviderService = mapTypeDataProviderService;
            _mapSizeDataProviderService = mapSizeDataProviderService;
            _difficultyDataProviderService = difficultyDataProviderService;
            _victoryTypeDataProviderService = victoryTypeDataProviderService;
            _civilizationDataProviderService = civilizationDataProviderService;
            _database = database;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public IActionResult IndexConfirm()
        {
            return View("SinglePlayer");
        }

        public IActionResult SinglePlayer()
        {
            return View();
        }

        [HttpPost, ActionName("SinglePlayer")]
        public IActionResult SinglePlayerConfirm(SinglePlayerAction action)
        {
            switch (action)
            {
                case SinglePlayerAction.PlayNow:
                    _database.PushData(new Data
                    {
                        ChosenName = "Player",
                        ChosenOpponents = new List<Int32?>
                        {
                            null, null, null
                        },
                        ChosenMapType = _mapTypeDataProviderService.GetEntities()[0].Id,
                        ChosenMapSize = _mapSizeDataProviderService.GetEntities()[0].Id,
                        ChosenDifficulty = _difficultyDataProviderService.GetEntities()[0].Id,
                        ChosenVictoryType = new List<Boolean>
                        {
                            true, true, true, true, true
                        },
                        ChosenAdvancedGameOptions = new List<Boolean>
                        {
                            false, false, false
                        },
                        AdvancedGameOptions = new List<String>
                        {
                            "Max Turns", "Complete Kills", "Once-City Challenge"
                        }
                    });

                    var data = _database.GetData()[_database.GetData().Count - 1];

                    var newModel = new LoadGameViewModel
                    {
                        Name = data.ChosenName,
                        Civilization = data.ChosenCivilization == null ? null : _civilizationDataProviderService.FindEntity((Int32)data.ChosenCivilization),
                        Opponents = new List<Civilization>(),
                        MapType = _mapTypeDataProviderService.FindEntity(data.ChosenMapType),
                        MapSize = _mapSizeDataProviderService.FindEntity(data.ChosenMapSize),
                        Difficulty = _difficultyDataProviderService.FindEntity(data.ChosenDifficulty),
                        VictoryTypes = new List<VictoryType>(),
                        AdvancedGameOptions = new List<String>(),
                        NumberOfGames = _database.GetData().Count
                    };

                    if (data.ChosenOpponents != null)
                    {
                        foreach (var opponent in data.ChosenOpponents)
                        {
                            if (opponent == null)
                            {
                                newModel.Opponents.Add(null);
                            }
                            else
                            {
                                newModel.Opponents.Add(_civilizationDataProviderService.FindEntity((Int32)opponent));
                            }
                        }
                    }

                    for (var i = 0; i < data.ChosenVictoryType.Count; i++)
                    {
                        if (data.ChosenVictoryType[i])
                        {
                            newModel.VictoryTypes.Add(_victoryTypeDataProviderService.FindEntity(i));
                        }
                    }

                    for (var i = 0; i < data.ChosenAdvancedGameOptions.Count; i++)
                    {
                        if (data.ChosenAdvancedGameOptions[i])
                        {
                            newModel.AdvancedGameOptions.Add(data.AdvancedGameOptions[i]);
                        }
                    }

                    newModel.ChosenGame = _database.GetData().Count - 1;

                    return View("LoadGame", newModel);
                case SinglePlayerAction.SetUpGame:
                    return View("SetUpGame", new SetUpGameViewModel
                    {
                        MapType = _mapTypeDataProviderService.GetEntities(),
                        MapSize = _mapSizeDataProviderService.GetEntities(),
                        Difficulty = _difficultyDataProviderService.GetEntities(),
                        VictoryType = _victoryTypeDataProviderService.GetEntities(),
                        Civilization = _civilizationDataProviderService.GetEntities(),
                        ChosenTab = "Civilization",
                        ChosenName = "Player",
                        ChosenOpponents = new List<Int32?>
                        {
                            null, null, null
                        },
                        ChosenMapType = _mapTypeDataProviderService.GetEntities()[0].Id,
                        ChosenMapSize = _mapSizeDataProviderService.GetEntities()[0].Id,
                        ChosenDifficulty = _difficultyDataProviderService.GetEntities()[0].Id,
                        ChosenVictoryType = new List<Boolean>
                        {
                            true, true, true, true, true
                        },
                        ChosenAdvancedGameOptions = new List<Boolean>
                        {
                            false, false, false
                        },
                        AdvancedGameOptions = new List<String>
                        {
                            "Max Turns", "Complete Kills", "Once-City Challenge"
                        }
                    });
                case SinglePlayerAction.LoadGame:
                    return View("LoadGame", new LoadGameViewModel
                    {
                        NumberOfGames = _database.GetData().Count,
                        ChosenGame = null
                    });
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        public IActionResult SetUpGame(SetUpGameViewModel model)
        {
            return View(model);
        }

        [HttpPost, ActionName("SetUpGame")]
        public IActionResult SetUpGameConfirm(SetUpGameViewModel model, SetUpGameAction action, Int32 removeIndex)
        {
            switch (action)
            {
                case SetUpGameAction.Civilization:
                    model.ChosenTab = "Civilization";
                    break;
                case SetUpGameAction.Opponents:
                    model.ChosenTab = "Opponents";
                    break;
                case SetUpGameAction.BasicSettings:
                    model.ChosenTab = "BasicSettings";
                    break;
                case SetUpGameAction.AdvancedSettings:
                    model.ChosenTab = "AdvancedSettings";
                    break;
                case SetUpGameAction.Start:
                    _database.PushData(new Data
                    {
                        ChosenName = model.ChosenName,
                        ChosenCivilization = model.ChosenCivilization,
                        ChosenOpponents = model.ChosenOpponents,
                        ChosenMapType = model.ChosenMapType,
                        ChosenMapSize = model.ChosenMapSize,
                        ChosenDifficulty = model.ChosenDifficulty,
                        ChosenVictoryType = model.ChosenVictoryType,
                        ChosenAdvancedGameOptions = model.ChosenAdvancedGameOptions,
                        AdvancedGameOptions = model.AdvancedGameOptions
                    });

                    var data = _database.GetData()[_database.GetData().Count - 1];

                    var newModel = new LoadGameViewModel
                    {
                        Name = data.ChosenName,
                        Civilization = data.ChosenCivilization == null ? null : _civilizationDataProviderService.FindEntity((Int32)data.ChosenCivilization),
                        Opponents = new List<Civilization>(),
                        MapType = _mapTypeDataProviderService.FindEntity(data.ChosenMapType),
                        MapSize = _mapSizeDataProviderService.FindEntity(data.ChosenMapSize),
                        Difficulty = _difficultyDataProviderService.FindEntity(data.ChosenDifficulty),
                        VictoryTypes = new List<VictoryType>(),
                        AdvancedGameOptions = new List<String>(),
                        NumberOfGames = _database.GetData().Count
                    };

                    if (data.ChosenOpponents != null)
                    {
                        foreach (var opponent in data.ChosenOpponents)
                        {
                            if (opponent == null)
                            {
                                newModel.Opponents.Add(null);
                            }
                            else
                            {
                                newModel.Opponents.Add(_civilizationDataProviderService.FindEntity((Int32)opponent));
                            }
                        }
                    }

                    for (var i = 0; i < data.ChosenVictoryType.Count; i++)
                    {
                        if (data.ChosenVictoryType[i])
                        {
                            newModel.VictoryTypes.Add(_victoryTypeDataProviderService.FindEntity(i));
                        }
                    }

                    for (var i = 0; i < data.ChosenAdvancedGameOptions.Count; i++)
                    {
                        if (data.ChosenAdvancedGameOptions[i])
                        {
                            newModel.AdvancedGameOptions.Add(data.AdvancedGameOptions[i]);
                        }
                    }

                    newModel.ChosenGame = _database.GetData().Count - 1;

                    return View("LoadGame", newModel);
                case SetUpGameAction.Add:
                    if (model.ChosenOpponents == null)
                    {
                        model.ChosenOpponents = new List<Int32?>();
                    }
                    model.ChosenOpponents.Add(null);
                    break;
                case SetUpGameAction.Remove:
                    model.ChosenOpponents.RemoveAt(removeIndex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

            return View(model);
        }

        public IActionResult LoadGame(LoadGameViewModel model)
        {
            return View(model);
        }

        [HttpPost, ActionName("LoadGame")]
        public IActionResult LoadGameConfirm(LoadGameViewModel model, Int32 showIndex)
        {
            var data = _database.GetData()[showIndex];

            var newModel = new LoadGameViewModel
            {
                Name = data.ChosenName,
                Civilization = data.ChosenCivilization == null ? null : _civilizationDataProviderService.FindEntity((Int32)data.ChosenCivilization),
                Opponents = new List<Civilization>(),
                MapType = _mapTypeDataProviderService.FindEntity(data.ChosenMapType),
                MapSize = _mapSizeDataProviderService.FindEntity(data.ChosenMapSize),
                Difficulty = _difficultyDataProviderService.FindEntity(data.ChosenDifficulty),
                VictoryTypes = new List<VictoryType>(),
                AdvancedGameOptions = new List<String>(),
                NumberOfGames = model.NumberOfGames
            };

            if (data.ChosenOpponents != null)
            {
                foreach (var opponent in data.ChosenOpponents)
                {
                    if (opponent == null)
                    {
                        newModel.Opponents.Add(null);
                    }
                    else
                    {
                        newModel.Opponents.Add(_civilizationDataProviderService.FindEntity((Int32)opponent));
                    }
                }
            }

            for (var i = 0; i < data.ChosenVictoryType.Count; i++)
            {
                if (data.ChosenVictoryType[i])
                {
                    newModel.VictoryTypes.Add(_victoryTypeDataProviderService.FindEntity(i));
                }
            }

            for (var i = 0; i < data.ChosenAdvancedGameOptions.Count; i++)
            {
                if (data.ChosenAdvancedGameOptions[i])
                {
                    newModel.AdvancedGameOptions.Add(data.AdvancedGameOptions[i]);
                }
            }

            newModel.ChosenGame = showIndex;

            return View(newModel);
        }
    }
}