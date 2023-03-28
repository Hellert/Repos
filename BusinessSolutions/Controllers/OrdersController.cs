using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessSolutions.Data;
using BusinessSolutions.Models;
using BusinessSolutions.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessSolutions.ViewModels;

namespace BusinessSolutions.Controllers
{ 

    public class OrdersController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly BusinessSolutionsContext _context;
        

        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrdersController(IUnitOfWork unitOfWork, BusinessSolutionsContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }


        // GET: Orders
        public Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate, string? SearchByNumber, DateTime? SearchByDate, string? SearchByProdiver)
        {
            if (toDate == null)
            {
                toDate = DateTime.Now;
            }
            if (fromDate == null)
            {
                fromDate = DateTime.Now.AddMonths(-1);
            }
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            IEnumerable<Order> orderList = _unitOfWork.Orders.GetAll(includeProperties: "Provider")
                .Where(c => c.Date >= fromDate && c.Date <= toDate); ;
            if (SearchByNumber != null)
            {
                orderList = orderList.Where(c => c.Number.Contains(SearchByNumber));
            }
            if (SearchByDate != null)
            {
                orderList = orderList.Where(c => c.Date.Date.Equals(SearchByDate));
            }
            if (SearchByProdiver != null)
            {
                orderList = orderList.Where(c => c.Provider.Name.Contains(SearchByProdiver));
            }
            return Task.FromResult<IActionResult>(View(orderList));
        }

        // GET: Orders/Details/id
        public  IActionResult Details(int id)
        {

            OrderVM = new OrderVM()
            {
                Order = _unitOfWork.Orders.Get(id, includeProperties: "Provider"),
                OrderItem = _unitOfWork.OrderItems.GetAll(o => o.OrderId == id)
            };
            ViewData["ProviderId"] = new SelectList(_unitOfWork.Provider.GetAll().ToList(), "Id", "Name");
            ViewBag.OrderId = id;
            return View(OrderVM);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ProviderId"] = new SelectList(_unitOfWork.Provider.GetAll().ToList(), "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Number,Date,ProviderId")] Order order)
        {
            //if (_context.Order.Any(i => (i.Number == order.Number && i.ProviderId == order.ProviderId)))
            //{
            //    ModelState.AddModelError("order.Number",
            //                              "The number is already in use for this Provider.");
            //}
            //if (!ModelState.IsValid)
            //{
            //    // if model is invalid, return the page with the model state errors.
            //    return View(order);
            //}
            _unitOfWork.Orders.Create(order);
                _unitOfWork.Commit();
                return RedirectToAction("Details", new {id = order.Id});
        }

        // GET: Orders/AddOrderItem (probably should be removed)
        public IActionResult AddOrderItem()
        {
            return PartialView(); 
        }

        // POST: Orders/AddOrderItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult AddOrderItem([Bind("OrderId,Name,Quantity,Unit")]OrderItem orderItem)
        {
            //if (orderItem.Name == _unitOfWork.Orders.Get(orderItem.OrderId).Number)
            //{
            //    ModelState.AddModelError("orderItem.Name",
            //                             "orderItem name can't be the same as Order.Number.");
            //}

            _unitOfWork.OrderItems.Create(orderItem);
            _unitOfWork.Commit();
                return RedirectToAction("Details", new {id = orderItem.OrderId});
        }

        // GET: Orders/EditOrder/id
        public IActionResult EditOrder(int id)
        {
            var order =  _unitOfWork.Orders.Get(id);
            ViewData["ProviderId"] = new SelectList(_unitOfWork.Provider.GetAll().ToList(), "Id", "Name");
            return View(order);
        }

        // POST: Orders/EditOrder/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult EditOrder(int id, [Bind("Id,Number,Date,ProviderId")] Order order)
        {
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();
            return RedirectToAction("Details", new { id = order.Id });
        }

        // GET: Orders/EditOrderItem/id
        public IActionResult EditOrderItem(int id)
        {
            var orderItem = _unitOfWork.OrderItems.Get(id);
            return View(orderItem);
        }

        // POST: Orders/EditOrderItem/id
        [HttpPost, ActionName("EditOrderItem")]
        public IActionResult EditOrderItem([Bind("Name,Quantity,Unit")] OrderItem orderItem)
        {
            _unitOfWork.OrderItems.Update(orderItem);
            _unitOfWork.Commit();
            return RedirectToAction("Details", new { id = orderItem.OrderId });
        }

        // POST: Orders/DeleteOrder/id
        [HttpPost, ActionName("DeleteOrder")]
        public  IActionResult DeleteOrder(int id)
        {
            _unitOfWork.Orders.Delete(id);
            _unitOfWork.Commit();
            return RedirectToAction("Index");
        }

        // POST: Orders/DeleteOrderItem/id
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            OrderItem orderItem =  _unitOfWork.OrderItems.Get(id);
            if (orderItem != null)
            {
                _unitOfWork.OrderItems.Delete(orderItem.Id);
                _unitOfWork.Commit();
                return RedirectToAction("Details", new { id = orderItem.OrderId });
            }
            return NotFound();
        }
    }
}
