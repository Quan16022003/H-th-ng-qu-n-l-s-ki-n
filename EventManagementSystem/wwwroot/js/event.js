document.querySelectorAll('.btn.btn-primary').forEach(button => {
    button.addEventListener('click', () => {
        console.log('Button clicked!');
    });
});
window.addEventListener('scroll', function () {
    var navbar = document.getElementById('navbar');
    if (window.scrollY > 50) { // Điều chỉnh giá trị theo ý muốn
        navbar.classList.add('scrolled');
    } else {
        navbar.classList.remove('scrolled');
    }
});
// JavaScript để toggle form bộ lọc
document.querySelector('.d-flex h5').addEventListener('click', function () {
    const filterForm = document.getElementById('filterForm');
    if (filterForm.style.display === 'none' || filterForm.style.display === '') {
        filterForm.style.display = 'block'; // Hiển thị form nếu đang bị ẩn
    } else {
        filterForm.style.display = 'none'; // Ẩn form nếu đang hiển thị
    }
});
function updateTotals() {
    let totalTickets = 0;
    let totalOrder = 0.0;

    // Lấy tất cả các ô chọn vé
    document.querySelectorAll('.ticket-option select').forEach(select => {
        const quantity = parseInt(select.value); // Số lượng vé
        const price = parseFloat(select.getAttribute('data-price')); // Giá của loại vé

        totalTickets += quantity; // Cộng dồn số lượng vé
        totalOrder += quantity * price; // Tính tổng tiền cho loại vé này
    });

    // Cập nhật tổng số lượng và tổng tiền vào các phần tử hiển thị
    document.getElementById('totalTickets').textContent = totalTickets;
    document.getElementById('totalOrder').textContent = totalOrder.toFixed(2) + ' USD';
}
// Mở modal và hiển thị hình ảnh khi bấm vào
function openModal(imageSrc, altText) {
    const modal = document.getElementById("imageModal");
    const modalImg = document.getElementById("imgModalContent");
    const captionText = document.getElementById("caption");

    modal.style.display = "block"; // Hiển thị modal
    modalImg.src = imageSrc; // Đặt nguồn ảnh cho modal
    captionText.innerHTML = altText; // Đặt văn bản mô tả ảnh (nếu có)
}

// Đóng modal khi nhấn vào nút "X" hoặc bên ngoài hình ảnh
function closeModal() {
    document.getElementById("imageModal").style.display = "none";
}

// Gán sự kiện cho mỗi hình ảnh
document.querySelectorAll('.gallery-img').forEach(img => {
    img.addEventListener('click', function () {
        openModal(this.src, this.alt);
    });
});
