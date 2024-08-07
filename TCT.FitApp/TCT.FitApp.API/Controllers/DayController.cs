﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TCT.FitApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DayController : ControllerBase
    {
        // returns all of the days
        // GET: api/<DayController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Day>>> Get()
        {
            try
            {
                return Ok(await DayManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{userId:Guid}/{date:DateTime}")]
        public async Task<ActionResult<Day>> Get(Guid userId, DateTime date)
        {
            try
            {
                return Ok(await DayManager.Load(userId, date));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Day>> Get(Guid id)
        {
            try
            {
                return Ok(await DayManager.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //inserts a day
        // POST api/<DayController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Day day, bool rollback = false)
        {
            try
            {
                return Ok(await DayManager.Insert(day, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //updates a day
        // PUT api/<DayController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Day day, bool rollback = false)
        {
            try
            {
                return Ok(await DayManager.Update(day, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<DayController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool rollback = false)
        {
            try
            {
                return Ok(await DayManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get a list of days for reporting
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("GenerateReport")]
        public async Task<IActionResult> GenerateReport(Guid userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(await DayManager.LoadReport(userId, startDate, endDate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
