namespace WebApi.Controllers.DTOs;

public record CreateSaleRequest(string CustomerFullName, string CustomerNit, List<SaleItemRequestDto> Items);
