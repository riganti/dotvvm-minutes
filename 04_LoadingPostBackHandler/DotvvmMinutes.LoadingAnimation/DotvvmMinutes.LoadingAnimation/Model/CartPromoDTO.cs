using System.ComponentModel.DataAnnotations;
using System;

namespace DotvvmMinutes.LoadingAnimation.Model
{
    public class CartPromoDTO
    {

        [Required]
        public string Code { get; set; }

        public void Redeem()
        {
            throw new NotImplementedException();
        }
    }
}