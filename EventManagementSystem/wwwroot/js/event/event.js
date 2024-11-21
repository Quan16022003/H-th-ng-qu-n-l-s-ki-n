document.querySelectorAll('.btn.btn-primary').forEach(button => {
    button.addEventListener('click', () => {
        console.log('Button clicked!');
    });
});

$(window).scroll(function () {
    var scroll = $(window).scrollTop();
    if (scroll >= 60) {
        $('#navbar').addClass('scrolled');
        $('#navbar').removeClass('text-light');
    } else {
        $('#navbar').removeClass('scrolled');
        $('#navbar').addClass('text-light');
    }
});

document.querySelector('.d-flex h5').addEventListener('click', function () {
    const filterForm = document.getElementById('filterForm');
    if (filterForm.style.display === 'none' || filterForm.style.display === '') {
        filterForm.style.display = 'block';
    } else {
        filterForm.style.display = 'none';
    }
});

function updateTotals() {
    let totalTickets = 0;
    let totalOrder = 0.0;

    document.querySelectorAll('.ticket-option select').forEach(select => {
        const quantity = parseInt(select.value);
        const price = parseFloat(select.getAttribute('data-price'));

        totalTickets += quantity;
        totalOrder += quantity * price;
    });

    document.getElementById('totalTickets').textContent = totalTickets;
    document.getElementById('totalOrder').textContent = totalOrder.toFixed(2) + ' USD';
}

document.getElementById("registerForm").addEventListener("submit", function (event) {
    const password = document.getElementById("password").value;
    const rePassword = document.getElementById("re-password").value;

    const uppercaseCheck = /[A-Z]/.test(password);
    const numberCheck = /[0-9]/.test(password);

    if (!uppercaseCheck || !numberCheck) {
        alert("Password must contain at least one uppercase letter and one number.");
        event.preventDefault();
        return false;
    }

    if (password !== rePassword) {
        alert("Passwords do not match.");
        event.preventDefault();
        return false;
    }
});

document.getElementById("menuToggle").addEventListener("click", function () {
    const navbarCollapse = document.getElementById("navbarNav");
    if (navbarCollapse.classList.contains("show")) {
        navbarCollapse.classList.remove("show");
    } else {
        $('#navbar').removeClass('scrolled');
    }
});
