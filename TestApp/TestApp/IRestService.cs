using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    interface IRestService
    {
        Task<string[]> RefreshDataAsync();

        Task SaveBookingAsync(Booking item, bool isNewItem);
    }
}
