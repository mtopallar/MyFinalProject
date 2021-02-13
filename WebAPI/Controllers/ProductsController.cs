﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //IoC Container -- Inversion of control (IProduct ın somutu ne çakılması) bellekte bir tane new ledim.
        //AOP tüm metodları loglamak ILogger.Log yerine [LogAspect] --> bir metodun önünde veya sonunda, yada bir metod hata verdiğinde çalışan kod parçacıklarına AOP diyoruz. Business içinde business yazıyoruz.[Transection], [Performans] [LogAspect] [RomoveCache] vs vs. Autofac bize AOP imkanı da IoC imkanı da sunuyor.

        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")] //buradaki alias ismi önemli. Client tarafında bu isimle kullanılacak api.
        public IActionResult GetAll()
        {
            //Swagger - API için hazır dökümantasyon
            //Dependancy chain
            
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}