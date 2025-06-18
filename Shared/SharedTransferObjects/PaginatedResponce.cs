namespace Shared.SharedTransferObjects;

public record PaginatedResponce<TEntity>(int PageIndex , int PageSize, int Count, IEnumerable<TEntity> Data);
