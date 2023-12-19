namespace DMS.Web.Seed
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminUsers(UserManager<ApplicationUser> usreManager)
        {

            ApplicationUser admin = new()
            {
                UserName = "admin",
                Email = "essames949@gmail.com",
                FullName = "Admin",
                EmailConfirmed = true
            };
            var user = await usreManager.FindByEmailAsync(admin.Email);
            if (user is null)
            {
                await usreManager.CreateAsync(admin, "Asd@1234");
                await usreManager.AddToRoleAsync(admin, AppRoles.Admin);
            }




            ApplicationUser doctor = new()
            {
                UserName = "doctor",
                Email = "doctor@dms.com",
                FullName = "doctor",
                EmailConfirmed = true
            };
            var doctorUser = await usreManager.FindByEmailAsync(doctor.Email);
            if (doctorUser is null)
            {
                await usreManager.CreateAsync(doctor, "Asd@1234");
                await usreManager.AddToRoleAsync(doctor, AppRoles.Doctors);
            }




            ApplicationUser secretary = new()
            {
                UserName = "secretary",
                Email = "secretary@dms.com",
                FullName = "secretary",
                EmailConfirmed = true
            };
            var secretaryUser = await usreManager.FindByEmailAsync(secretary.Email);
            if (secretaryUser is null)
            {
                await usreManager.CreateAsync(secretary, "Asd@1234");
                await usreManager.AddToRoleAsync(secretary, AppRoles.Secretary);
            }

        }
    }
}
