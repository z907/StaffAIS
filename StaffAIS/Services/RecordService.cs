using Microsoft.EntityFrameworkCore;
using Model.Entities;
using ViewModel.VmEntities;

namespace StaffAIS.Services;

public class RecordService:BaseService
{
    public List<DisplayRecord> GetRecords()
    {
        db.Workers.Load();
        return db.Records.Include(r=>r.Worker).ToList().Select(r=>new DisplayRecord { Date = r.Date,Id = r.Id,Quantity = r.Quantity,WorkerName = r.Worker.Surname}).ToList();
    }
    
    public List<DisplayRecord> GetRecordsByDate(DateOnly StartDate,DateOnly EndDate,int wId)
    {
        db.Workers.Load();
        db.Workshops.Load();
        return db.Records.Include(r=>r.Worker).Include(r=>r.Worker.Workshop).
            ToList().Where(r=>r.Date<=EndDate && r.Date>=StartDate &&r.Worker.Workshop.Id==wId).Select(r=>new DisplayRecord 
            { Date = r.Date,
                Id = r.Id,
                Quantity = r.Quantity,
                WorkerName = r.Worker.Surname}).ToList();
    }

    public void DeleteRecord(int id)
    {
        db.Remove(db.Records.Find(id));
        db.SaveChanges();
    }
    public void EditRecord(Record item)
    {
        var a = db.Records.Find(item.Id);
        a.Quantity = item.Quantity;
        a.Date = item.Date;
        db.Update(a);
        db.SaveChanges();
    }
    public void AddRecord(Record item)
    {
        db.Add(item);
        db.SaveChanges();
    }
}