// eStore/Components/Pages/Product/ProductListBase.cs
using Microsoft.AspNetCore.Components;
using BLL.DTOs;
using BLL.Interface;

public class ProductListBase : ComponentBase
{
    [Inject] protected IProductService ProductService { get; set; } = null!;
    [Inject] protected ICategoryService CategoryService { get; set; } = null!;

    protected List<ProductDTO> Products { get; set; } = new();
    protected List<ProductDTO> FilteredProducts => ApplyFilters();
    protected List<CategoryDTO> Categories { get; set; } = new();

    protected string SearchTerm { get; set; } = string.Empty;
    protected int CategoryFilter { get; set; } = 0;
    protected string StatusFilter { get; set; } = "all";
    protected bool IsLoading { get; set; } = true;

    protected bool ShowDelete { get; set; }
    protected int? DeleteProductId { get; set; }
    protected bool IsDeleting { get; set; }
    protected string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Categories = await CategoryService.GetAllCategoriesAsync();
        Products = await ProductService.GetAllProductsAsync();
        IsLoading = false;
    }

    protected List<ProductDTO> ApplyFilters()
    {
        var query = Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(SearchTerm))
            query = query.Where(p => p.ProductName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
        if (CategoryFilter > 0)
            query = query.Where(p => p.CategoryId == CategoryFilter);
        if (StatusFilter == "active")
            query = query.Where(p => p.ActiveStatus);
        else if (StatusFilter == "inactive")
            query = query.Where(p => !p.ActiveStatus);
        return query.ToList();
    }

    protected void ShowDeleteDialog(int productId)
    {
        DeleteProductId = productId;
        ShowDelete = true;
        ErrorMessage = null;
    }

    protected void HideDeleteDialog()
    {
        ShowDelete = false;
        DeleteProductId = null;
        ErrorMessage = null;
    }

    protected async Task ConfirmDelete()
    {
        if (DeleteProductId.HasValue)
        {
            IsDeleting = true;
            ErrorMessage = null;
            try
            {
                await ProductService.DeleteProductAsync(DeleteProductId.Value);
                Products = await ProductService.GetAllProductsAsync();
                HideDeleteDialog();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Không thể xóa sản phẩm: " + ex.Message;
            }
            IsDeleting = false;
        }
    }
}