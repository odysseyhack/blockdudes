namespace BlockDudes.Models
{
    public class AssetEditModel
    {
        private string _title;
        private string _description;

        public string Title
        {
            get => _title; set
            {
                _title = value;
                IsChanged = true;
            }
        }

        public string Description
        {
            get => _description; set
            {
                _description = value;
                IsChanged = true;
            }
        }

        public bool IsChanged { get; set; }

        public static AssetEditModel CreateFromViewModel(AssetViewModel viewModel)
        {
            return new AssetEditModel
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                IsChanged = false
            };
        }

        public void SetViewModelFields(AssetViewModel viewModel)
        {
            viewModel.Title = Title;
            viewModel.Description = Description;
        }
    }
}
