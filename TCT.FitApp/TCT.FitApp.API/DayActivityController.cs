﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCT.FitApp.API
{
    [Route("[controller]")]
    [ApiController]
    public class DayActivityController : ControllerBase
    {
        // GET: api/<DayActivityController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayActivity>>> Get()
        {
            try
            {
                return Ok(await DayActivityManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<DayActivityController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DayActivity dayActivity, bool rollback = false)
        {
            try
            {
                return Ok(await DayActivityManager.Insert(dayActivity.DayId, dayActivity.ActivityId, 60, 2));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<DayActivityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool rollback = false)
        {
            try
            {
                return Ok(await DayActivityManager.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
