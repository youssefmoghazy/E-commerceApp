using Shared.SharedTransferObjects.Authentication;

namespace Shared.SharedTransferObjects.Orders;

public record OrderRequest(string BasketId, AddressDTO shipToAddress, int DeliveryMethodId);
