using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warehouse.Contexts;
using warehouse.Models;
using warehouse.Models.Dto;

namespace warehouse.Controllers
{
    public class ShippingController(WarehouseDbContext context) : Controller
    {
        // GET: Shipping
        public async Task<IActionResult> Index()
        {
            return View(await context.Shippings.ToListAsync());
        }

        // GET: Shipping/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await context.Shippings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // GET: Shipping/Create
        public IActionResult Create()
        {
            var viewModel = new CreateShippingView();
            return View(viewModel);
        }

        // POST: Shipping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "BookingConfirmation,ActualShipment,CargoReady,ContNo,SealNo,Destination,InvoiceCodes,SalesDocumentCodes,Products")]
            CreateShippingView viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            await using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var shipping = new Shipping
                {
                    BookingConfirmation = viewModel.BookingConfirmation,
                    ActualShipment = viewModel.ActualShipment,
                    CargoReady = viewModel.CargoReady,
                    ContNo = viewModel.ContNo,
                    SealNo = viewModel.SealNo,
                    Destination = viewModel.Destination,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                context.Shippings.Add(shipping);
                await context.SaveChangesAsync();

                var invoiceCodes = viewModel.InvoiceCodes
                    .Split(',')
                    .Select(code => code.Trim()) // Trim spaces from each code
                    .ToList();
                foreach (var invoice in invoiceCodes.Select(code => new Invoice
                         {
                             ShippingId = shipping.Id,
                             InvoiceCode = code,
                             CreatedAt = DateTime.Now,
                             UpdatedAt = DateTime.Now
                         }))
                {
                    context.Invoices.Add(invoice);
                }

                var salesDocuments = viewModel.SalesDocumentCodes
                    .Split(',')
                    .Select(code => code.Trim()) // Trim spaces from each code
                    .ToList();
                foreach (var salesDocument in salesDocuments.Select(code => new SalesDocument
                         {
                             ShippingId = shipping.Id,
                             SalesDocumentCode = code,
                             CreatedAt = DateTime.Now,
                             UpdatedAt = DateTime.Now
                         }))
                {
                    context.SalesDocuments.Add(salesDocument);
                }

                if (viewModel.Products.Count != 0)
                {
                    foreach (var newProduct in viewModel.Products.Select(product => new Product
                             {
                                 ShippingId = shipping.Id,
                                 ContainerSize = product.ContainerSize,
                                 ContainerQ = product.ContainerQ,
                                 Po = product.Po,
                                 Model = product.Model,
                                 Packing = product.Packing,
                                 QtyPlan = product.QtyPlan,
                                 NwPack = product.NwPack,
                                 NwTotal = product.NwTotal,
                                 GwPack = product.GwPack,
                                 GwTotal = product.GwTotal,
                                 M3L = product.M3L,
                                 M3Total = product.M3Total,
                                 CreatedAt = DateTime.Now,
                                 UpdatedAt = DateTime.Now
                             }))
                    {
                        context.Products.Add(newProduct);
                    }
                }

                await context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Rollback the transaction if any error occurs
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to handle it elsewhere if needed
            }
        }

        // GET: Shipping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await context.Shippings.FindAsync(id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // POST: Shipping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,BookingConfirmation,ActualShipment,CargoReady,ContNo,SealNo,Destination")]
            Shipping shipping)
        {
            if (id != shipping.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(shipping);
            try
            {
                context.Update(shipping);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingExists(shipping.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Shipping/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipping = await context.Shippings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipping == null)
            {
                return NotFound();
            }

            return View(shipping);
        }

        // POST: Shipping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipping = await context.Shippings.FindAsync(id);
            if (shipping != null)
            {
                context.Shippings.Remove(shipping);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingExists(int id)
        {
            return context.Shippings.Any(e => e.Id == id);
        }
    }
}