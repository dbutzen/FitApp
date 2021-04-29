using Microsoft.AspNetCore.Http;
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
    public class ItemTypeController : ControllerBase
    {
        /// <summary>
        /// Return a list of itemTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemType>>> Get()
        {
            try
            {
                return Ok(await ItemTypeManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        

        /// <summary>
        /// Get an itemType by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ItemType>> Get(Guid id)
        {
            try
            {
                return Ok(await ItemTypeManager.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Insert a new itemType
        /// </summary>
        /// <param name="ItemType"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemType ItemType, bool rollback = false)
        {
            try
            {
                return Ok(await ItemTypeManager.Insert(ItemType, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update an itemType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ItemType"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ItemType ItemType, bool rollback = false)
        {
            try
            {
                return Ok(await ItemTypeManager.Update(ItemType, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete an itemType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rollback"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, bool rollback = false)
        {
            try
            {
                return Ok(await ItemTypeManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
