/**
 * Styling paginate of table from datatables for dashboard area
 */
export const stylingDashboardPaginate = () => {
    $('.pagination').find('.page-item').addClass("dashboard-table-paginate");
    $('.pagination').find('.previous').addClass("dashboard-text-selected datatables-previous-icon");
    $('.pagination').find('.next').addClass("dashboard-text-selected datatables-next-icon");
}