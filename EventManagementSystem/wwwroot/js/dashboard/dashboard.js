// store open or close based on user click
let isClose = false;
// store open or close based on user hover
let isOpenHover = false;

//#region assign event

//#region side menu group item

let groups = Array.from(document.querySelectorAll(".group"));

groups.forEach(aTag => {
    let parent = aTag.parentElement;
    let group = parent.querySelector(".item-child");

    if (parent.classList.contains("selected")) onGroupClick(group, aTag);

    aTag.addEventListener("click", () => {
        onGroupClick(group, aTag);
    })
});

//#endregion

//#region side menu slide in out hover while hide

let sideMenu = document.getElementsByClassName("side-menu")[0];
let content = document.getElementsByClassName("body-container")[0];

// open when hover
sideMenu.addEventListener("mouseenter", () => {
    if (isClose && !isOpenHover) onSideMenuButtonClick(false);
});

// close when out
content.addEventListener("mouseenter", () => {
    setTimeout(() => {
        if (isClose && isOpenHover) onSideMenuButtonClick(false);
    }, 250);
});

//#endregion

//#region expand/hide side menu click

let sideMenuButton = document.getElementsByClassName("side-menu-button")[0];
sideMenuButton.addEventListener("click", () => {
    onSideMenuButtonClick(true);
    changeSideMenuButtonIcon(sideMenuButton);
});

// trigger when first load
onSideMenuButtonClick(true);
changeSideMenuButtonIcon(sideMenuButton);

//#endregion

//#region profile button click

let profileDropdown = document.getElementsByClassName("profile-dropdown")[0];
let profileButton = profileDropdown.querySelector(".dropdown-button");

profileDropdown.addEventListener("hidden.bs.dropdown", () => {
    if (profileButton.classList.contains("text-primary")) {
        profileButton.classList.remove("text-primary");
    }
})

profileDropdown.addEventListener("shown.bs.dropdown", () => {
    if (!profileButton.classList.contains("text-primary")) {
        profileButton.classList.add("text-primary");
    }
})

//#endregion

//#endregion

function onGroupClick(container, aTag) {
    //select
    if (container.classList.contains("collapse")) {
        container.classList.remove("collapse");
        aTag.classList.add("selected");
    }
    //close
    else {
        container.classList.add("collapse");
        aTag.classList.remove("selected");
    }
}

function closeAllGroup() {
    groups.forEach(aTag => {
        let parent = aTag.parentElement;
        let group = parent.querySelector(".item-child");
        closeGroup(group, aTag);
    });
}

function closeGroup(container, aTag) {
    if (!container.classList.contains("collapse")) {
        container.classList.add("collapse");
        aTag.classList.remove("selected");
    }
}

/**
 * 
 * @param {boolean} isClick is click or is hover
 */
function onSideMenuButtonClick(isClick) {
    let sideMenu = document.getElementsByClassName("side-menu")[0];
    let box = document.getElementsByClassName("dashboard-box")[0];
    let dashboardHeader = document.getElementsByClassName("dashboard-header")[0];

    if (isClick) isClose = !isClose;

    // close
    if (sideMenu.classList.contains("open")) {
        closeAllGroup();

        sideMenu.classList.remove("open");
        sideMenu.classList.add("close");

        dashboardHeader.classList.add("close");
        box.classList.add("close");

        if (!isClick) isOpenHover = false;
    }
    // open
    else if (sideMenu.classList.contains("close")) {
        sideMenu.classList.remove("close");
        setTimeout(() => sideMenu.classList.add("open"), 250);

        dashboardHeader.classList.remove("close");
        box.classList.remove("close");

        if (!isClick) isOpenHover = true;
    }
}

function changeSideMenuButtonIcon(button) {
    let child = button.children;
    let close = child[0];
    let open = child[1];

    if (isClose) {
        close.classList.add("d-none");
        open.classList.remove("d-none");
    }
    else {
        open.classList.add("d-none");
        close.classList.remove("d-none");
    }
}

/**
 * Custom styling paginate for dashboard table
 * This function at dashboard.js
 */
function stylingAdminPaginate() {
    $('.pagination').find('.previous').addClass("bg-light datatables-previous-icon text-primary");
    $('.pagination').find('.next').addClass("bg-light datatables-next-icon text-primary");
}