using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelApplication.Data;
using HotelApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelApplication.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public object PriceOfBooking { get; private set; }

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Booking.Include(b => b.Guest).Include(b => b.Room);

            foreach(Booking b in applicationDbContext)
            {
                b.TotalPrice = TotalPriceOfBooking(b, b.Room);
            }

            return View(await applicationDbContext.ToListAsync());
        }

        public double TotalPriceOfBooking(Booking booking, Room room)
        {
            var TotalPrice = (booking.EndDate - booking.StartDate).TotalDays * (double)room.RoomPrice;

            return TotalPrice;
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .SingleOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [Authorize(Roles = "Administrator, Receptionist")]
        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomName");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,RoomID,GuestId,PaidBooking,StartDate,EndDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Users, "Id", "Id", booking.GuestId);
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomID", booking.RoomID);
            return View(booking);

        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Users, "Id", "Id", booking.GuestId);
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomID", booking.RoomID);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,RoomID,GuestId,PaidBooking,StartDate,EndDate,TotalPrice")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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
            ViewData["GuestId"] = new SelectList(_context.Users, "Id", "Id", booking.GuestId);
            ViewData["RoomID"] = new SelectList(_context.Room, "RoomID", "RoomID", booking.RoomID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Guest)
                .Include(b => b.Room)
                .SingleOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.SingleOrDefaultAsync(m => m.BookingID == id);
            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }
    }
}
