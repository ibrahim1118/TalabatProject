using Adminpanal.Hellper;
using Adminpanal.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Text.RegularExpressions;
using Talabat.Core;
using Talabat.Core.Entitys;
using Talabat.Core.Spcifications;
using Talabat.Repositiry;
using Product = Talabat.Core.Entitys.Product;

namespace Adminpanal.Controllers
{
    public class ProdcutController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProdcutController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var spac = new ProductSpacification();
            var produt = await unitOfWork.Repostitry<Product>().GetSpacAllAysnc(spac);
            var mapProduts = mapper.Map<IReadOnlyList<ProductVM>>(produt);
            return View(mapProduts);
        }
         
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.image==null)
                {
                    ModelState.AddModelError("image", "Image is reqired");
                    return View(productVM); 
                }
                productVM.ImageUrl = ImageSetting.UplodaImage(productVM.image, "products"); 
                var product = mapper.Map<Product>(productVM);   
                await unitOfWork.Repostitry<Product>().AddAysnc(product);
                await unitOfWork.IsComplet(); 
                return RedirectToAction("Index");
            }
            return View(productVM);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var spac = new ProductSpacification(id.Value);  
            var product = await unitOfWork.Repostitry<Product>().GetByIdSpacAysnc(spac);
            
            if (product is null)
                return NotFound();
            var productVM  = mapper.Map<ProductVM>(product);
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.image is not null)
                {
                    if (model.ImageUrl is not null)
                        ImageSetting.DeleteImage(model.ImageUrl, "products");
                    model.ImageUrl = ImageSetting.UplodaImage(model.image, "products");
                }
               
                var produt = mapper.Map<Product>(model); 
                await unitOfWork.Repostitry<Product>().Update(produt); 
                await unitOfWork.IsComplet();
                return RedirectToAction(nameof(Index));
            }
            return View(model); 
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var Product = await unitOfWork.Repostitry<Product>().GetByIdAysnc(id.Value);
            if (Product is null) return NotFound();
            var produtVm = mapper.Map<ProductVM>(Product);  
            return View (produtVm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductVM model)
        {
            if (ModelState.IsValid) { 
              var produc = mapper.Map<Product>(model);    
              await unitOfWork.Repostitry<Product>().Delete(produc);
            await unitOfWork.IsComplet();   
            return RedirectToAction(nameof(Index)); 
            }
            return View(model);
        }



	}

}
