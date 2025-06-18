namespace Domain.Exceptions;

public sealed class DeliveryMethodNotfoundException(int id) 
    : NoFoundException($"No Delivery Method With Id {id}");
