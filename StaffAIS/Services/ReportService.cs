using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Npgsql;
using ViewModel.VmEntities;

namespace StaffAIS.Services;

public class ReportService:BaseService
{
    public List<PaymentReport> GetPaymentReports(DateOnly StartDate, DateOnly EndDate)
    {
        db.Records.Load();
        db.Workers.Load();
        db.Workshops.Load();
        var l= db.Workers.Include(w => w.Workshop).Select(w=>
            new PaymentReport
            {
                FIO = w.Surname+" "+w.Name+" "+w.LastName,
                Count=(int)w.Records.Where(r=>r.Worker.Id==w.Id && r.Date<=EndDate && r.Date>=StartDate).Sum(r=>r.Quantity),
                Wage = (int)w.Wage,
                Workshop = (int)w.Workshop.Number
            }).ToList();
        foreach (var x in l)
        {
            x.ToPay = x.Count * x.Wage;
        }

        return l;
    }

    public List<ProdReportData>GetProdPerDay(int month,int year,int WorkerId,int wNumber)
    {
        db.Records.Load();
        db.Workshops.Load();
        List<ProdReportData> result=new List<ProdReportData>();
        if (WorkerId == 0)
        {
            int sum;
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                sum = 0;
               var r=db.Workers.Include(w=>w.Workshop).
                        Where(w=>w.Workshop.Number==wNumber);
                    foreach (var x in r)
                    {
                        var q= x.Records.Where(r =>
                            r.Date.Value.Month == month && r.Date.Value.Year == year && r.Date.Value.Day == i);
                        if(q.Count()!=0) sum+=(int)q.First().Quantity;
                    }
                    result.Add(new ProdReportData{Day=i,Quantity = sum});
            }
            
        }
        else
        {
           
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                var w = db.Workers.Find(WorkerId).Records.Where(
                    r => r.Date.Value.Month == month && r.Date.Value.Year == year && r.Date.Value.Day == i);
                if(w.Count()!=0)result.Add(new ProdReportData{Day = i,Quantity = (int)w.First().Quantity});
                else
                {
                    result.Add(new ProdReportData{Day = i,Quantity = 0});
                }
            }
        }

        return result;
    }

    public List<PlanReportData> GetPlanInYear(int year)
    {
        List<PlanReportData> l = new List<PlanReportData>();
        for (int i = 1; i <= 12; i++)
        {
            if (db.Plans.ToList().Exists(p => Convert.ToInt32(p.Year) == year)) l.Add(new PlanReportData{Month = i,Quantity = Convert.ToInt32(db.Plans.First(p => Convert.ToInt32(p.Year) == year).Quantity)}) ;
            else l.Add(new PlanReportData{Month = i,Quantity = 0});
        }

        return l;
    }
    
    public List<PlanReportData> GetActualInYear(int year)
    {
        List<PlanReportData> l = new List<PlanReportData>();
        
        for (int i = 1; i <= 12; i++)
        {
            int sum = db.Records.ToList().Exists(r => r.Date.Value.Month == i && r.Date.Value.Year == year)?
                (int)db.Records.Where(r => r.Date.Value.Month == i && r.Date.Value.Year == year).Sum(r => r.Quantity):0;
            l.Add(new PlanReportData{Month = i,Quantity = sum});
        }

        return l;
    }
}