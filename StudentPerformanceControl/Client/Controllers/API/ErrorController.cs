using System;
using System.Threading.Tasks;
using DataCore.Exceptions;
using Entity.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers.API
{
    public class ErrorController : ControllerBase
    {
        protected async Task<IActionResult> HandleRequestAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var result = await action();
                return Ok(result);
            }
            catch (SPCException ex)
            {
                if (ex.StatusCode == StatusCodes.Status400BadRequest)
                {
                    return BadRequest(ex.Message);
                }

                if (ex.StatusCode == StatusCodes.Status404NotFound)
                {
                    return NotFound(ex.Message);
                }
            }
            
            return BadRequest("Unknown error");
        }

        protected async Task<IActionResult> HandleRequestAsync(Func<Task> action)
        {
            try
            {
                await action();
                return Ok();
            }
            catch (SPCException ex)
            {
                if (ex.StatusCode == StatusCodes.Status400BadRequest)
                {
                    return BadRequest(ex.Message);
                }

                if (ex.StatusCode == StatusCodes.Status404NotFound)
                {
                    return NotFound(ex.Message);
                }
            }
            
            return BadRequest("Unknown error");
        }
    }
}