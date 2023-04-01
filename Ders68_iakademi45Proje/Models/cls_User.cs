using Ders68_iakademi45Proje.Models.MVVM;
using Microsoft.EntityFrameworkCore;
using System.Text;
using XSystem.Security.Cryptography;

namespace Ders68_iakademi45Proje.Models
{
    public class cls_User
    {
        iakademi45Context context = new iakademi45Context();
        public async Task<User> loginControl(User user)
        {
            User? usr = await context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password && u.IsAdmin == true && u.Active == true);
            return usr;
        }
        public static User? SelectMemberInfo(string email)
        {
            using (iakademi45Context context = new iakademi45Context())
            {
                User? user = context.Users.FirstOrDefault(u => u.Email == email);
                return user;
            }
        }
        public static string MemberControl(User user)
        {
            using (iakademi45Context context = new iakademi45Context())
            {
                string answer = "";
                try
                {
                    string md5sifre = MD5Sifrele(user.Password);
                    User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == md5sifre);
                    if (usr == null)
                    {
                        //kullanıcı yanlış email veya şifre girdi
                        answer = "error";
                    }
                    else
                    {
                        //kullanıcı veri tabanında kayıtlı
                        if (usr.IsAdmin == true)
                        {
                            answer = "admin";
                        }
                        else
                        {
                            answer = usr.Email;
                        }
                    }
                }
                catch (Exception)
                {
                    return "HATA";
                    throw;
                }
                return answer;
            }
        }
        public static string MD5Sifrele(string value)
        {
            using (iakademi45Context context = new iakademi45Context())
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] btr = Encoding.UTF8.GetBytes(value);
                btr = md5.ComputeHash(btr);
                StringBuilder sb = new StringBuilder();
                foreach (byte item in btr)
                {
                    sb.Append(item.ToString("x2").ToLower());
                }
                return sb.ToString();
            }
        }
        public static bool AddUser(User user)
        {
            using (iakademi45Context context = new iakademi45Context())
            {
                try
                {
                    user.Active = true;
                    user.IsAdmin = false;
                    user.Password = MD5Sifrele(user.Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }
        }
        public static bool loginEmailControl(User user)
        {
            using (iakademi45Context context = new iakademi45Context())
            {
                //eğer cevap false ise kayıt yapılacak. aynı email yoksa false, varsa true döner
                User? usr = context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (usr == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}