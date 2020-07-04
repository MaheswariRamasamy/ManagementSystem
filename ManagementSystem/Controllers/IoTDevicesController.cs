using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Data;
using ManagementSystem.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ManagementSystem.Models.ViewModel;

namespace ManagementSystem.Controllers
{
    public class IoTDevicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public IoTDevicesController(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: IoTDevices
        public async Task<IActionResult> Index()
        {
            var viewModel = await mapper.ProjectTo<IoTDeviceViewModel>(_context.IoTDevice).ToListAsync();

            return View(viewModel);
        }

        // GET: IoTDevices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ioTDevice = await _context.IoTDevice
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ioTDevice == null)
            {
                return NotFound();
            }

            return View(ioTDevice);
        }

        // GET: IoTDevices/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: IoTDevices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsDeviceOn,UserId")] IoTDevice ioTDevice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ioTDevice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", ioTDevice.UserId);
            return View(ioTDevice);
        }

        // GET: IoTDevices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ioTDevice = await _context.IoTDevice.FindAsync(id);
            if (ioTDevice == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", ioTDevice.UserId);
            return View(ioTDevice);
        }

        // POST: IoTDevices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsDeviceOn,UserId")] IoTDevice ioTDevice)
        {
            if (id != ioTDevice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ioTDevice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IoTDeviceExists(ioTDevice.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", ioTDevice.UserId);
            return View(ioTDevice);
        }

        // GET: IoTDevices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ioTDevice = await _context.IoTDevice
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ioTDevice == null)
            {
                return NotFound();
            }

            return View(ioTDevice);
        }

        // POST: IoTDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ioTDevice = await _context.IoTDevice.FindAsync(id);
            _context.IoTDevice.Remove(ioTDevice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IoTDeviceExists(int id)
        {
            return _context.IoTDevice.Any(e => e.Id == id);
        }
    }
}
