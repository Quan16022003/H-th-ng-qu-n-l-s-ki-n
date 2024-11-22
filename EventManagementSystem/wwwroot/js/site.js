// add dependency for webpack
import '../scss/site.scss'
import '../scss/event.scss'

//#region Profile SCSS

import '../scss/profile/profile.scss'

//#endregion

//#region Dashboard SCSS

import '../scss/dashboard/dashboard.scss'
import '../scss/dashboard/dashboard-event.scss'
import '../scss/dashboard/dashboard-category.scss'
import '../scss/dashboard/dashboard-user.scss'

//#endregion

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

//#region custom js

import '../js/service/service.js'

//#endregion

//#endregion

