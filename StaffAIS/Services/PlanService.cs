using Model.Entities;

namespace StaffAIS.Services;

public class PlanService:BaseService
{
    public List<Plan> GetPlans()
    {
        return db.Plans.ToList();
    }

    public void DeletePlan(int id)
    {
        db.Plans.Remove(db.Plans.Find(id));
        db.SaveChanges();
    }
    public void EditPlan(Plan item)
    {
       var a= db.Plans.Find(item.Id);
       a.Quantity = item.Quantity;
        db.Plans.Update(a);
        db.SaveChanges();
    }
    public void AddPlan(Plan item)
    {
        db.Plans.Add(item);
        db.SaveChanges();
    }
}