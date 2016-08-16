using ESN.Domain.Abstract;
using ESN.Domain.Concrete;
using ESN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESN3.WebUI.Models
{
    public static class Extensions
    {
        public static int GetAge(this DateTime BirthDate)
        {
            int result = DateTime.Now.Year - BirthDate.Year;
            if (DateTime.Now.Month < BirthDate.Month || (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day < BirthDate.Day))
            {
                result--;
            }

            return result;
        }

        

        public static TimeSpan GetIntervalOfNow(this DateTime dateTime)
        {
            return DateTime.Now - dateTime;
        }
    }

    


}
