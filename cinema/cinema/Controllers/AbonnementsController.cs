#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;
using cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QRCoder;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace cinema.Controllers
{

    public class AbonnementsController : Controller
    {
        private readonly IAbonnementRepository _abonnementRepository;

        public AbonnementsController(IAbonnementRepository abonnementRepository)
        {
            _abonnementRepository = abonnementRepository;
        }

        // GET: Abonnements
        [Authorize(Roles = "Kassamedewerker")]
        public async Task<IActionResult> Index()
        {
            return View(await _abonnementRepository.ListOfAllAbonnements());
        }

        // GET: Abonnements/Details/5
        [Authorize(Roles = "Kassamedewerker")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _abonnementRepository.FindAbonnementById(id);
            if (abonnement == null)
            {
                return NotFound();
            }

            var aboQR = abonnement.AbboQR;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(aboQR, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);

            // Byte to image
            Bitmap bmp;
            using (var ms = new MemoryStream(qrCodeAsBitmapByteArr))
            {
                Bitmap qrCodeImage = new Bitmap(ms);
                ViewBag.qrImage = BitmapToBytes(qrCodeImage);
            }


            return View(abonnement);
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        // GET: Abonnements/Myabonnement/{username}
        [HttpGet]
        [Authorize(Roles = "Bezoeker")]
        [Route("/Abonnements/Myabonnement/{username}")]
        public async Task<IActionResult> Myabonnement(string username)
        {
            if (username == null)
            {
                return NotFound();
            }

            var abonnement =  _abonnementRepository.AbonnementByName(username);
            if (abonnement == null)
            {
                return NotFound();
            }

            var aboQR = abonnement.AbboQR;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(aboQR, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);

            //From here on, you can implement your platform-dependent byte[]-to-image code 

            //e.g. Windows 10 - Full .NET Framework
            Bitmap bmp;
            using (var ms = new MemoryStream(qrCodeAsBitmapByteArr))
            {
                Bitmap qrCodeImage = new Bitmap(ms);
                ViewBag.qrImage = BitmapToBytes(qrCodeImage);
            }


            return View(abonnement);
        }


        // GET: Abonnements/Create
        [Authorize(Roles = "Kassamedewerker")]
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Abonnements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Kassamedewerker")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AbboQR,StartDate,ExpireDate,AbboName,Expired")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                _abonnementRepository.Add(abonnement);
                _abonnementRepository.SaveAbonnement();
                return RedirectToAction(nameof(Index));
            }
            return View(abonnement);
        }

        // GET: Abonnements/Edit/5
        [Authorize(Roles = "Kassamedewerker")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _abonnementRepository.FindAbonnementByIdAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }
            return View(abonnement);
        }
        
        // POST: Abonnements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Kassamedewerker")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AbboQR,StartDate,ExpireDate,AbboName,Expired")] Abonnement abonnement)
        {
            if (id != abonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _abonnementRepository.UpdateAbonnement(abonnement);
                    _abonnementRepository.SaveAbonnement();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonnementExists(abonnement.Id))
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
            return View(abonnement);
        }

        // GET: Abonnements/Delete/5
        [Authorize(Roles = "Kassamedewerker")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _abonnementRepository.FindAbonnementById(id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }
        
        // POST: Abonnements/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Kassamedewerker")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonnement = await _abonnementRepository.FindAbonnementByIdAsync(id);
            _abonnementRepository.RemoveAbonnement(abonnement);
            _abonnementRepository.SaveAbonnement();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonnementExists(int id)
        {
            return _abonnementRepository.AbonnementExists(id);
        }
    }
}
