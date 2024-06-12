using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using ViewModel.VmEntities;

namespace StaffAIS.Services;

public class WorkerService:BaseService
{
    public List<Worker> GetWorkersByWNumber(int number)
    {
        db.Workshops.Load();
        return db.Workers.Include(w => w.Workshop).Where(w => w.Workshop.Number == number).ToList();
    }
    public List<DisplayWorker> GetWorkersByWNumberD(int number)
    {
        db.Workshops.Load();
        return db.Workers.Include(w => w.Workshop).Where(w => w.Workshop.Number == number).ToList().
            Select(w=>new DisplayWorker
            {
                Id=w.Id,
                HireDate = w.HireDate,
                Name=w.Name,
                Surname = w.Surname,
                LastName = w.LastName,
                Wage = w.Wage,
                WorkshopNumber = w.Workshop.Number
            }).ToList();
    }
    public List<DisplayWorker> GetWorkersD()
    {
        db.Workshops.Load();
        return db.Workers.Include(w => w.Workshop).ToList().
            Select(w=>new DisplayWorker
            {
                Id=w.Id,
                HireDate = w.HireDate,
                Name=w.Name,
                Surname = w.Surname,
                LastName = w.LastName,
                Wage = w.Wage,
                WorkshopNumber = w.Workshop.Number
            }).ToList();
    }

    public List<Worker> GetWorkersPlusAll(int wNumber)
    {
        db.Workshops.Load();
        var w=db.Workers.Include(w => w.Workshop).Where(w => w.Workshop.Number == wNumber).ToList();
        w.Add(new Worker{Id=0,Surname = "Все"});
        return w;
    }
    public void DeleteWorker(int id)
    {
        db.Records.Load();
        var r = db.Workers.Find(id).Records;
        db.Records.RemoveRange(r);
        db.SaveChanges();
        db.Workers.Remove(db.Workers.Find(id));
        db.SaveChanges();
    }
    public string GetWorkerName(int recId)
    {
        db.Workers.Load();
        return db.Records.Include(r => r.Worker).Where(r=>r.Id==recId).First().Worker.Surname;
    }

    public void AddWorker(Worker item)
    {
        db.Workers.Add(item);
        db.SaveChanges();
    }

    public Worker GetWorkerById(int id)
    {
        db.Workshops.Load();
        return db.Workers.Include(w => w.Workshop).First(w => w.Id == id);
    }
    public void EditWorker(Worker item)
    {
        var a = db.Workers.Find(item.Id);
        a.Name = item.Name;
        a.Surname = item.LastName;
        a.LastName = item.LastName;
        a.Wage = item.Wage;
        a.Name = item.Name;
        db.Workers.Update(a);
        db.SaveChanges();
    }
   
}