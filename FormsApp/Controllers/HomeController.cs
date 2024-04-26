using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string searchString,string category)
    {
        var products = Repository.Products;

        if(!String.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p=>p.Name.ToLower().Contains(searchString)).ToList();
        }
        if(!String.IsNullOrEmpty(category) && category !="0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }
     
        //  ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
      
        var model = new ProductViewModel {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };

        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product model,IFormFile imageFile)
    {
        var extension = Path.GetExtension(imageFile.FileName);
        var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
        var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);


        if(ModelState.IsValid)
        {
            using(var stream = new FileStream(path,FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            model.Image = randomFileName;
             Repository.CreateProduct(model);
             return RedirectToAction("Index");
        };
         ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
     }
 
    public IActionResult Edit(int? id)
    {
       
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Edit (int id,Product model,IFormFile imageFile)
    {
        if(ModelState.IsValid)
        {
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }

         ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }

    public IActionResult Delete(int? id)
    {
        var entity = Repository.Products.FirstOrDefault(p=> p.ProductId==id);
        if(entity == null)
        {
            return NotFound();
        }

        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");
    }


}
