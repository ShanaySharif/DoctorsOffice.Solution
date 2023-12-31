using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoctorsOffice.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly DoctorsOfficeContext _db;

        public DoctorsController(DoctorsOfficeContext db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            List<Doctor> model = _db.Doctors.ToList();
            return View(model);
        }
        
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
          Doctor thisDoctor = _db.Doctors
                .Include(doctor => doctor.Patients)
                .ThenInclude(patient => patient.JoinEntities)
                .ThenInclude(join => join.Tag)
                .FirstOrDefault(doctor => doctor.DoctorId == id);
              return View(thisDoctor);
        }
        public ActionResult Edit(int id)
        {
          Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
          return View(thisDoctor);
        }
        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
          _db.Doctors.Update(doctor);
          _db.SaveChanges();
          return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
          Doctor thisDoctor =  _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
          return View(thisDoctor);
        }
        [HttpPost, ActionName("Delete")]
         public ActionResult DeleteConfirmed(int id)
        {
          Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
          _db.Doctors.Remove(thisDoctor);
          _db.SaveChanges();
          return RedirectToAction("Index");

      
        }
    }
}
