﻿namespace Domain.Models.OrderModels;

public class DeliveryMethod : BaseEntity<int>
{
    public string ShortName { get; set; }
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Price { get; set; }
}
