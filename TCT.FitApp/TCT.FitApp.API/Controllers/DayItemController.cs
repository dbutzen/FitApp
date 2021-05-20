using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class DayItemController : ControllerBase
    {
        /// <summary>
        /// Return a list of items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BL.Models.DayItem>>> Get()
        {
            try
            {
                return Ok(await DayItemManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


       


        /// <summary>
        /// Insert a new item
        /// </summary>
        /// <param name="DayItem"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DayItem DayItem, bool rollback = false)
        {
            try
            {
                return Ok(await DayItemManager.InsertWithDayItem(DayItem , rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="dayId"></param>
        /// <param name="itemId"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpDelete("{dayId}/{itemId}")]
        public async Task<IActionResult> Delete(Guid dayId, Guid itemId, bool rollback = false)
        {
            try
            {
                return Ok(await DayItemManager.Delete(dayId, itemId, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool rollback = false)
        {
            try
            {
                return Ok(await DayItemManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
