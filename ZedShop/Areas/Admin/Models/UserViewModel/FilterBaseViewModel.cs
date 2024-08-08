namespace ZedShop.Web.Areas.Admin.Models.UserViewModel
{
    public class FilterBaseViewModel
    {
        public FilterBaseViewModel(int _id, string _name)
        {
            Id = _id;
            Name = _name;   
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
