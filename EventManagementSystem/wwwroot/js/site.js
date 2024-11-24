import '../scss/site.scss';
import '../scss/dashboard/__dashboard__.scss';
import '../scss/profile/profile.scss';
import '../scss/event.scss';

//#region JS

import 'jquery/dist/jquery.min.js'
import 'jquery-validation/dist/jquery.validate.min.js'
import 'jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js'
import 'datatables.net'
import 'datatables.net-bs5'
import "datatables.net-plugins/sorting/date-eu"
import 'bootstrap/dist/js/bootstrap.bundle.min.js'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-icons/font/bootstrap-icons.css'
import '@fortawesome/fontawesome-free/js/all'
import '@fortawesome/fontawesome-free/css/all.css'
import '@fortawesome/fontawesome-free/css/all.min.css'

// Khởi tạo AOS

import AOS from 'aos'
import 'aos/dist/aos.css'
AOS.init();

//#region Toastr

import toastr from 'toastr';
import 'toastr/build/toastr.min.css';
window.toastr = toastr;

//#endregion

//#endregion

