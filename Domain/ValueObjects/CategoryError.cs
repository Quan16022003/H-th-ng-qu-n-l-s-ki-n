namespace Domain.ValueObjects
{
    public static class CategoryError
    {
        public static readonly Error NotFound = new Error(
            "Category.NotFound", "Không tìm thấy danh mục.");

        public static readonly Error DuplicateName = new Error(
            "Category.DuplicateName", "Tên danh mục này đã tồn tại, vui lòng chọn tên khác.");

        public static readonly Error InUse = new Error(
            "Category.InUse", "Danh mục đang được sử dụng, không thể xóa.");

        public static readonly Error CreationFailed = new Error(
            "Category.CreationFailed", "Tạo danh mục không thành công, vui lòng thử lại.");

        public static readonly Error UpdateFailed = new Error(
            "Category.UpdateFailed", "Cập nhật danh mục không thành công, vui lòng thử lại.");

        public static readonly Error DeleteFailed = new Error(
            "Category.DeleteFailed", "Xóa danh mục không thành công, vui lòng thử lại.");

        public static readonly Error GetAllFailed = new Error(
            "Category.GetAllFailed", "Lấy danh sách danh mục không thành công, vui lòng thử lại.");

        public static readonly Error GetFailed = new Error(
            "Category.GetFailed", "Lấy thông tin danh mục không thành công, vui lòng thử lại.");
    }
}