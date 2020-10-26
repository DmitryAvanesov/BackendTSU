﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend5.Data;
using Backend5.Models;
using Backend5.Models.ViewModels;

namespace Backend5.Controllers
{
    public class AnalysesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalysesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Analyses
        public async Task<IActionResult> Index(Int32? patientId)
        {
            if (patientId == null)
            {
                return this.NotFound();
            }

            var patient = await this._context.Patients
                .SingleOrDefaultAsync(x => x.Id == patientId);

            if (patient == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Patient = patient;
            var analyses = await this._context.Analyses
                .Include(w => w.Patient)
                .Include(w => w.Lab)
                .Where(x => x.PatientId == patientId)
                .ToListAsync();

            return this.View(analyses);
        }

        // GET: Analyses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses
                .Include(d => d.Patient)
                .Include(d => d.Lab)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (analysis == null)
            {
                return NotFound();
            }

            return View(analysis);
        }

        // GET: Analyses/Create
        public async Task<IActionResult> Create(Int32? patientId)
        {
            if (patientId == null)
            {
                return this.NotFound();
            }

            var patient = await this._context.Patients
                .SingleOrDefaultAsync(x => x.Id == patientId);

            if (patient == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Patient = patient;
            this.ViewData["LabId"] = new SelectList(this._context.Labs, "Id", "Name");
            return this.View(new AnalysisViewModel());
        }

        // POST: Analyses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Int32? patientId, AnalysisViewModel model)
        {
            if (patientId == null)
            {
                return this.NotFound();
            }

            var patient = await this._context.Patients
                .SingleOrDefaultAsync(x => x.Id == patientId);

            if (patient == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                var analysis = new Analysis
                {
                    PatientId = patient.Id,
                    LabId = model.LabId,
                    Type = model.Type,
                    Date = model.Date,
                    Status = model.Status
                };

                this._context.Add(analysis);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { patientId = patient.Id });
            }

            this.ViewBag.Patient = patient;
            return this.View(model);
        }

        // GET: Analyses/Edit/5
        public async Task<IActionResult> Edit(Int32? id, Int32? patientId)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var analysis = await this._context.Analyses.SingleOrDefaultAsync(m => m.Id == id);
            if (analysis == null)
            {
                return this.NotFound();
            }

            var model = new AnalysisViewModel
            {
                LabId = analysis.LabId,
                Type = analysis.Type,
                Date = analysis.Date,
                Status = analysis.Status
            };

            if (patientId == null)
            {
                return this.NotFound();
            }

            var patient = await this._context.Patients
                .SingleOrDefaultAsync(x => x.Id == patientId);

            if (patient == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Patient = patient;
            this.ViewData["LabId"] = new SelectList(this._context.Labs, "Id", "Name");
            return this.View(model);
        }

        // POST: Analyses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int32? id, AnalysisViewModel model)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var analysis = await this._context.Analyses.SingleOrDefaultAsync(m => m.Id == id);
            if (analysis == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                analysis.LabId = model.LabId;
                analysis.Type = model.Type;
                analysis.Date = model.Date;
                analysis.Status = model.Status;
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { patientId = analysis.PatientId });
            }

            return this.View(model);
        }

        // GET: Analyses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses
                .Include(d => d.Patient)
                .Include(d => d.Lab)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (analysis == null)
            {
                return NotFound();
            }

            return View(analysis);
        }

        // POST: Analyses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32 id)
        {
            var analysis = await this._context.Analyses.SingleOrDefaultAsync(m => m.Id == id);
            this._context.Analyses.Remove(analysis);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction("Index", new { patientId = analysis.PatientId });
        }

        private bool AnalysisExists(int id)
        {
            return _context.Analyses.Any(e => e.PatientId == id);
        }
    }
}
