using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotvvmMinutes.LoadingAnimation.Model
{
    public class CartDTO
    {

        public List<CartItemDTO> CartItems { get; set; }

        public int ItemCount => CartItems.Count(i => i.Price > 0);

        public decimal TotalPrice => CartItems.Sum(i => i.Price);

        public CartPromoDTO Promo { get; set; }
    }
}
