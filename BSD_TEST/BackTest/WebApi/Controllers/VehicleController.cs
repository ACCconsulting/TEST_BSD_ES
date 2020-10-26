using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Vehicle;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VehicleController(IMediator mediator)
        {
            this._mediator = mediator;

        }
        //<List<Domain.Dtos.VehicleDto>>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var Result =  await _mediator.Send(new GetAllVehicle.Vehiclelist());
            return Ok(Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result= await _mediator.Send(new VehicleId.VehicleById { Id=id});
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(New.VehicleNew data)
        {
            var result= await _mediator.Send(data);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,Update.VehicleUpdate data)
        {
            data.VehicleId = id;
            var result=  await _mediator.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result= await _mediator.Send(new Delete.VehicleDelete { Id=id});
            return Ok(result);
        }

    }
}