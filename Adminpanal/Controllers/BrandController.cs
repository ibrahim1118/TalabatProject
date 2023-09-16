using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entitys;

namespace Adminpanal.Controllers
{
	public class BrandController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public BrandController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task<ActionResult> Index()
		{
			var Brands = await unitOfWork.Repostitry<ProductBrand>().GetAllAysnc(); 
			return View(Brands);
		}

		[HttpPost]
		public async Task<IActionResult> Add(ProductBrand modle)
		{
			if (ModelState.IsValid)
			{
			    await unitOfWork.Repostitry<ProductBrand>().AddAysnc(modle);
				await unitOfWork.IsComplet(); 
			}
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null)
				return BadRequest(); 
			var brand = await unitOfWork.Repostitry<ProductBrand>().GetByIdAysnc(id.Value);
			if (brand is null)
				return NotFound(); 
			await unitOfWork.Repostitry<ProductBrand>().Delete(brand);
			await	unitOfWork.IsComplet();	
			return RedirectToAction("Index");
		}
	}
}
