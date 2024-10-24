namespace Web.Utils
{
    /// <summary>
    /// Provider subfolder path for Views
    /// </summary>
    public static class PathProvider
    {
        #region Admin

        /// <returns>~/Areas/Admin/Views/Dashboard</returns>
        public static string GetAdminDashboard() => "~/Areas/Admin/Views/Dashboard";

        /// <returns>~/Areas/Admin/Views/ManageEvents</returns>
        public static string GetAdminManageEvents() => "~/Areas/Admin/Views/ManageEvents";

        /// <returns>~/Areas/Admin/Views/ManageBookings</returns>
        public static string GetAdminManageBookings() => "~/Areas/Admin/Views/ManageBookings";

        /// <returns>~/Areas/Admin/Views/ManageUsers</returns>
        public static string GetAdminManageUsers() => "~/Areas/Admin/Views/ManageUsers";

        /// <returns>~/Areas/Admin/Views/ManageSite</returns>
        public static string GetAdminManageSite() => "~/Areas/Admin/Views/ManageSite";

        #endregion

        #region Profile

        /// <returns>~/Areas/Profile/Views/Profile</returns>
        public static string GetProfile() => "~/Areas/Profile/Views/Profile";

        #endregion
    }
}
