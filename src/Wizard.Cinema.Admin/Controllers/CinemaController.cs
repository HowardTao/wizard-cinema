﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Wizard.Cinema.Remote.ApplicationServices;
using Wizard.Cinema.Remote.Repository.Condition;

namespace Wizard.Cinema.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : BaseController
    {
        private readonly CityService _cityService;
        private readonly HallService _hallService;
        private readonly CinemaService _cinemaService;

        public CinemaController(CityService cityService, HallService hallService, CinemaService cinemaService)
        {
            this._cityService = cityService;
            this._hallService = hallService;
            this._cinemaService = cinemaService;
        }

        public IActionResult SearchCity(string keyword)
        {
            var citys = this._cityService.Search(keyword);
            return new JsonResult(citys);
        }

        public IActionResult SearchCinema(int cityId, string keyword, int page = 1, int size = 10)
        {
            var cinemas = this._cinemaService.GetByCityId(new SearchCinemaCondition()
            {
                CityId = cityId,
                Keyword = keyword,
                PageSize = size,
                PageNow = page
            });

            return Ok(cinemas.Records.Select(x => new
            {
                x.CinemaId,
                x.Name,
                x.Address
            }));
        }
    }
}