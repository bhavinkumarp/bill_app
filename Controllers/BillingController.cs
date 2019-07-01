using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_core_role_based_authentication.Data;
using ASP.NET_core_role_based_authentication_master.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_core_role_based_authentication.Controllers
{ 
    public class BillingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Billing
        public async Task<IActionResult> Index(string searchString,string sortOrder)
        {     
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["CurrentFilter"] = searchString;
            // if (searchString != null)
            // {
            //     pageNumber = 1;
            // }
            // else
            // {
            //     searchString = currentFilter;
            // }
            // ViewData["CurrentFilter"] = searchString;

            var billings = from m in _context.Billing
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {                
                billings = billings.Where(s => s.ProviderName.Contains(searchString));
            }       
            else{
                billings = billings.Where(s => s.ProviderName.Contains("blank"));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    billings = billings.OrderByDescending(s => s.ProviderName);
                    break;                
                default:
                    billings = billings.OrderBy(s => s.ClaimNumber);
                    break;
            }     
            return View(await billings.ToListAsync());            
            //  int pageSize = 2;
            //  return View(await PaginatedList<Billings>.CreateAsync(billings.AsNoTracking(), pageNumber ?? 1, pageSize));       
            //return View(await _context.Billing.ToListAsync());
        }

        // GET: Billing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billing
                .SingleOrDefaultAsync(m => m.ClaimNumber == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // GET: Billing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimNumber,ProviderName,DOB,DOS,CPT1,CPT2,CPT3,CPT4,ICD1,ICD2,ICD3")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billing);
        }

        // GET: Billing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billing.SingleOrDefaultAsync(m => m.ClaimNumber == id);
            if (billing == null)
            {
                return NotFound();
            }
            return View(billing);
        }

        // POST: Billing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimNumber,ProviderName,DOB,DOS,CPT1,CPT2,CPT3,CPT4,ICD1,ICD2,ICD3")] Billing billing)
        {
            if (id != billing.ClaimNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingExists(billing.ClaimNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(billing);
        }

        // GET: Billing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billing
                .SingleOrDefaultAsync(m => m.ClaimNumber == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // POST: Billing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billing = await _context.Billing.SingleOrDefaultAsync(m => m.ClaimNumber == id);
            _context.Billing.Remove(billing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingExists(int id)
        {
            return _context.Billing.Any(e => e.ClaimNumber == id);
        }
    }
}
