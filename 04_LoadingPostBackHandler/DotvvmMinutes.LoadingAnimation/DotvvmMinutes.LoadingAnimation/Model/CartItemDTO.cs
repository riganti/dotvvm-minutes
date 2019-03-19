using System;

namespace DotvvmMinutes.LoadingAnimation.Model
{
    public class CartItemDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsPromo => Price <= 0;

    }
}