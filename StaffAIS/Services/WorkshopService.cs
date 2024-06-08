using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using Model.Context;
using Model.Entities;
using ViewModel.VmEntities;

namespace StaffAIS.Services;

public class WorkshopService:BaseService
{
    public List<DisplayWorkshop> GetWorkshops()
    {
        
        db.Records.Load();
        db.Workers.Load();
       return db.Workshops.ToList().Select(w=>new DisplayWorkshop
        {
            Id=w.Id,Number = w.Number,
            StaffCap = w.StaffCap,
            StaffPresent = w.Workers.Count,
            LastMonthProduction=GetProductionForLastMonth(w.Id)
        }).ToList();
    }
    public List<Workshop> GetWorkshopsDefault()
    {
        
        db.Records.Load();
        db.Workers.Load();
        return db.Workshops.Include(w=>w.Workers).ToList();
    }

    private int GetProductionForLastMonth(int wId)
    {
        db.Records.Load();
        db.Workers.Load();
        DateOnly date = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
        var l = db.Records.Include(w=>w.Worker).Where(r => r.Date >date ).ToList();
        var wr = db.Workshops.Find(wId).Workers.ToList();
        if (wr.Count == 0) return 0;
        var res = l.Join(wr, r => r.Worker, w => w, 
            (r, w) =>
            new { Quantity = r.Quantity }).ToList();
      return  (int)res.Sum(r => r.Quantity);
    }
    public bool DeleteWorkshop(int id)
    {
        db.Workers.Load();
        if (db.Workshops.Find(id).Workers.Count() > 0) return false;
        db.Workshops.Remove(db.Workshops.Find(id));
        db.SaveChanges();
        return true;
    }
    public bool EditWorkshop(DisplayWorkshop item)
    {
        db.Workers.Load();
        Workshop w = db.Workshops.Find(item.Id);
        w.StaffCap = item.StaffCap;
        w.Number = item.Number;
        db.Workshops.Update(w);
        db.SaveChanges();
        return true;
    }

    public void AddWorkshop(Workshop item)
    {
        db.Workshops.Add(item);
        db.SaveChanges();
    }

    public Workshop GetWorkshopById(int id)
    {
        return db.Workshops.Find(id);
    }
}