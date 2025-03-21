using Application.Products.Commands.AddStorageItem;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Commands.UseProduct;
using Application.Reservation.Commands.ReserveTable;
using Application.Staff.Commands.CreateStaff;
using Application.Staff.Commands.UpdateStaff;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Commands.UpdateTable;
using Application.Tables.Commands.UpdateTableStatus;
using Domain.Entities.Products;
using Mapster;
using Shared.Dtos.Products;
using Shared.Dtos.Reservation;
using Shared.Dtos.Staff;
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

        TypeAdapterConfig<CreateStaffDto, CreateStaffCommand>.NewConfig();
        TypeAdapterConfig<UpdateStaffDto, UpdateStaffCommand>.NewConfig();

        TypeAdapterConfig<CreateTableDto, CreateTableCommand>.NewConfig();
        TypeAdapterConfig<UpdateTableDto, UpdateTableCommand>.NewConfig();
        TypeAdapterConfig<UpdateTableStatusDto, UpdateTableStatusCommand>.NewConfig();
        TypeAdapterConfig<ReserveTableDto, ReserveTableCommand>.NewConfig();
    }
}