using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketsAsync(string basketId);
        Task<CustomerBasket> UpdateBasketsAsync(CustomerBasket basket);
        Task<bool> DeleteBasketsAsync(string basketId);
    }
}
