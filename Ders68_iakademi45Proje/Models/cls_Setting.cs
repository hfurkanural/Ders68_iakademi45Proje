using Ders68_iakademi45Proje.Models.MVVM;

namespace Ders68_iakademi45Proje.Models
{
    public class cls_Setting
    {
        iakademi45Context context = new iakademi45Context();
        public Setting SettingDetails()
        {
            Setting? settings = context.Settings.FirstOrDefault(s => s.SettingID == 1);
            return settings;
        }
        public static bool SettingUpdate(Setting settings)
        {
            try
            {
                //metod static olduğu için metodu burada tanımalamak zorundayız
                using (iakademi45Context context = new iakademi45Context())
                {
                    context.Update(settings);
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
