using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.API.ViewModel;
using PaintingAndSound.DAL.Services;
using PaintingAndSound.DB;

namespace PaintingAndSound.API.Controllers
{
    [Route("api/Radios")]
    [ApiController]
    public class RadioController : ControllerBase
    {
        private readonly DAL.Services.RadioServiceIDAL<Radio> radioServiceIDAL;

        public RadioController(RadioServiceIDAL<Radio> radioServiceIDAL)
        {
            this.radioServiceIDAL = radioServiceIDAL;
        }
        [HttpGet(Name = nameof(GetRdios))]
        public IActionResult GetRdios()
        {
           var radios= radioServiceIDAL.GetAll();
            RadioViewModel radioViewModel = new RadioViewModel();
            foreach(var i in radios)
            {
                radioViewModel.Id = i.Id;
                radioViewModel.Name = i.Name;
                radioViewModel.RadioUrl = i.RadioUrl;
                radioViewModel.IsDelete = i.IsDelete;
                radioViewModel.DateTime = i.DateTime;
            }
            return Ok(radioViewModel);
        }
    }
}
