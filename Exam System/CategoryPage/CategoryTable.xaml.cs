namespace Exam_System.CategoryPage;

public partial class CategoryTable : ContentPage
{
    public CategoryTable()
    {
        InitializeComponent();
        BindingContext = new CategoryTableViewModel();
    }
}