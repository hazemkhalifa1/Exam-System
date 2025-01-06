using Exam_System.Models;
namespace Exam_System.CategoryPage;

public partial class CreateCategory : ContentPage
{
    public CreateCategory(Category? category)
    {
        InitializeComponent();
        BindingContext = new CreateCategoryVM(category);
    }
}