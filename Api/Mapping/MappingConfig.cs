using Application.Products.Commands.AddStorageItem;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Commands.UseProduct;
using Domain.Entities.Products;
using Mapster;
using Shared.Dtos.Products;
using ProductDto = Application.Products.Contracts.ProductDto;

namespace Api.Mapping;

public static class MappingConfig
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<CreateProductDto, CreateProductCommand>.NewConfig();
        TypeAdapterConfig<UpdateProductDto, UpdateProductCommand>.NewConfig();
        TypeAdapterConfig<AddStorageItemDto, AddStorageItemCommand>.NewConfig();
        TypeAdapterConfig<UseProductDto, UseProductCommand>.NewConfig();
    }
}