// store open or close based on user click
let isClose = true;
// store open or close based on user hover
let isOpenHover = false;

//#region assign event

$(document).ready(() => {
    assignSideMenu();
    assignProfileDropdown();
    assignSideMenuGroups();

    //onSideMenuButtonClick(true);
    //changeSideMenuButtonIcon();
    closeAllGroup();
});

function assignSideMenu() {
    // open when hover
    $(".side-menu").on("mouseenter", () => {
        if (isClose && !isOpenHover) onSideMenuButtonClick(false);
    });

    // close when out
    $(".body-container").on("mouseenter", () => {
        setTimeout(() => {
            if (isClose && isOpenHover) onSideMenuButtonClick(false);
        }, 250);
    });

    $(".side-menu-button").on("click", () => {
        onSideMenuButtonClick(true);
        changeSideMenuButtonIcon();
    });
}

function assignProfileDropdown() {
    let profileDropdown = $(".profile-dropdown");
    let profileButton = profileDropdown.find(".dropdown-button")[0];

    profileDropdown.on("hidden.bs.dropdown", () => {
        if (profileButton.classList.contains("dashboard-text-selected")) {
            profileButton.classList.remove("dashboard-text-selected");
        }
    });

    profileDropdown.on("shown.bs.dropdown", () => {
        if (!profileButton.classList.contains("dashboard-text-selected")) {
            profileButton.classList.add("dashboard-text-selected");
        }
    });
}

function assignSideMenuGroups() {
    let groups = Array.from(document.querySelectorAll(".group"));
    groups.forEach(aTag => {
        let parent = aTag.parentElement;
        let group = parent.querySelector(".item-child");

        if (parent.classList.contains("selected")) onGroupClick(group, aTag);

        aTag.addEventListener("click", () => {
            onGroupClick(group, aTag);
        })
    });
}

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
    let groups = Array.from(document.querySelectorAll(".group"));

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

function changeSideMenuButtonIcon() {
    let button = $(".side-menu-button")[0];

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