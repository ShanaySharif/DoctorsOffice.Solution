using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoctorsOffice.Controllers
{
  public class PatientsController : Controller
  {
    private readonly DoctorsOfficeContext _db;

    public PatientsController(DoctorsOfficeContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Patient> model = _db.Patients.Include(patient => patient.Doctor).ToList();
      return View(model);
    }
    public ActionResult Create()
      {
        ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
        return View();
      }

      [HttpPost]
      public ActionResult Create(Patient patient)
      {
        if (patient.DoctorId == 0)
        {
          return RedirectToAction("Create");
        }
            _db.Patients.Add(patient);
            _db.SaveChanges();
            return RedirectToAction("Index");
          }
      public ActionResult Details(int id)
      {
          Patient thisPatient = _db.Patients
          .Include(patient => patient.Doctor)
          .Include(patient => patient.JoinEntities)
          .ThenInclude(join => join.Tag)
          .FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
      }
       public ActionResult Edit(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "Name");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult Edit(Patient patient)
    {
      _db.Patients.Update(patient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patient => patient.PatientId == id);
      _db.Patients.Remove(thisPatient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTag(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patients => patients.PatientId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult AddTag(Patient patient, int tagId)
    {
      #nullable enable
      PatientTag? joinEntity = _db.PatientTags.FirstOrDefault(join => (join.TagId == tagId && join.PatientId == patient.PatientId));
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.PatientTags.Add(new PatientTag() { TagId = tagId, PatientId = patient.PatientId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = patient.PatientId });
    }   

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      PatientTag joinEntry = _db.PatientTags.FirstOrDefault(entry => entry.PatientTagId == joinId);
      _db.PatientTags.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    } 
  }
}