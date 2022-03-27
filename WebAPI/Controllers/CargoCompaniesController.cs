using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        ICargoCompanyService _cargoCompanyService;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCompanyService = cargoCompanyService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _cargoCompanyService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int cargoCompanyId)
        {
            var result = _cargoCompanyService.Get(cargoCompanyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcargocompanydetails")]
        public IActionResult GetCargoCompanyDetails()
        {
            var result = _cargoCompanyService.GetCargoCompanyDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcargocompanydetailsid")]
        public IActionResult GetCargoCompanyDetailsById(int cargoCompanyId)
        {
            var result = _cargoCompanyService.GetCargoCompanyDetails(cargoCompanyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcargocompanydetailsbystatus")]
        public IActionResult GetCargoCompanyDetailsByStatus(bool status)
        {
            var result = _cargoCompanyService.GetStatusByCargoCompanyDetails(status);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CargoCompany cargoCompany)
        {
            var result = _cargoCompanyService.Add(cargoCompany);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(CargoCompany cargoCompany)
        {
            var result = _cargoCompanyService.Delete(cargoCompany);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CargoCompany cargoCompany)
        {
            var result = _cargoCompanyService.Update(cargoCompany);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
