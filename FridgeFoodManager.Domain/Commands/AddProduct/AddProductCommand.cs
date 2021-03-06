﻿using System;

namespace FridgeFoodManager.Domain.Commands.AddProduct
{
    public class AddProductCommand : ICommand
    {
        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int MaxDaysAfterOpening { get; set; }
    }
}
