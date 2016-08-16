using ESN.Domain.Abstract;
using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ESN.Domain.Concrete
{
    public class EFProfileRepository : IProfileRepository
    {
        private EFDbContext context = new EFDbContext();

        //public EFProfileRepository()
        //{
        //    //string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ESN2.mdf");
        //    string mdfFilePath = HttpContext.Current.Server.MapPath("~/App_Data/ESN3.mdf");
        //    context = new EFDbContext(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={0};Integrated Security=True", mdfFilePath));
        //    //context = new EFDbContext(string.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\A\Documents\ESN2.mdf;Integrated Security=True;Connect Timeout=30", mdfFilePath));
        //}

        public IEnumerable<Profile> Profiles
        {
            get { return context.Profiles; }
            //get { return localContext; }
        }

        //Profile GetProfileById(Guid Id)
        //{
        //    var profile = Profiles
        //            .Where(p => p.ProfileId == Id)
        //            .First();
                    
        //    return profile;
        //}

        //List<Profile> localContext = new List<Profile>
        //{
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a1"), Gender = "male", dob = default(int), fName = "name1"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a2"), Gender = "male", dob = default(int), fName = "name2"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a3"), Gender = "male", dob = default(int), fName = "name3"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a4"), Gender = "male", dob = default(int), fName = "name4"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a5"), Gender = "male", dob = default(int), fName = "name5"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a6"), Gender = "female", dob = default(int), fName = "name6"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a7"), Gender = "female", dob = default(int), fName = "name7"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a8"), Gender = null, dob = default(int), fName = "name8"},
        //    new Profile { ProfileId = Guid.Parse("355ced56-1642-42a2-b98e-6e6ba31277a9"), Gender = null, dob = default(int), fName = "name9"},
        //};

        public bool SaveProfile(Profile profile)
        {
            if (profile.ProfileId == default(Guid))
            {
                profile.ProfileId = Guid.NewGuid();
                context.Profiles.Add(profile);
            }
            else
            {
                //foreach (var item in Profiles)
                //{
                //    if (item.ProfileId != profile.ProfileId) continue;
                //    item.fName = profile.fName;
                //    item.dob = profile.dob;
                //    item.Gender = profile.Gender;

                //    item.ImageData = profile.ImageData;
                //    item.ImageMimeType = profile.ImageMimeType;
                //}
                Profile dbEntry = context.Profiles.Find(profile.ProfileId);
                if (dbEntry != null)
                {
                    dbEntry.fName = profile.fName;
                    dbEntry.dob = profile.dob;
                    dbEntry.Annotation = profile.Annotation;
                    dbEntry.Gender = profile.Gender;
                    dbEntry.lName = profile.lName;
                    dbEntry.mName = profile.mName;
                    //dbEntry.ImageData = profile.ImageData;
                    //dbEntry.ImageMimeType = profile.ImageMimeType;
                }

            }
            
                return context.SaveChanges() > 0;
            
        }

        public Profile DeleteProfile(Guid? ProfileId)
        {
            Profile dbEntry = context.Profiles.Find(ProfileId);
            if (dbEntry != null)
            {
                context.Profiles.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
            //Profile forReturn = null;
            //foreach(var item in Profiles)
            //{
            //    if (item.ProfileId != ProfileId) continue;
            //    Profiles.Remove(item);
            //    forReturn = item;
            //}
            //return forReturn;
        }
    }
}
