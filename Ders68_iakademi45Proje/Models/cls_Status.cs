using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;

namespace Ders68_iakademi45Proje.Models
{
    public class cls_Status
    {
        iakademi45Context context = new iakademi45Context();
        public async Task<List<Status>> StatusSelect()
        {
            List<Status> statuses = await context.Statuses.ToListAsync();
            return statuses;
        }
        public static bool StatusInsert(Status status)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    context.Add(status);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public async Task<Status> StatusDetails(int? id)
        {
            Status? statuses = await context.Statuses.FindAsync(id);
            return statuses;

        }
        public static bool StatusUpdate(Status status)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    context.Update(status);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public static bool StatusDelete(int id)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    Status? statuses = context.Statuses.FirstOrDefault(c => c.StatusID == id);
                    statuses.Active = false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }


    }
}
